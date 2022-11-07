using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface ICarpoolDataService
    {

        /// <summary>
        /// This method appends a new carpool to the carpool file
        /// </summary>
        void AddCarpoolDaService(CarpoolModel carpool);

        /// <summary>
        /// This method deletes/removes an existing carpool from the carpool file
        /// </summary>
        void DeleteCarpoolDaService(CarpoolModel carpool);

        /// <summary>
        /// This method lists all the carpools/lines from the carpool file
        /// </summary>
        string[] ListAllCarpoolsDataService();
    }
}