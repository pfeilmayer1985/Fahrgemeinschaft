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
        [Route("api/CarPoolApi/GetPassengers")]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            Passenger[] items = passengerBusinessService.ListAllPassengersData();
            return items;
        }
        [HttpGet]
        [Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(string id)
        {
            Passenger item = passengerBusinessService.ListPassengerDataById(id);
            return item;
        }

        [HttpPost]
        [Route("api/CarPoolApi/AddPassenger")]

        public async Task<ActionResult<Passenger>> AddPassenger(PassengerModelDto passengerModelDto)
        {
            Passenger item = passengerBusinessService.AddPassengerBuService(passengerModelDto);
            return item;
        }

    }
}