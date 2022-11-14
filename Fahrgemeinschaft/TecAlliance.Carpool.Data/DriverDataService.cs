using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class DriverDataService : IDriverDataService
    {


        string path = DriversTxtPath();

        /// <summary>
        /// This method lists all the drivers/lines from the drivers file
        /// </summary>
        public string[] ListAllDriversService()
        {
            string[] showDriversList = File.ReadAllLines(path);
            return showDriversList;
        }

        /// <summary>
        /// This method appends a new driver to the drivers file
        /// </summary>
        public void AddDriverDaService(DriverModelData driver)
        {
            File.AppendAllText(path, driver.ToString());
        }

        /// <summary>
        /// This method replaces saved infos with new infos for a defined Driver ID
        /// </summary>
        public void EditDriverDaService(DriverModelData driver)
        {
            string[] showDriversList = File.ReadAllLines(path);

            string editedDriver = $"{driver.ID},{driver.FreePlaces},{driver.FirstName},{driver.LastName},{driver.CarTypeMake},{driver.StartingCity},{driver.Destination}";

            List<string> addAllOtherEntriesBack = showDriversList.Where(e => !e.Contains(driver.ID)).ToList();
            addAllOtherEntriesBack.Add(editedDriver);
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        /// <summary>
        /// This method defines the path to the drivers file
        /// </summary>
        private static string DriversTxtPath()
        {
            var path = Assembly.GetEntryAssembly().Location;
            path = path + "/../../../../../" + "drivers.txt";
            return path;
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

        /// <summary>
        /// This method deletes/removes an existing driver from the drivers file
        /// </summary>
        public void DeleteDriverDaService(DriverModelData driver)
        {
            string[] showDriversList = File.ReadAllLines(path);

            List<string> addAllOtherEntriesBack = showDriversList.Where(e => !e.Contains(driver.ID)).ToList();
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }
    }
}