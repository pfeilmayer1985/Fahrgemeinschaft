using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolDataService
    {
        void AddCarpoolDaService(CarpoolModel carpool);
        void DeleteCarpoolDaService(CarpoolModel carpool);
        string[] ListAllCarpoolsDataService();
    }
}