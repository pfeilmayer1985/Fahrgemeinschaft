using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Providers
{
    public class CarpoolModelProvider : IExamplesProvider<CarpoolsModelDto>
    {
        public CarpoolsModelDto GetExamples()
        {
            return new CarpoolsModelDto()
            {
                DriverID = 1,
                FreeSeatsRemaining = 1,
                Origin = "Schrozberg",
                Destination = "Weikersheim",
                DepartureDate = new DateOnly(31,12,2022),
                DepartureTime = new TimeOnly(13-30-45)
            };
        }
    }
}

