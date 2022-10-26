using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TecAlliance.Carpool.Business.Providers
{
    public class PassengerProvider : IExamplesProvider<Passenger>
    {
        public Passenger GetExamples()
        {
            return new Passenger()
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
