using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IDriverDataService
    {
        void AddDriverDaService(Driver driver);
        void DeleteDriverDaService(Driver driver);
        void EditDriverDaService(Driver driver);
        string[] ListAllDriversService();
    }
}