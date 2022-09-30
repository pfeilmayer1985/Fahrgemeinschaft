using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fahrgemeinschaft
{
    public class CarpoolC
    {
        public UDrivers Driver { get; set; }
        public UPassengers Passengers { get; set; }
        public string StartingCity { get; set; }
        public float TimeStart { get; set; }
        public string Destination { get; set; }
        public List<string> CarpPassengers { get; set; }
        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";



        public CarpoolC(UDrivers driver, UPassengers passengers, string startingCity, float timeStart, string destination, List<string> carpPassengers)
        {
            Driver = driver;
            Passengers = passengers;
            StartingCity = startingCity;
            TimeStart = timeStart;
            Destination = destination;
            CarpPassengers = carpPassengers;
        }

        public CarpoolC()
        {
            CarpPassengers = new List<string>();
        }

        public void AddPassengerToCarpool()
        {
            Console.WriteLine("What id your PID (Passenger ID): ");
            string inputPassengerID = Console.ReadLine();
            Console.WriteLine("To what driver DID (Driver ID) do you want to be assigned: ");
            string inputDriverID = Console.ReadLine();
            string MatchDriver = File.ReadLines(pathFilePassengers).TakeWhile(line => !line.Contains(inputDriverID)).ToString();
            File.AppendAllText(pathFileCarpools, MatchDriver); ;
            string MatchPassenger = File.ReadLines(pathFilePassengers).TakeWhile(line => !line.Contains(inputPassengerID)).ToString();
            File.AppendAllText(pathFileCarpools, MatchPassenger); ;



        }
    }


}
