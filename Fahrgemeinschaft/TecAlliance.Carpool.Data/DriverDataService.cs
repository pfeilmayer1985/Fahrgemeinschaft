namespace TecAlliance.Carpool.Data
{
    public class DriverDataService
    {
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";

        public string[] ListAllDriversService()
        {
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);
            return showDriversList;
        }
    }
}