using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarpoolsController : ControllerBase
    {
        private readonly ILogger<CarpoolsController> _logger;
        private ICarpoolBusinessService _carpoolBusinessService;

        public CarpoolsController(ILogger<CarpoolsController> logger, ICarpoolBusinessService carpoolBusinessService)
        {
            _carpoolBusinessService = carpoolBusinessService;
            _logger = logger;
        }

        [HttpGet]

        // [Route("api/CarPoolApi/GetCarpools")]
        public async Task<ActionResult<IEnumerable<CarpoolModelDto>>> GetAllCarpools()
        {
            CarpoolModelDto[] items = _carpoolBusinessService.ListAllCarpoolsDataBu();
            return items;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Route("api/CarPoolApi/GetCarpoolById/{id}")]
        public async Task<ActionResult<CarpoolModelDto>> GetCarpoolById(string id)
        {
            CarpoolModelDto items = _carpoolBusinessService.ListCarpoolByIdBu(id.ToUpper());
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeleteCarpoolById")]
        public async Task<IActionResult> DeleteCarpool(string idCarpool)
        {
            var item = _carpoolBusinessService.DeleteCarpoolByDriverIdBu(idCarpool.ToUpper());

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _carpoolBusinessService.DeleteCarpoolByDriverIdBu(idCarpool.ToUpper());
                return NoContent();
            }
        }

        [HttpDelete("{idDriver}/{idPassenger}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeletePassengerFromCarpool")]
        public async Task<IActionResult> DeletePassengerFromCarpool(string idDriver, string idPassenger)
        {
            var items = _carpoolBusinessService.RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(idDriver.ToUpper(), idPassenger.ToUpper());
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                _carpoolBusinessService.RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(idDriver.ToUpper(), idPassenger.ToUpper());
                return NoContent();
            }
        }

        [HttpPost]
        //[Route("api/CarPoolApi/AddCarpool")]
        public async Task<ActionResult<CarpoolModel>> AddCarpool(string idDriver, string idPassenger)
        {
            CarpoolModel item = _carpoolBusinessService.AddCarpoolByPassengerAndDriverIdBu(idDriver.ToUpper(), idPassenger.ToUpper());
            return item;
        }
    }
}