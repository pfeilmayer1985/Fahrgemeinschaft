using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private DriverBusinessService driverBusinessService;

        public DriversController(ILogger<DriversController> logger)
        {
            StringBuilder test = new StringBuilder();
            driverBusinessService = new DriverBusinessService();

            _logger = logger;
        }

        [HttpGet]
        [Route("api/CarPoolApi/GetDrivers")]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAllDrivers()
        {
            Driver[] items = driverBusinessService.ListAllDriverData();
            return items;
        }

        [HttpGet]
        [Route("api/CarPoolApi/GetDriverById/{id}")]
        public async Task<ActionResult<Driver>> GetDriverById(string id)
        {
            Driver items = driverBusinessService.ListDriverById(id);
            return items;
        }


        [HttpPost]
        [Route("api/CarPoolApi/AddDriver")]
                public async Task<ActionResult<Driver>> AddDriver(DriverModelDto driverModelDto)
        {
            Driver item = driverBusinessService.AddDriverBuService(driverModelDto);
            return item;
        }


    }
}