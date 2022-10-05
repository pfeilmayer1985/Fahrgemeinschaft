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
            //asking for the passenger ID and checking if the ID exists in the passenger list
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Take a ride (add your passenger PID to an existing driver DID)");
            Console.ResetColor();

            Console.Write("What id your PID (Passenger ID): ");
            string inputPassengerID = Console.ReadLine();
            bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains("PID" + inputPassengerID));
            if (ckeckInputPassengerID)
            {
                //asking for the driver ID and checking if the ID exists in the drivers list
                Console.Write("Choose your carpool by driver DID (Driver ID) to be assigned to: ");
                string inputDriverID = Console.ReadLine();


                bool checkBothDriverAndPassenger = false;
                string[] linesInCarpool = File.ReadAllLines(pathFileCarpools);
                checkBothDriverAndPassenger = CheckBothExistance(linesInCarpool, inputDriverID, inputPassengerID, checkBothDriverAndPassenger);


                bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains("DID" + inputDriverID));
                if (ckeckInputDriverID)
                {

                    string[] linesInDrivers = File.ReadAllLines(pathFileDrivers);

                    bool checkIfDriverHasACarpool = File.ReadLines(pathFileCarpools).Any(line => line.Contains("DID" + inputDriverID));

                    foreach (string line in linesInDrivers)
                    {
                        string[] splittedLinesInDriversArray = line.Split(',');
                        if (splittedLinesInDriversArray[0].Equals("DID" + inputDriverID))
                        {
                            int numberOfFreeSeats = Convert.ToInt32(splittedLinesInDriversArray[1]);


                            if (checkBothDriverAndPassenger)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"User {"PID" + inputPassengerID} is allready a member of the carpool {"DID" + inputDriverID}. Choose another carpool!");
                                Console.ResetColor();
                            }
                            else
                            {

                                //if there are NO free seats remaining inform the pasenger that the carpool is full and no seat can be taken
                                if (numberOfFreeSeats == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine($"Unfortunately you can't be added to the carpool because there are NO free seats available.");
                                    Console.ResetColor();

                                }
                                //if there is at least one free seat, take it and change the available seats to seats - 1
                                else
                                {

                                    if (checkIfDriverHasACarpool)
                                    {
                                        //adding the new passenger to the carpool
                                        List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();
                                        var findDriverInCarpool = theCarpoolList.Where(e => e.Contains("DID" + inputDriverID)).Select(e => e + ",PID" + inputPassengerID).ToList();
                                        var addAllOtherEntriesBack = theCarpoolList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                                        findDriverInCarpool.AddRange(addAllOtherEntriesBack);
                                        File.WriteAllLines(pathFileCarpools, findDriverInCarpool);

                                        //changing the free seats fo freeseats-1
                                        List<string> theDriverslList = File.ReadAllLines(pathFileDrivers).ToList();
                                        string j = $"{"DID" + inputDriverID},{numberOfFreeSeats - 1},{splittedLinesInDriversArray[2]},{splittedLinesInDriversArray[3]},{splittedLinesInDriversArray[4]},{splittedLinesInDriversArray[5]}";
                                        var addAllOtherEntriesBackToDrivers = theDriverslList.Where(f => !f.Contains("DID" + inputDriverID)).ToList();
                                        addAllOtherEntriesBackToDrivers.Add(j);
                                        File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBackToDrivers);

                                        Console.WriteLine($"You were added the the existing carpool created by driver {"DID" + inputDriverID}.");
                                    }
                                    else
                                    {
                                        File.AppendAllText(pathFileCarpools, ("\nDID" + inputDriverID + ",PID" + inputPassengerID));
                                        Console.WriteLine($"A new carpool was created by driver {"DID" + inputDriverID} and {"PID" + inputPassengerID} as passenger.");

                                        //changing the free seats fo freeseats-1
                                        List<string> theDriverslList = File.ReadAllLines(pathFileDrivers).ToList();
                                        string j = $"{"DID" + inputDriverID},{numberOfFreeSeats - 1},{splittedLinesInDriversArray[2]},{splittedLinesInDriversArray[3]},{splittedLinesInDriversArray[4]},{splittedLinesInDriversArray[5]}";
                                        var addAllOtherEntriesBackToDrivers = theDriverslList.Where(f => !f.Contains("DID" + inputDriverID)).ToList();
                                        addAllOtherEntriesBackToDrivers.Add(j);
                                        File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBackToDrivers);

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This Driver ID does not exist.");
                    Console.ResetColor();
                }


            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This Passenger ID does not exist.");
                Console.ResetColor();

            }
            Console.ReadLine();
        }


        public void OfferCarpoolToPassenger()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Offer a ride (add your driver ID to an existing passenger ID)");
            Console.ResetColor();

            string[] linesInCarpool = File.ReadAllLines(pathFileCarpools);


            //asking for the driver ID and checking if the ID exists in the drivers list
            Console.Write("Enter your driver ID (DID): ");
            string inputDriverID = Console.ReadLine();
            bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains("DID" + inputDriverID));


            if (ckeckInputDriverID)
            {

                //asking for the passenger ID and checking if the ID exists in the passenger list
                Console.Write("Enter the passenger ID for whom you are offering the ride (PID): ");
                string inputPassengerID = Console.ReadLine();
                bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains("PID" + inputPassengerID));

                bool checkBothDriverAndPassenger = false;

                checkBothDriverAndPassenger = CheckBothExistance(linesInCarpool, inputDriverID, inputPassengerID, checkBothDriverAndPassenger);



                if (ckeckInputPassengerID)
                {

                    string[] linesInDrivers = File.ReadAllLines(pathFileDrivers);

                    bool checkIfDriverHasACarpool = File.ReadLines(pathFileCarpools).Any(line => line.Contains("DID" + inputDriverID));

                    foreach (string line in linesInDrivers)
                    {
                        string[] splittedLinesInDriversArray = line.Split(',');
                        if (splittedLinesInDriversArray[0].Equals("DID" + inputDriverID))
                        {
                            int numberOfFreeSeats = Convert.ToInt32(splittedLinesInDriversArray[1]);

                            if (checkBothDriverAndPassenger)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"User {"PID" + inputPassengerID} is allready a member of the carpool {"DID" + inputDriverID}. Choose another user!");
                                Console.ResetColor();
                            }
                            else
                            {
                                //if there are NO free seats remaining inform the pasenger that the carpool is full and no seat can be taken
                                if (numberOfFreeSeats == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Driver {"DID" + inputDriverID} can't accept any new passengers since he has NO free seats available. Choose another carpool!");
                                    Console.ResetColor();
                                }
                                //if there is at least one free seat, take it and change the available seats to seats - 1
                                else
                                {

                                    if (checkIfDriverHasACarpool)
                                    {
                                        //adding the new passenger to the carpool
                                        List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();
                                        var findDriverInCarpool = theCarpoolList.Where(e => e.Contains("DID" + inputDriverID)).Select(e => e + ",PID" + inputPassengerID).ToList();
                                        var addAllOtherEntriesBack = theCarpoolList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                                        findDriverInCarpool.AddRange(addAllOtherEntriesBack);
                                        File.WriteAllLines(pathFileCarpools, findDriverInCarpool);

                                        //changing the free seats fo freeseats-1
                                        RemoveFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);

                                        Console.WriteLine($"You were added the the existing carpool created by driver {"DID" + inputDriverID}.");
                                    }
                                    else
                                    {
                                        File.AppendAllText(pathFileCarpools, ("\nDID" + inputDriverID + ",PID" + inputPassengerID));
                                        Console.WriteLine($"A new carpool was created by driver {"DID" + inputDriverID} and {"PID" + inputPassengerID} as passenger.");

                                        //changing the free seats fo freeseats-1
                                        RemoveFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This Passenger ID does not exist.");
                    Console.ResetColor();
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This Driver ID does not exist.");
                Console.ResetColor();
            }
            Console.ReadLine();
        }

        private void RemoveFreeSeat(string inputDriverID, string[] splittedLinesInDriversArray, int numberOfFreeSeats)
        {
            List<string> theDriverslList = File.ReadAllLines(pathFileDrivers).ToList();
            string j = $"{"DID" + inputDriverID},{numberOfFreeSeats - 1},{splittedLinesInDriversArray[2]},{splittedLinesInDriversArray[3]},{splittedLinesInDriversArray[4]},{splittedLinesInDriversArray[5]}";
            var addAllOtherEntriesBackToDrivers = theDriverslList.Where(f => !f.Contains("DID" + inputDriverID)).ToList();
            addAllOtherEntriesBackToDrivers.Add(j);
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBackToDrivers);
        }

        public void RemovePassengerFromCarpool()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Remove a passenger from a carpool");
            Console.ResetColor();

            string[] linesInCarpool = File.ReadAllLines(pathFileCarpools);

            //asking for the driver ID and checking if the ID exists in the drivers list
            Console.Write("Enter the carpool (driver ID (DID)): ");
            string inputDriverID = Console.ReadLine();
            bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains("DID" + inputDriverID));


            if (ckeckInputDriverID)
            {

                //asking for the passenger ID and checking if the ID exists in the passenger list
                Console.Write("Enter the passenger to be removed from the ride (PID): ");
                string inputPassengerID = Console.ReadLine();
                bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains("PID" + inputPassengerID));

                bool checkBothDriverAndPassenger = false;
                checkBothDriverAndPassenger = CheckBothExistance(linesInCarpool, inputDriverID, inputPassengerID, checkBothDriverAndPassenger);

                if (ckeckInputPassengerID)
                {

                    string[] linesInDrivers = File.ReadAllLines(pathFileDrivers);

                    bool checkIfDriverHasACarpool = File.ReadLines(pathFileCarpools).Any(line => line.Contains("DID" + inputDriverID));

                    foreach (string line in linesInDrivers)
                    {
                        string[] splittedLinesInDriversArray = line.Split(',');
                        if (splittedLinesInDriversArray[0].Equals("DID" + inputDriverID))
                        {
                            int numberOfFreeSeats = Convert.ToInt32(splittedLinesInDriversArray[1]);

                            if (checkBothDriverAndPassenger)
                            {
                                //removing the new passenger to the carpool
                                RemovePassengerByPassengerID(inputDriverID, inputPassengerID);

                                //changing the free seats fo freeseats+1
                                AddFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);

                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The passenger does not belong to the carpool. ");
                                Console.ResetColor();
                            }
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This Passenger ID does not exist.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This Driver ID does not exist.");
                Console.ResetColor();
            }
            Console.ReadLine();
        }

        private static bool CheckBothExistance(string[] linesInCarpool, string inputDriverID, string inputPassengerID, bool checkBothDriverAndPassenger)
        {
            foreach (string driverDuo in linesInCarpool)
            {
                string[] splittetArrayDuo = driverDuo.Split(',');
                for (int i = 0; i < splittetArrayDuo.Length - 1; i++)
                {
                    if (splittetArrayDuo[0].Equals("DID" + inputDriverID) && splittetArrayDuo[i + 1].Equals("PID" + inputPassengerID))
                    {
                        checkBothDriverAndPassenger = true;
                    }
                }
            }

            return checkBothDriverAndPassenger;
        }

        private void AddFreeSeat(string inputDriverID, string[] splittedLinesInDriversArray, int numberOfFreeSeats)
        {
            List<string> theDriverslList = File.ReadAllLines(pathFileDrivers).ToList();
            string j = $"{"DID" + inputDriverID},{numberOfFreeSeats + 1},{splittedLinesInDriversArray[2]},{splittedLinesInDriversArray[3]},{splittedLinesInDriversArray[4]},{splittedLinesInDriversArray[5]}";
            var addAllOtherEntriesBackToDrivers = theDriverslList.Where(f => !f.Contains("DID" + inputDriverID)).ToList();
            addAllOtherEntriesBackToDrivers.Add(j);
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBackToDrivers);
        }

        private void RemovePassengerByPassengerID(string inputDriverID, string inputPassengerID)
        {
            List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();


            var findDriverInCarpool = theCarpoolList.FirstOrDefault(e => e.Contains("DID" + inputDriverID) && e.Contains("PID" + inputPassengerID));
            string[] newArray = findDriverInCarpool.Split(',');
            var foo = new List<string>();

            if (newArray.Length != 2)
            {
                for (int i = 1; i < newArray.Length; i++)
                {
                    if (newArray[i] != "PID" + inputPassengerID)
                        foo.Add(newArray[i]);
                }
                var result = string.Join(",", foo.ToArray());
                var finalResult = newArray[0] + "," + result;

                var addAllOtherEntriesBack = theCarpoolList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                addAllOtherEntriesBack.Add(finalResult);
                File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBack);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Passenger was removed from the carpool.");
                Console.ResetColor();

            }
            else
            {

                var addAllOtherEntriesBack = theCarpoolList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBack);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Passenger was removed from the carpool and since it was the only passenger, the carpool was dissolved.");
                Console.ResetColor();
            }


        }

        public void SearchCarpoolStartDestination()
        {

            //asking for the passenger starting location/city
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Searching for a driver based on your start point and destination");
            Console.ResetColor();

            Console.Write("Where (city) do you want to be picked up from: ");
            string inputStartLocation = Console.ReadLine();

            //asking for the passenger destination city
            Console.Write("What is your destination: ");
            string inputDestination = Console.ReadLine();

            string[] theDriverslList = File.ReadAllLines(pathFileDrivers);

            List<string> trefferBothWays = new List<string>();
            List<string> trefferStart = new List<string>();
            List<string> trefferDestination = new List<string>();

            foreach (string driverCheck in theDriverslList)
            {
                string[] splittetArray = driverCheck.Split(',');

                if (splittetArray[4].Equals(inputStartLocation) && splittetArray[5].Equals(inputDestination))
                {
                    trefferBothWays.Add(splittetArray[0] + "," + splittetArray[1] + "," + splittetArray[2] + "," + splittetArray[3] + "," + splittetArray[4] + "," + splittetArray[5]);
                }

                if (splittetArray[4].Equals(inputStartLocation) && !splittetArray[5].Equals(inputDestination))
                {
                    trefferStart.Add(splittetArray[0] + "," + splittetArray[1] + "," + splittetArray[2] + "," + splittetArray[3] + "," + splittetArray[4] + "," + splittetArray[5]);
                }

                if (!splittetArray[4].Equals(inputStartLocation) && splittetArray[5].Equals(inputDestination))
                {
                    trefferDestination.Add(splittetArray[0] + "," + splittetArray[1] + "," + splittetArray[2] + "," + splittetArray[3] + "," + splittetArray[4] + "," + splittetArray[5]);
                }

            }


            if (trefferBothWays.Count > 0)
            {
                // string[] theDriverslList = File.ReadAllLines(pathFileDrivers);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nThe following drivers are starting at {inputStartLocation} and have {inputDestination} as destination:\n");
                Console.ResetColor();

                foreach (string passengerDuo in trefferBothWays)
                {
                    string[] splittetArray = passengerDuo.Split(',');
                    if (Convert.ToInt32(splittetArray[1]) > 0)
                    {
                        Console.WriteLine($"{splittetArray[2]} has {splittetArray[1]} free places available and is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]}. His driver ID is: {splittetArray[0]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"{splittetArray[2]} is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]} but has NO free places.");
                        Console.ResetColor();
                    }
                }

            }

            if (trefferStart.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nThe following drivers are starting at {inputStartLocation} but have a different destination than {inputDestination}:\n");
                Console.ResetColor();

                foreach (string passengerStart in trefferStart)
                {
                    string[] splittetArray = passengerStart.Split(',');
                    Console.WriteLine($"{splittetArray[2]} has {splittetArray[1]} free places available and is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]}. His driver ID is: {splittetArray[0]}");

                }
            }

            if (trefferDestination.Count > 0)
            {
                string[] thePassengerslListEnd = File.ReadAllLines(pathFilePassengers);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nThe following drivers are going to {inputDestination} but have a different start point than {inputStartLocation}:\n");
                Console.ResetColor();


                foreach (string passengerEnd in trefferDestination)
                {
                    string[] splittetArray = passengerEnd.Split(',');
                    Console.WriteLine($"{splittetArray[2]} has {splittetArray[1]} free places available and is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]}. His driver ID is: {splittetArray[0]}");
                }
            }

            if (trefferBothWays.Count == 0 && trefferStart.Count == 0 & trefferDestination.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No drivers were found matching your start location as: {inputStartLocation} and destination as: {inputDestination}.");
                Console.ResetColor();
            }

            Console.ReadLine();
                    }

        public void SearchPassengerStartDestination()
        {
            //asking for the passenger starting location/city
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Searching for a passenger based on your start point and destination");
            Console.ResetColor();

            Console.Write("Passenger's start location (city): ");
            string inputStartLocation = Console.ReadLine();

            //asking for the passenger destination city
            Console.Write("Passenger's destination: ");
            string inputDestination = Console.ReadLine();

            string[] thePassengerlList = File.ReadAllLines(pathFilePassengers);

            List<string> trefferBothWays = new List<string>();
            List<string> trefferStart = new List<string>();
            List<string> trefferDestination = new List<string>();

            foreach (string passengerCheck in thePassengerlList)
            {
                string[] splittetArray = passengerCheck.Split(',');

                if (splittetArray[2].Equals(inputStartLocation) && splittetArray[3].Equals(inputDestination))
                {
                    trefferBothWays.Add(splittetArray[0] + "," + splittetArray[1] + "," + splittetArray[2] + "," + splittetArray[3]);
                }

                if (splittetArray[2].Equals(inputStartLocation) && !splittetArray[3].Equals(inputDestination))
                {
                    trefferStart.Add(splittetArray[0] + "," + splittetArray[1] + "," + splittetArray[2] + "," + splittetArray[3]);
                }

                if (!splittetArray[2].Equals(inputStartLocation) && splittetArray[3].Equals(inputDestination))
                {
                    trefferDestination.Add(splittetArray[0] + "," + splittetArray[1] + "," + splittetArray[2] + "," + splittetArray[3]);
                }

            }


            if (trefferBothWays.Count > 0)
            {
                // string[] theDriverslList = File.ReadAllLines(pathFileDrivers);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nThe following passengers want a ride from {inputStartLocation} with {inputDestination} as destination:\n");
                Console.ResetColor();

                foreach (string passengerDuo in trefferBothWays)
                {
                    string[] splittetArray = passengerDuo.Split(',');
                    Console.WriteLine($"{splittetArray[1]} wants to ride from {splittetArray[2]} to {splittetArray[3]}. User ID: {splittetArray[0]}");

                }

            }

            if (trefferStart.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nThe following passengers want a ride from {inputStartLocation} but have a different destination than {inputDestination}:\n");
                Console.ResetColor();

                foreach (string passengerStart in trefferStart)
                {
                    string[] splittetArray = passengerStart.Split(',');
                    Console.WriteLine($"{splittetArray[1]} wants to ride from {splittetArray[2]} to {splittetArray[3]}. User ID: {splittetArray[0]}");

                }
            }

            if (trefferDestination.Count > 0)
            {
                string[] thePassengerslListEnd = File.ReadAllLines(pathFilePassengers);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nThe following passengers are looking for a ride to {inputDestination} but have a different pick up point than {inputStartLocation}:\n");
                Console.ResetColor();


                foreach (string passengerEnd in trefferDestination)
                {
                    string[] splittetArray = passengerEnd.Split(',');

                    Console.WriteLine($"{splittetArray[1]} wants to ride from {splittetArray[2]} to {splittetArray[3]}. User ID: {splittetArray[0]}");
                }
            }

            if (trefferBothWays.Count == 0 && trefferStart.Count == 0 & trefferDestination.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No passengers were found matching your start location as: {inputStartLocation} and destination as: {inputDestination}.");
                Console.ResetColor();
            }


            Console.ReadLine();

        }


    }
}





