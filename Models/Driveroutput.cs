using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriversAPI.Models
{
    public class Driveroutput
    {
        public int averageAge { get; set; }
        public Dictionary<string, int> numberOfBirths { get; set; }
        public Dictionary<string, List<int>> sameLastnames { get; set; }
        public Dictionary<string, int> mostPopularVehicleModels { get; set; }
        public Dictionary<string, int> averageAgeVehicles { get; set; }
        public Dictionary<string, int> engineTypesPrecent { get; set; }
        public List<int> driversFilter1 { get; set; }
        public List<int> driversFilter2 { get; set; }
    }
}
