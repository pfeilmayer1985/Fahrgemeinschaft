using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
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
        public async Task<ActionResult<IEnumerable<PassengerModelDto>>> GetAllPassengers()
        {
            PassengerModelDto[] items = passengerBusinessService.ListAllPassengersData();
            return items;
        }
        [HttpGet]
        [Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<PassengerModelDto>> GetPassengerById(string id)
        {
            PassengerModelDto item = passengerBusinessService.ListPassengerDataById(id);
            return item;
        }

    }
}