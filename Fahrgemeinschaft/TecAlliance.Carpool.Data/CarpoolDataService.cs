using System.Linq;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class CarpoolDataService : ICarpoolDataService
    {
        // string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";
        string path = CarpoolTxtPath();

        /// <summary>
        /// This method lists all the carpools/lines from the carpools file
        /// </summary>
        public string[] ListAllCarpoolsDataService()
        {
            string[] showCarpoolList = File.ReadAllLines(path);
            return showCarpoolList;
        }

        /// <summary>
        /// This method defines the path to the carpools file
        /// </summary>
        private static string CarpoolTxtPath()
        {
            var path = Assembly.GetEntryAssembly().Location;
            path = path + "/../../../../../" + "carpools.txt";
            return path;
        }

        /// <summary>
        /// This method appends a new carpool to the carpools file
        /// </summary>
        public void AddCarpoolDaService(CarpoolModel carpool)
        {
            File.AppendAllText(path, carpool.ToString());
        }

        /// <summary>
        /// This method deletes/removes an existing carpool from the carpools file
        /// </summary>
        public void DeleteCarpoolDaService(CarpoolModel carpool)
        {
            string[] showCarpoolsList = File.ReadAllLines(path);

            List<string> addAllOtherEntriesBack = showCarpoolsList.Where(e => !e.Contains(carpool.Driver)).ToList();
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        public string Path
        {
            get
            {
                return this.path;

            }
            set
            {
                this.path = value;
            }
        }
    }
}