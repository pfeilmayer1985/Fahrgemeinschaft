using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface IDriverBusinessService
    {
        Driver AddDriverBuService(Driver driver);
        Driver DeleteDriverBuService(string id);
        DriverModelDto EditDriverBuService(string id, DriverModelDto driverModelDto);
        Driver[] ListAllDriverData();
        Driver ListDriverById(string id);
        string SMGenerateAndCheckDriver(string id);
    }
}