using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class CarpoolDataService
    {
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";
        

        public string[] ListAllCarpoolsDataService()
        {
            string[] showCarpoolList = File.ReadAllLines(pathFileCarpools);
            return showCarpoolList;
        }

        public void AddCarpoolDaService(CarpoolModel carpool)
        {
            File.AppendAllText(pathFileCarpools, carpool.ToString());

        }

        public void DeleteCarpoolDaService(CarpoolModel carpool)
        {
            string[] showCarpoolsList = File.ReadAllLines(pathFileCarpools);

            List<string> addAllOtherEntriesBack = showCarpoolsList.Where(e => !e.Contains(carpool.Driver)).ToList();
            File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBack);
        }

    }
}