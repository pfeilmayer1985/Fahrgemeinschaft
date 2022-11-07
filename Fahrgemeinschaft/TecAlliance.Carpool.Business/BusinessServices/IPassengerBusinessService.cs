using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface IPassengerBusinessService
    {
        /// <summary>
        /// This method will add a new passenger in the file
        /// </summary>
        Passenger AddPassengerBuService(PassengerModelDto passengerModelDto);

        /// <summary>
        /// This method will delete a passenger based on his ID
        /// </summary>
        Passenger DeletePassengerBuService(string id);

        /// <summary>
        /// This method will "edit" the infos of a passenger based on his ID
        /// </summary>
        PassengerModelDto EditPassengerBuService(string id, PassengerModelDto dtoPassenger);

        /// <summary>
        /// This method will return a detailed list with the passenger IDs and infos
        /// </summary>
        Passenger[] ListAllPassengersData();

        /// <summary>
        /// This method will return one detailed passenger info based on a search after his IDs
        /// </summary>
        Passenger ListPassengerDataById(string id);

        /// <summary>
        /// Remapping a Passenger obj to a DTO obj
        /// </summary>
        PassengerModelDto MapToModelDtoPassenger(Passenger passenger);

        /// <summary>
        /// This submethod will generate a Passenger ID based on the first 3 letters in the First Name and first 3 letters in the Last Name
        /// If the ID allready exists in the file, a new ID will be generated, where the last character of the first 3 letters both in First Name and Last Name
        /// are replaced by a random character.
        /// </summary>
        string SMGenerateAndCheckPassenger(string id);

        /// <summary>
        /// This submethod will return random character to be used in the ID generator
        /// </summary>
        string SMGetaRandomChar();
    }
}