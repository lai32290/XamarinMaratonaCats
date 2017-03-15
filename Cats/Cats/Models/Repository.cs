using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.ViewModels;

namespace Cats.Models
{
    public class Repository
    {
        public async Task<List<Cat>> GetCats2()
        {
            List<Cat> Cats;
            var URLWebAPI = "http://demos.ticapacitacion.com/cats";
            using (var Client = new System.Net.Http.HttpClient())
            {
                var JSON = await Client.GetStringAsync(URLWebAPI);
                Cats = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cat>>(JSON);
            }
            return Cats;
        }

        public async Task<List<Cat>> GetCats()
        {
            var Service = new AzureService<Cat>();
            var Items = await Service.GetTable();
            return Items.ToList();
        }
    }
}
