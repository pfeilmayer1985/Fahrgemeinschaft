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
        public async Task<ActionResult<IEnumerable<PassengerModelDto>>> GetAllPassengers()
        {
            PassengerModelDto[] items = passengerBusinessService.ListAllPassengersData();
            return items;
        }
    }
}