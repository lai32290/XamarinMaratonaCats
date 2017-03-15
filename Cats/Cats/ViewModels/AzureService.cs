using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Cats.ViewModels
{
    public class AzureService<T>
    {
        private IMobileServiceClient Client;
        private IMobileServiceTable<T> Table;

        public AzureService()
        {
            string MyAppServiceURL = "http://catsapp32290.azurewebsites.net/";
            Client = new MobileServiceClient(MyAppServiceURL);
            Table = Client.GetTable<T>();
        }

        public Task<IEnumerable<T>> GetTable()
        {
            return Table.ToEnumerableAsync();
        }
    }
}
