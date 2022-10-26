﻿using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data

{
    public class PassengerDataService : IPassengerDataService
    {
        string path = PassengersTxtPath();

        public string[] ListAllPassengersService()
        {
            string[] showPassengerList = File.ReadAllLines(path);
            return showPassengerList;
        }

        public void AddPassengerDaService(Passenger passenger)
        {
            File.AppendAllText(path, passenger.ToString());
        }

        public void EditPassengerDaService(Passenger passenger)
        {
            string[] showPassengersList = File.ReadAllLines(path);

            string editedPassenger = $"{passenger.ID},{passenger.FirstName},{passenger.LastName},{passenger.StartingCity},{passenger.Destination}";

            List<string> addAllOtherEntriesBack = showPassengersList.Where(e => !e.Contains(passenger.ID)).ToList();
            addAllOtherEntriesBack.Add(editedPassenger);
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        public void DeletePassengerDaService(Passenger passenger)
        {
            string[] showPassengersList = File.ReadAllLines(path);

            List<string> addAllOtherEntriesBack = showPassengersList.Where(e => !e.Contains(passenger.ID)).ToList();
            File.WriteAllLines(path, addAllOtherEntriesBack);
        }

        private static string PassengersTxtPath()
        {
            var path = Assembly.GetEntryAssembly().Location;
            path = path + "/../../../../../" + "passengers.txt";
            return path;
        }
    }
}