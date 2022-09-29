using System;
using System.Collections.Generic;
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

        public void AddUsersToCarpool()
        {
            Console.WriteLine("To what driver ID do you want to be assigned: ");
            string inputDriverID = Console.ReadLine();



        }
    }


}
