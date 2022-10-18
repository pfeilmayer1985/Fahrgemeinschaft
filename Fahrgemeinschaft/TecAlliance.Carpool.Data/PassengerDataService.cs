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
    }
}