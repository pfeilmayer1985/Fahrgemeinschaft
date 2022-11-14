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
        private IDriverBusinessService _driverBusinessService;
        public DriversController(ILogger<DriversController> logger, IDriverBusinessService driverBusinessService)
        {
            StringBuilder test = new StringBuilder();
            _driverBusinessService = driverBusinessService;

            _logger = logger;
        }

        [HttpGet]
        //[Route("api/CarPoolApi/GetDrivers")]
        public async Task<ActionResult<IEnumerable<DriverModelData>>> GetAllDrivers()
        {

            DriverModelData[] items = _driverBusinessService.ListAllDriverData();
            return items;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetDriverById/{id}")]
        public async Task<ActionResult<DriverModelData>> GetDriverById(string id)
        {
            DriverModelData items = _driverBusinessService.ListDriverById(id.ToUpper());
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
        public async Task<ActionResult<DriverModelData>> AddDriver(DriverModelData driver)
        {
            DriverModelData item = _driverBusinessService.AddDriverBuService(driver);
            return item;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdateDriver(string id, DriverModelDto driver)
        {
            var items = _driverBusinessService.EditDriverBuService(id.ToUpper(), driver);
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                _driverBusinessService.EditDriverBuService(id.ToUpper(), driver);
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
            var item = _driverBusinessService.DeleteDriverBuService(id.ToUpper());
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _driverBusinessService.DeleteDriverBuService(id.ToUpper());
                return NoContent();
            }
        }
    }
}