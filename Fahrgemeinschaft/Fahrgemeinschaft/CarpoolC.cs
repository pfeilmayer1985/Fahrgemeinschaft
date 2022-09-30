using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public string Destination { get; set; }
        public List<string> CarpPassengers { get; set; }
        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";



        public CarpoolC(UDrivers driver, UPassengers passengers, string startingCity, string destination, List<string> carpPassengers)
        {
            Driver = driver;
            Passengers = passengers;
            StartingCity = startingCity;
            Destination = destination;
            CarpPassengers = carpPassengers;
        }

        public CarpoolC()
        {
            CarpPassengers = new List<string>();
        }

        public void AddPassengerToCarpool()
        {
            Console.Clear();
            Console.WriteLine("Take a ride (add your passenger PID to an existing driver DID)");
            Console.Write("What id your PID (Passenger ID): ");
            string inputPassengerID = Console.ReadLine();
            bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains("PID" + inputPassengerID));
            if (ckeckInputPassengerID)
            {
                Console.Write("Choose your carpool by driver DID (Driver ID) to be assigned to: ");
                string inputDriverID = Console.ReadLine();
                bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains("DID" + inputDriverID));
                if (ckeckInputDriverID)
                {

                    var linesInDrivers = File.ReadAllLines(pathFileDrivers);
                    var matchLineInDrivers = linesInDrivers[Convert.ToInt32(File.ReadAllLines(pathFileDrivers).ToList().FindIndex(line => line.Contains("DID" + inputDriverID)))];
                    var fields = matchLineInDrivers.Split(',');
                    var number = fields[1];
                  
                    
                    //Console.WriteLine(number);

                   



                    bool checkIfDriverHasACarpool = File.ReadLines(pathFileCarpools).Any(line => line.Contains("DID" + inputDriverID));
                    if (checkIfDriverHasACarpool)
                    {
                        List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();
                        
                        var findDriverInCarpool = theCarpoolList.Where(e => e.Contains("DID" + inputDriverID)).Select(e => e + ",PID" + inputPassengerID).ToList();
                        var addAllOtherEntriesBack = theCarpoolList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                        findDriverInCarpool.AddRange(addAllOtherEntriesBack);

                       
                        File.WriteAllLines(pathFileCarpools, findDriverInCarpool);
                        

                        Console.WriteLine($"You were added the the existing carpool created by driver {"DID" + inputDriverID}.");

                    }
                    else
                    {
                        File.AppendAllText(pathFileCarpools, ("\nDID" + inputDriverID + ";PID" + inputPassengerID));
                        Console.WriteLine($"A new carpool was created by driver {"DID" + inputDriverID} and {"PID" + inputPassengerID} as passenger.");

                    }

                }
                else
                {
                    Console.WriteLine("This Driver ID does not exist.");
                }

            }
            else
            {
                Console.WriteLine("This Passenger ID does not exist.");

            }

        }
    }


}
