using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarpoolsController : ControllerBase
    {
        private readonly ILogger<CarpoolsController> _logger;
        private CarpoolBusinessService carpoolBusinessService;

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
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                return items;
            }
        }

        [HttpDelete("{idCarpool}")]
        //[Route("api/CarPoolApi/DeleteCarpoolById")]

        public async Task<IActionResult> DeleteCarpool(string idCarpool)
        {
            var item = carpoolBusinessService.DeleteCarpoolByDriverId(idCarpool.ToUpper());

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                carpoolBusinessService.DeleteCarpoolByDriverId(idCarpool.ToUpper());
                return NoContent();
            }

        }


        [HttpDelete("{idDriver}/{idPassenger}")]
        //[Route("api/CarPoolApi/DeletePassengerFromCarpool")]

        public async Task<IActionResult> DeletePassengerFromCarpool(string idDriver, string idPassenger)
        {
            var items = carpoolBusinessService.RemovePassengerFromCarpoolByPassengerIdAndDriverId(idDriver.ToUpper(), idPassenger.ToUpper());
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                carpoolBusinessService.RemovePassengerFromCarpoolByPassengerIdAndDriverId(idDriver.ToUpper(), idPassenger.ToUpper());
                return NoContent();
            }
        }


        [HttpPost]
        //[Route("api/CarPoolApi/AddCarpool")]
        public async Task<ActionResult<CarpoolModel>> AddCarpool(string idDriver, string idPassenger)
        {
            CarpoolModel item = carpoolBusinessService.AddCarpoolBuByPassengerAndDriverId(idDriver.ToUpper(), idPassenger.ToUpper());
            return item;
        }
    }
}