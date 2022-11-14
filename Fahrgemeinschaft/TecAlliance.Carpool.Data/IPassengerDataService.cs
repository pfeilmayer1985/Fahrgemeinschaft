using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IPassengerDataService
    {
        /// <summary>
        /// This method appends a new passenger to the passengers file
        /// </summary>
        void AddPassengerDaService(PassengerModelData passenger);

        /// <summary>
        /// This method deletes/removes an existing passenger from the passengers file
        /// </summary>
        void DeletePassengerDaService(PassengerModelData passenger);

        /// <summary>
        /// This method replaces saved infos with new infos for a defined Passenger ID
        /// </summary>
        void EditPassengerDaService(PassengerModelData passenger);

        /// <summary>
        /// This method lists all the passengers/lines from the passengers file
        /// </summary>
        string[] ListAllPassengersService();
    }
}