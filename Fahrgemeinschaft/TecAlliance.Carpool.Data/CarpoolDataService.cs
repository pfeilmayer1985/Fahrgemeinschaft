namespace TecAlliance.Carpool.Data
{
    public class CarpoolDataService
    {
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passenger.txt";


        public string[] ListAllCarpoolsDataService()
        {
            string[] showCarpoolList = File.ReadAllLines(pathFileCarpools);
            return showCarpoolList;
        }

    }
}