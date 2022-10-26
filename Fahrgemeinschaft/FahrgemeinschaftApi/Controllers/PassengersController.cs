using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly ILogger<PassengersController> _logger;
        private IPassengerBusinessService _passengerBusinessService;

        public PassengersController(ILogger<PassengersController> logger, IPassengerBusinessService passengerBusinessService)
        {
            StringBuilder test = new StringBuilder();
            _passengerBusinessService = passengerBusinessService;

            _logger = logger;
        }

        [HttpGet]
        //[Route("api/CarPoolApi/GetPassengers")]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            Passenger[] items = _passengerBusinessService.ListAllPassengersData();
            return items;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(string id)
        {
            Passenger item = _passengerBusinessService.ListPassengerDataById(id.ToUpper());

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }

        [HttpPost]
        //[Route("api/CarPoolApi/AddPassenger")]
        public async Task<ActionResult<Passenger>> AddPassenger(PassengerModelDto passenger)
        {
            Passenger item = _passengerBusinessService.AddPassengerBuService(passenger);
            return item;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdatePassenger(string id, PassengerModelDto passenger)
        {
            var item = _passengerBusinessService.EditPassengerBuService(id.ToUpper(), passenger);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _passengerBusinessService.EditPassengerBuService(id.ToUpper(), passenger);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeletePassengerById")]
        public async Task<IActionResult> DeletePassenger(string id)
        {
            var item = _passengerBusinessService.DeletePassengerBuService(id.ToUpper());
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _passengerBusinessService.DeletePassengerBuService(id.ToUpper());
                return NoContent();
            }
        }
    }
}