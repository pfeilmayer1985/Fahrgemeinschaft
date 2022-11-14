using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface IDriverBusinessService
    {
        /// <summary>
        /// This method will add a new driver in the file
        /// </summary>
        DriverModelData AddDriverBuService(DriverModelData driver);

        /// <summary>
        /// This method will delete a driver based on his ID
        /// </summary>
        DriverModelData DeleteDriverBuService(string id);

        /// <summary>
        /// This method will "edit" the infos of a driver based on his ID. ID and Free places are not going to be edited
        /// </summary>
        DriverModelDto EditDriverBuService(string id, DriverModelDto driverModelDto);

        /// <summary>
        /// This method will return a detailed list with the driver IDs and infos
        /// </summary>
        DriverModelData[] ListAllDriverData();

        /// <summary>
        /// This method will return one detailed driver info based on a search after his IDs
        /// </summary>
        DriverModelData ListDriverById(string id);

        /// <summary>
        /// This submethod will generate a driver ID based on the first 3 letters in the First Name and first 3 letters in the Last Name
        /// If the ID allready exists in the file, a new ID will be generated, where the last character of the first 3 letters both in First Name and Last Name
        /// are replaced by a random character.
        /// </summary>
        string SMGenerateAndCheckDriver(string id);
    }
}