using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TecAlliance.Carpool.Business.Providers
{
    public class PassengerProvider : IExamplesProvider<PassengerModelData>
    {
        public PassengerModelData GetExamples()
        {
            return new PassengerModelData()
            {
                ID = "MAXMUS",
                FirstName = "Max",
                LastName = "Mustermann",
                StartingCity = "Schrozberg",
                Destination = "Berlin"
            };
        }
    }
}
