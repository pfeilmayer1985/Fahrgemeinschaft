using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Providers
{
    public class CarpoolModelProvider : IExamplesProvider<CarpoolModel>
    {
        public CarpoolModel GetExamples()
        {
            return new CarpoolModel()
            {
                Driver = "EMISOM",
                Passengers = new List<string>() { "ROBPFE" }
            };
        }
    }
}

