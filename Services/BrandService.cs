using DriversAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace DriversAPI.Services
{
    public class BrandService
    {
        private string JsonName
        {
            get { return ".\\Data\\brands.json"; }
        }

        public IEnumerable<Brand> GetBrands()
        {
            using (var jsonFileReader = File.OpenText(JsonName))
            {
                return JsonSerializer.Deserialize<Brand[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        public string getBrandNameById(int id)
        {
            IEnumerable<Brand> brands = GetBrands();
            var query = brands.FirstOrDefault(x => x.Id == id);

            if (query == null) return "badId";
            return query.BrandName;
        }
    }
}
