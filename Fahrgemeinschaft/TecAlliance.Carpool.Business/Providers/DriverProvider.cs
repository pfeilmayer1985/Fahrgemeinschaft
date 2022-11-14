using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Providers
{
    public class DriverProvider : IExamplesProvider<DriverModelData>
    {
        public DriverModelData GetExamples()
        {
            return new DriverModelData()
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

