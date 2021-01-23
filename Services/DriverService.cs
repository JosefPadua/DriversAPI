using DriversAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DriversAPI.Services
{
    public class DriverService
    {
        public DriverService(BrandService bs)
        {
            this.BrandSer = bs;
        }
        
        private Dictionary<string, int> CalculateEnginePrecent(Dictionary<string, List<Driver>> dictionary, List<Driver> drivers)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach(var item in dictionary)
            {
                int procent;
                procent = (item.Value.Count*100)/drivers.Count;
                list.Add(item.Key, procent);
            }

            return list;
        }

        public BrandService BrandSer { get; set; }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
             age = age - 1;

            return age;
        }

        private int CalculateAgeFromYear(int year)
        {
            int age = 0;
            age = DateTime.Now.Year - year;
            return age;
        }

        private Dictionary<string, List<int>> ClearOneId(Dictionary<string, List<int>> dictionary)
        {
            foreach(var item in dictionary.Where(x => x.Value.Count == 1))
            {
                dictionary.Remove(item.Key);
            }
 
            return dictionary;
        }

        private Dictionary<string, int> getMostUsingVehModels(List<int> vehbrands)
        {
            Dictionary<string, int> models = new Dictionary<string, int>();
            foreach (int vehicleBrands in vehbrands)
            {
                string vehmodelname = BrandSer.getBrandNameById(vehicleBrands);
 
                try
                {
                    models[vehmodelname]++;
                } 
                catch(KeyNotFoundException)
                {
                    models.Add(vehmodelname, 1);
                }

            }
            int[] sizes = new int[models.Values.Count];
            int i = 0;
            foreach(int value in models.Values)
            {
                sizes[i] = value;
                i++;
            }
            List<int> thbiggest = get3largest(sizes, models.Values.Count);
            Console.Out.Write(thbiggest);

            var modelstodelete = models.Where(x => !thbiggest.Contains(x.Value));
            foreach(var model in modelstodelete)
            {
                models.Remove(model.Key);
            }
            return models;
        }
        private List<int> get3largest(int[] arr,
                              int arr_size)
        {
            List<int> sizes = new List<int>();
            int i, first, second, third;


            if (arr_size < 3)
            {
                sizes.Add(arr.Max());
                return sizes;
            }

            third = first = second = 000;
            for (i = 0; i < arr_size; i++)
            {

                if (arr[i] > first)
                {
                    third = second;
                    second = first;
                    first = arr[i];
                }


                else if (arr[i] > second)
                {
                    third = second;
                    second = arr[i];
                }

                else if (arr[i] > third)
                    third = arr[i];
            }
            sizes.Insert(0, first);
            sizes.Insert(1, second);
            sizes.Insert(2, third);
            
            return sizes;
        }

        private Dictionary<string, int> calcAverage(Dictionary<string, List<int>> dictionary)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var vehages in dictionary)
            {
                double average = vehages.Value.Average();
                int averageint = Convert.ToInt32(average);

                list.Add(vehages.Key, averageint); 
            }
            return list;
        }

        public Driveroutput ProceedData(List<Driver> drivera)
        { 
            Driveroutput dout = new Driveroutput();
            List<int> ages = new List<int>();
            Dictionary<string, int> years = new Dictionary<string, int>();
            Dictionary<string, List<int>> lastNames = new Dictionary<string, List<int>>();
            List<int> vehicleBrandIds = new List<int>();
            Dictionary<string, List<int>> vehicleages = new Dictionary<string, List<int>>();
            Dictionary<string, List<Driver>> vehEngine = new Dictionary<string, List<Driver>>();
            List<int> filter1list = new List<int>();
            List<int> filter2list = new List<int>();

            drivera.ForEach(driver =>
            {
                int age = CalculateAge(driver.driver.birthDate);
                ages.Add(age);

                int year = driver.driver.birthDate.Year;
                string yearstring = year.ToString();
                foreach(VehinfoObject vehicles in driver.vehicleInfo)
                {
                    vehicleBrandIds.Add(vehicles.brandId);
                    int ageveh = CalculateAgeFromYear(vehicles.modelYear);
                    string vehicleBrand = BrandSer.getBrandNameById(vehicles.brandId);
                    foreach(var vehdata in driver.vehicleInfo)
                    {
                        string eng = vehdata.engineType;
                        try
                        {
                            if(!vehEngine[eng].Contains(driver)) vehEngine[eng].Add(driver);
                        }
                        catch(KeyNotFoundException)
                        {
                            List<Driver> list = new List<Driver>();
                            list.Add(driver);
                            vehEngine.Add(eng, list);
                        }
                    }
                     
                    try
                    {
                        vehicleages[vehicleBrand].Add(ageveh);
                    }
                    catch(KeyNotFoundException)
                    {
                        List<int> list = new List<int>();
                        list.Add(ageveh);
                        vehicleages.Add(vehicleBrand, list);
                    }
                }

                try
                {
                    years[yearstring]++;
                } 
                catch(KeyNotFoundException)
                {
                    years.Add(yearstring, 1);
                }

                string lastName = driver.driver.lastName;
                int id = driver.id;

                try
                {
                    lastNames[lastName].Add(id);
                } 
                catch(KeyNotFoundException)
                {
                    List<int> list = new List<int>();
                    list.Add(id);
                    lastNames.Add(lastName, list);
                }
            });
            foreach(Driver driver in drivera.Where(x => x.driver.eyeColor.ToLower().Equals("blue") && x.vehicleInfo.Any(y => y.engineType.ToLower().Equals("hybrid") || y.engineType.ToLower().Equals("electric"))))
            {
                filter1list.Add(driver.id);
            }
            foreach(Driver driver in drivera.Where(x => x.vehicleInfo.Count > 1 && x.vehicleInfo.All(y => y.engineType.Equals(x.vehicleInfo[0].engineType))))
            {
                filter2list.Add(driver.id);
            }

            double averageage = Queryable.Average(ages.AsQueryable());
            int averageageint = Convert.ToInt32(averageage);
            dout.averageAge = averageageint;
            dout.numberOfBirths = years;
            dout.sameLastnames = ClearOneId(lastNames);
            dout.mostPopularVehicleModels = getMostUsingVehModels(vehicleBrandIds);
            dout.averageAgeVehicles = calcAverage(vehicleages);
            dout.engineTypesPrecent = CalculateEnginePrecent(vehEngine, drivera);
            dout.driversFilter1 = filter1list;
            dout.driversFilter2 = filter2list;

            return dout;
            
        }
    }
}
