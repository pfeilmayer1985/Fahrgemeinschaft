using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Controllers
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
        //[Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(string id)
        {
            Passenger item = passengerBusinessService.ListPassengerDataById(id.ToUpper());
            return item;
        }

        [HttpPost]
        //[Route("api/CarPoolApi/AddPassenger")]

        public async Task<ActionResult<Passenger>> AddPassenger(PassengerModelDto passenger)
        {
            Passenger item = passengerBusinessService.AddPassengerBuService(passenger);
            return item;
        }

        [HttpPut("{id}")]
        //[Route("api/CarPoolApi/EditDriverById")]

        public async Task<IActionResult> UpdatePassenger(string id, PassengerModelDto passenger)
        {

            passengerBusinessService.EditPassengerBuService(id.ToUpper(), passenger);
            return NoContent();

        }

        [HttpDelete("{id}")]
        //[Route("api/CarPoolApi/DeletePassengerById")]

        public async Task<IActionResult> DeletePassenger(string id)
        {
            passengerBusinessService.DeletePassengerBuService(id.ToUpper());
            return NoContent();
        }

    }
}