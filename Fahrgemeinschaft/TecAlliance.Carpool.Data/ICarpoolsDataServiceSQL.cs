using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolsDataServiceSQL
    {
        List<CarpooslModelData> ListAllCarpoolsDataService();
    }
}