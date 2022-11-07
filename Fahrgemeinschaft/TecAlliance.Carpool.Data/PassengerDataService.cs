using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data

{
    public class PassengerDataService : IPassengerDataService
    {
        string path = PassengersTxtPath();

        /// <summary>
        /// This method lists all the passengers/lines from the passengers file
        /// </summary>
        public string[] ListAllPassengersService()
        {
            string[] showPassengerList = File.ReadAllLines(path);
            return showPassengerList;
        }

        /// <summary>
        /// This method appends a new passenger to the passengers file
        /// </summary>
        public void AddPassengerDaService(Passenger passenger)
        {
            File.AppendAllText(path, passenger.ToString());
        }

        /// <summary>
        /// This method replaces saved infos with new infos for a defined Passenger ID
        /// </summary>
        public void EditPassengerDaService(Passenger passenger)
        {
            string[] showPassengersList = File.ReadAllLines(path);

            string editedPassenger = $"{passenger.ID},{passenger.FirstName},{passenger.LastName},{passenger.StartingCity},{passenger.Destination}";

            List<string> addAllOtherEntriesBack = showPassengersList.Where(e => !e.Contains(passenger.ID)).ToList();
            addAllOtherEntriesBack.Add(editedPassenger);
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        /// <summary>
        /// This method deletes/removes an existing passenger from the passengers file
        /// </summary>
        public void DeletePassengerDaService(Passenger passenger)
        {
            string[] showPassengersList = File.ReadAllLines(path);

            List<string> addAllOtherEntriesBack = showPassengersList.Where(e => !e.Contains(passenger.ID)).ToList();
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        /// <summary>
        /// This method defines the path to the passengers file
        /// </summary>
        private static string PassengersTxtPath()
        {
            var path = Assembly.GetEntryAssembly().Location;
            path = path + "/../../../../../" + "passengers.txt";
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
    }
}