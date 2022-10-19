using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
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
        public async Task<ActionResult<IEnumerable<DriverModelDto>>> GetAllDrivers()
        {
            DriverModelDto[] items = driverBusinessService.ListAllDriverData();
            return items;
        }

        [HttpGet]
        [Route("api/CarPoolApi/GetDriverById/{id}")]
        public async Task<ActionResult<DriverModelDto>> GetDriverById(string id)
        {
            DriverModelDto items = driverBusinessService.ListDriverById(id);
            return items;
        }
    }
}