namespace TecAlliance.Carpool.Data
{
    public class CarpoolDataService
    {
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";

        public string[] ListAllCarpoolsService()
        {
            string[] showCarpoolList = File.ReadAllLines(pathFileCarpools);
            return showCarpoolList;
        }

      

    }
}