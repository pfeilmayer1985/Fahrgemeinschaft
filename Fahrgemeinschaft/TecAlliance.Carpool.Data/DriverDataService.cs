using TecAlliance.Carpool.Data.Models;

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

        public void AddDriverDaService(Driver driver)
        {
            File.AppendAllText(pathFileDrivers, driver.ToString());
        }

        public void EditDriverDaService(Driver driver)
        {
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);
            
            string editedDriver = $"{driver.ID},{driver.FreePlaces},{driver.FirstName},{driver.LastName},{driver.CarTypeMake},{driver.StartingCity},{driver.Destination}";

            List<string> addAllOtherEntriesBack = showDriversList.Where(e => !e.Contains(driver.ID)).ToList();
            addAllOtherEntriesBack.Add(editedDriver);
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //File.AppendAllText(pathFileDrivers, driverModelDto.ToString());
        }

        public void DeleteDriverDaService(Driver driver)
        {
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);

            List<string> addAllOtherEntriesBack = showDriversList.Where(e => !e.Contains(driver.ID)).ToList();
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
        }

    }
}