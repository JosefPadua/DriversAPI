using DriversAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriversAPI.Services;
using System.Text.Json;

namespace DriversAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriversdataController : Controller
    {
     

        public DriversdataController(DriverService driverService)
        {
            this.Driverser = driverService;
        }

        public DriverService Driverser { get; set; }


        [HttpPost]
        public JsonResult Post([FromBody] List<Driver> drivers)
        {
            Driveroutput driveroutput = Driverser.ProceedData(drivers);
            return Json(new
            {
                data = driveroutput
            });
            
        }

    }
}
