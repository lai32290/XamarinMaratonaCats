using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Xamarin.Forms;

namespace Cats.ViewModels
{
    class CatsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Cat> Cats { get; set; }
        public Command GetCatsCommand { get; set;}
        private bool Busy;

        public bool IsBusy
        {
            get { return Busy; }
            set
            {
                Busy = value;
                OnPropertyChanged();
                GetCatsCommand.ChangeCanExecute();
            }
        }
        public CatsViewModel()
        {
            Cats = new ObservableCollection<Cat>();

            GetCatsCommand = new Command(
                async () => await GetCats(),
                () => !IsBusy
                );
        }

        async Task GetCats()
        {
            if (IsBusy)
                return;

            Exception Error = null;
            try
            {
                IsBusy = true;
                var Repository  = new Repository();
                var Items = await Repository.GetCats();
                Cats.Clear();
                foreach (var Cat in Items)
                {
                    Cats.Add(Cat);
                }
            }
            catch (Exception e)
            {
                Error = e;
            }
            finally
            {
                IsBusy = false;
            }

            if (Error != null)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error!", Error.Message, "Ok");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
