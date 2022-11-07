using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IDriverDataService
    {
        /// <summary>
        /// This method appends a new driver to the drivers file
        /// </summary>
        void AddDriverDaService(Driver driver);

        /// <summary>
        /// This method deletes/removes an existing driver from the drivers file
        /// </summary>
        void DeleteDriverDaService(Driver driver);

        /// <summary>
        /// This method replaces saved infos with new infos for a defined Driver ID
        /// </summary>
        void EditDriverDaService(Driver driver);

        /// <summary>
        /// This method lists all the drivers/lines from the drivers file
        /// </summary>
        string[] ListAllDriversService();
    }
}