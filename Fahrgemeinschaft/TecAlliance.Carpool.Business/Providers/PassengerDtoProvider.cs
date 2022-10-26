using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.Providers
{
    public class PassengerDtoProvider : IExamplesProvider<PassengerModelDto>
    {
        public PassengerModelDto GetExamples()
        {
            return new PassengerModelDto()
            {
                FirstName = "Max",
                LastName = "Mustermann",
                StartingCity = "Schrozberg",
                Destination = "Berlin"
            };
        }
    }
}
