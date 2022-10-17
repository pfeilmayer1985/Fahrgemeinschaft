using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;

namespace TecAlliance.Carpool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<PassengersController> _logger;

        public PassengersController(ILogger<PassengersController> logger)
        {
            StringBuilder test = new StringBuilder();
            

            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public bool Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
            return true;
        }
    }
}