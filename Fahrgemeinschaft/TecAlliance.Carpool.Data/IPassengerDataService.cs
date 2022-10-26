using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IPassengerDataService
    {
        void AddPassengerDaService(Passenger passenger);
        void DeletePassengerDaService(Passenger passenger);
        void EditPassengerDaService(Passenger passenger);
        string[] ListAllPassengersService();
    }
}