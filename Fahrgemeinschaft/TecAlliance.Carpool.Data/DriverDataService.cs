using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public class DriverDataService : IDriverDataService
    {
        string path = DriversTxtPath();

        public string[] ListAllDriversService()
        {
            string[] showDriversList = File.ReadAllLines(path);
            return showDriversList;
        }

        public void AddDriverDaService(Driver driver)
        {
            File.AppendAllText(path, driver.ToString());
        }

        public void EditDriverDaService(Driver driver)
        {
            string[] showDriversList = File.ReadAllLines(path);

            string editedDriver = $"{driver.ID},{driver.FreePlaces},{driver.FirstName},{driver.LastName},{driver.CarTypeMake},{driver.StartingCity},{driver.Destination}";

            List<string> addAllOtherEntriesBack = showDriversList.Where(e => !e.Contains(driver.ID)).ToList();
            addAllOtherEntriesBack.Add(editedDriver);
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        private static string DriversTxtPath()
        {
            var path = Assembly.GetEntryAssembly().Location;
            path = path + "/../../../../../" + "drivers.txt";
            return path;
        }

        public void DeleteDriverDaService(Driver driver)
        {
            string[] showDriversList = File.ReadAllLines(path);

            List<string> addAllOtherEntriesBack = showDriversList.Where(e => !e.Contains(driver.ID)).ToList();
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }
    }
}