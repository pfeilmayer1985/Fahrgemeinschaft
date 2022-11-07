using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface ICarpoolBusinessService
    {
        /// <summary>
        /// This method will either build a new carpool based on Driver ID and Passenger ID or add to an existing Driver ID carpool the Passenger ID
        /// </summary>
        CarpoolModel? AddCarpoolByPassengerAndDriverIdBu(string inputDriverID, string inputPassengerID);

        /// <summary>
        /// This method will delete an existing carpool based on Driver ID
        /// </summary>
        CarpoolModelDto DeleteCarpoolByDriverIdBu(string id);

        /// <summary>
        /// This method will return a detailed carpool list with drivers and passengers IDs and infos
        /// </summary>
        CarpoolModelDto[] ListAllCarpoolsDataBu();

        /// <summary>
        /// This method will return one detailed carpool info based on a search after driver ID and passenger IDs
        /// </summary>
        CarpoolModelDto ListCarpoolByIdBu(string id);

        /// <summary>
        /// This method will either remove an passenger from an existing carpool/Driver ID or if he is the only passenger 
        /// then will dissolve/delete the existing carpool based on Driver ID
        /// </summary>
        CarpoolModelDto RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(string inputDriverID, string inputPassengerID);
    }
}