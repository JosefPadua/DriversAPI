using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriversAPI.Models
{
    public class Driver
    {
        public int id { get; set; }
        public Driverobject driver { get; set; }
        public List<VehinfoObject> vehicleInfo { get; set; }
    }

    public class Driverobject
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string eyeColor { get; set; }
        public string company { get; set; }
        public DateTime birthDate { get; set; }
        public string city { get; set; }
    }
    public class VehinfoObject
    {
        public int brandId { get; set; }
        public int modelYear { get; set; }
        public string engineType { get; set; }
    }
}
