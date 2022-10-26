using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface IPassengerBusinessService
    {
        Passenger AddPassengerBuService(PassengerModelDto passengerModelDto);
        Passenger DeletePassengerBuService(string id);
        PassengerModelDto EditPassengerBuService(string id, PassengerModelDto dtoPassenger);
        Passenger[] ListAllPassengersData();
        Passenger ListPassengerDataById(string id);
        PassengerModelDto MapToModelDtoPassenger(Passenger passenger);
        string SMGenerateAndCheckPassenger(string id);
        string SMGetaRandomChar();
    }
}