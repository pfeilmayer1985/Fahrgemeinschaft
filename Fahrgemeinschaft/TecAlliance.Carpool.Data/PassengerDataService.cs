using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data

{
    public class PassengerDataService
    {
        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";

        public string[] ListAllPassengersService()
        {
            string[] showPassengerList = File.ReadAllLines(pathFilePassengers);
            return showPassengerList;
        }

        public void AddPassengerDaService(Passenger passenger)
        {
            File.AppendAllText(pathFilePassengers, passenger.ToString());
        }


        public void EditPassengerDaService(Passenger passenger)
        {
            string[] showPassengersList = File.ReadAllLines(pathFilePassengers);

            string editedPassenger = $"{passenger.ID},{passenger.FirstName},{passenger.LastName},{passenger.StartingCity},{passenger.Destination}";

            List<string> addAllOtherEntriesBack = showPassengersList.Where(e => !e.Contains(passenger.ID)).ToList();
            addAllOtherEntriesBack.Add(editedPassenger);
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
        }

        public void DeletePassengerDaService(Passenger passenger)
        {
            string[] showPassengersList = File.ReadAllLines(pathFilePassengers);

            List<string> addAllOtherEntriesBack = showPassengersList.Where(e => !e.Contains(passenger.ID)).ToList();
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
        }

    }
}