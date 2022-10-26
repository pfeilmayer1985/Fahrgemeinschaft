using System.Linq;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class CarpoolDataService : ICarpoolDataService
    {
        // string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";
        string path = CarpoolTxtPath();

        public string[] ListAllCarpoolsDataService()
        {
            string[] showCarpoolList = File.ReadAllLines(path);
            return showCarpoolList;
        }

        private static string CarpoolTxtPath()
        {
            var path = Assembly.GetEntryAssembly().Location;
            path = path + "/../../../../../" + "carpools.txt";
            return path;
        }

        public void AddCarpoolDaService(CarpoolModel carpool)
        {
            File.AppendAllText(path, carpool.ToString());
        }

        public void DeleteCarpoolDaService(CarpoolModel carpool)
        {
            string[] showCarpoolsList = File.ReadAllLines(path);

            List<string> addAllOtherEntriesBack = showCarpoolsList.Where(e => !e.Contains(carpool.Driver)).ToList();
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }
    }
}