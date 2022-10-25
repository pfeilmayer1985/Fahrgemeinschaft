using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public class DriverDtoProvider : IExamplesProvider<DriverModelDto>
    {
        public DriverModelDto GetExamples()
        {
            return new DriverModelDto()
            {
                FirstName = "Max",
                LastName = "Mustemann",
                CarTypeMake = "Peugeot 507",
                StartingCity = "Stuttgart",
                Destination = "Schrozberg"
            };

        }




    }
}

