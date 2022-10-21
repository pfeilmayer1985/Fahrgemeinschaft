using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
namespace TecAlliance.Carpool.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarpoolsController : ControllerBase
    {
        private readonly ILogger<CarpoolsController> _logger;
        private CarpoolBusinessService carpoolBusinessService;
       // private PassengerBusinessService passengerBusinessService;
       // private DriverBusinessService driverBusinessService;

        public CarpoolsController(ILogger<CarpoolsController> logger)
        {
            StringBuilder test = new StringBuilder();
            carpoolBusinessService = new CarpoolBusinessService();

            _logger = logger;
        }

        [HttpGet]
        // [Route("api/CarPoolApi/GetCarpools")]
        public async Task<ActionResult<IEnumerable<CarpoolModelDto>>> GetAllCarpools()
        {
            CarpoolModelDto[] items = carpoolBusinessService.ListAllCarpoolsData();
            return items;
        }

        [HttpGet("{id}")]
        // [Route("api/CarPoolApi/GetCarpoolById/{id}")]
        public async Task<ActionResult<CarpoolModelDto>> GetCarpoolById(string id)
        {
            CarpoolModelDto items = carpoolBusinessService.ListCarpoolById(id.ToUpper());
            return items;
        }

        [HttpDelete("{idCarpool}")]
        //[Route("api/CarPoolApi/DeleteCarpoolById")]

        public async Task<IActionResult> DeleteCarpool(string idCarpool)
        {
            carpoolBusinessService.DeleteCarpoolByDriverId(idCarpool.ToUpper());
            return NoContent();
        }


        [HttpDelete("{idDriver}/{idPassenger}")]
        //[Route("api/CarPoolApi/DeletePassengerFromCarpool")]

        public async Task<IActionResult> DeletePassengerFromCarpool(string idDriver, string idPassenger)
        {
            carpoolBusinessService.DeleteCarpoolByPassengerAndDriverId(idDriver.ToUpper(), idPassenger.ToUpper());
            return NoContent();
        }
    }
}