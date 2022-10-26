using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
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
        //[Route("api/CarPoolApi/GetDrivers")]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAllDrivers()
        {

            Driver[] items = driverBusinessService.ListAllDriverData();
            return items;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetDriverById/{id}")]
        public async Task<ActionResult<Driver>> GetDriverById(string id)
        {
            Driver items = driverBusinessService.ListDriverById(id.ToUpper());
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                return items;
            }
        }

        [HttpPost]
        //[Route("api/CarPoolApi/AddDriver")]
        public async Task<ActionResult<Driver>> AddDriver(Driver driver)
        {
            Driver item = driverBusinessService.AddDriverBuService(driver);
            return item;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdateDriver(string id, DriverModelDto driver)
        {
            var items = driverBusinessService.EditDriverBuService(id.ToUpper(), driver);
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                driverBusinessService.EditDriverBuService(id.ToUpper(), driver);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeleteDriverById")]
        public async Task<IActionResult> DeleteDriver(string id)
        {
            var item = driverBusinessService.DeleteDriverBuService(id.ToUpper());
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                driverBusinessService.DeleteDriverBuService(id.ToUpper());
                return NoContent();
            }
        }
    }
}