using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Providers
{
    public class DriverProvider : IExamplesProvider<Driver>
    {
        public Driver GetExamples()
        {
            return new Driver()
            {
                ID = "MAXMUS",
                FreePlaces = 4,
                FirstName = "Max",
                LastName = "Mustemann",
                CarTypeMake = "Peugeot 507",
                StartingCity = "Stuttgart",
                Destination = "Schrozberg"

            };
        }

    }
}

