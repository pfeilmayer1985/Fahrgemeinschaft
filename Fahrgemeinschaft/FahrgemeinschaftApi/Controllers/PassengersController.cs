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
        private PassengerBusinessService passengerBusinessService;

        public PassengersController(ILogger<PassengersController> logger)
        {
            StringBuilder test = new StringBuilder();
            passengerBusinessService = new PassengerBusinessService();

            _logger = logger;
        }

        [HttpGet]
        //[Route("api/CarPoolApi/GetPassengers")]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            Passenger[] items = passengerBusinessService.ListAllPassengersData();
            return items;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(string id)
        {
            Passenger item = passengerBusinessService.ListPassengerDataById(id.ToUpper());

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
            Passenger item = passengerBusinessService.AddPassengerBuService(passenger);
            return item;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdatePassenger(string id, PassengerModelDto passenger)
        {
            var item = passengerBusinessService.EditPassengerBuService(id.ToUpper(), passenger);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                passengerBusinessService.EditPassengerBuService(id.ToUpper(), passenger);
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
            var item = passengerBusinessService.DeletePassengerBuService(id.ToUpper());
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                passengerBusinessService.DeletePassengerBuService(id.ToUpper());
                return NoContent();
            }
        }
    }
}