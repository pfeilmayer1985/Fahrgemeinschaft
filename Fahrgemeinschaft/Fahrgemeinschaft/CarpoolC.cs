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
            Console.WriteLine("Take a ride (add your passenger PID to an existing driver DID)");
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
                                Console.WriteLine($"User {"PID" + inputPassengerID} is allready a member of the carpool {"DID" + inputDriverID}. Choose another carpool!");

                            }
                            else
                            {

                                //if there are NO free seats remaining inform the pasenger that the carpool is full and no seat can be taken
                                if (numberOfFreeSeats == 0)
                                {
                                    Console.WriteLine($"Unfortunately you can't be added to the carpool because there are NO free seats available.");

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
                    Console.WriteLine("This Driver ID does not exist.");
                }


            }
            else
            {
                Console.WriteLine("This Passenger ID does not exist.");

            }
            Console.ReadLine();
        }


        public void OfferCarpoolToPassenger()
        {
            Console.Clear();
            Console.WriteLine("Offer a ride (add your driver ID to an existing passenger ID)");
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
                                Console.WriteLine($"User {"PID" + inputPassengerID} is allready a member of the carpool {"DID" + inputDriverID}. Choose another user!");

                            }
                            else
                            {
                                //if there are NO free seats remaining inform the pasenger that the carpool is full and no seat can be taken
                                if (numberOfFreeSeats == 0)
                                {
                                    Console.WriteLine($"Driver {"DID" + inputDriverID} can't accept any new passengers since he has NO free seats available. Choose another carpool!");

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
                    Console.WriteLine("This Passenger ID does not exist.");
                }

            }
            else
            {
                Console.WriteLine("This Driver ID does not exist.");

            }
            Console.ReadLine();
        }


        public void SearchCarpoolStartDestination()
        {
            //asking for the passenger starting location/city
            Console.Clear();
            Console.WriteLine("Searching for a driver based on your start point and destination");
            Console.Write("Where (city) do you want to be picked up from: ");
            string inputStartLocation = Console.ReadLine();

            string[] theDriverslList = File.ReadAllLines(pathFileDrivers);

            bool ckeckinputStartLocation = false;

            foreach (string driversCheck in theDriverslList)
            {
                string[] splittetArrayDrivers = driversCheck.Split(',');

                if (splittetArrayDrivers[4].Equals(inputStartLocation))
                {
                    ckeckinputStartLocation = true;
                }

            }


            //asking for the passenger destination city
            Console.Write("What is your destination: ");
            string inputDestination = Console.ReadLine();

            bool ckeckinputDestination = false;

            foreach (string driversCheck in theDriverslList)
            {
                string[] splittetArrayDriversEnd = driversCheck.Split(',');

                if (splittetArrayDriversEnd[5].Equals(inputDestination))
                {
                    ckeckinputDestination = true;
                }

            }

            bool checkBoth = false;

            foreach (string driverDuo in theDriverslList)
            {
                string[] splittetArrayDuo = driverDuo.Split(',');

                if (splittetArrayDuo[4].Equals(inputStartLocation) && splittetArrayDuo[5].Equals(inputDestination))
                {
                    checkBoth = true;
                }
            }


            if (checkBoth)
            {
                // string[] theDriverslList = File.ReadAllLines(pathFileDrivers);

                Console.WriteLine($"\nThe following drivers are starting at {inputStartLocation} and have {inputDestination} as destination:\n");
                foreach (string driver in theDriverslList)
                {
                    string[] splittetArray = driver.Split(',');

                    if (splittetArray[4].Equals(inputStartLocation) && splittetArray[5].Equals(inputDestination))
                    {
                        if (Convert.ToInt32(splittetArray[1]) > 0)
                        {
                            Console.WriteLine($"{splittetArray[2]} has {splittetArray[1]} free places available and is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]}. His driver ID is: {splittetArray[0]}");
                        }
                        else
                        {
                            Console.WriteLine($"{splittetArray[2]} is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]} but has NO free places.");
                        }
                    }
                }

            }

            if (ckeckinputStartLocation && !checkBoth)
            {
                Console.WriteLine($"\nThe following drivers are starting at {inputStartLocation} but have a different destination than {inputDestination}:\n");
                string[] theDriverslListStartOne = File.ReadAllLines(pathFileDrivers);
                foreach (string driver in theDriverslListStartOne)
                {
                    string[] splittetArray = driver.Split(',');
                    if (splittetArray[4].Equals(inputStartLocation))
                    {
                        Console.WriteLine($"{splittetArray[2]} has {splittetArray[1]} free places available and is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]}. His driver ID is: {splittetArray[0]}");
                    }
                }
            }


            if (ckeckinputDestination && !checkBoth)
            {
                string[] theDriverslListEnd = File.ReadAllLines(pathFileDrivers);
                Console.WriteLine($"\nThe following drivers are going to {inputDestination} but have a different start point than {inputStartLocation}:\n");

                foreach (string driver in theDriverslListEnd)
                {
                    string[] splittetArray = driver.Split(',');


                    if (splittetArray[5].Equals(inputDestination))
                    {
                        Console.WriteLine($"{splittetArray[2]} has {splittetArray[1]} free places available and is driving a {splittetArray[3]} from {splittetArray[4]} to {splittetArray[5]}. His driver ID is: {splittetArray[0]}");
                    }
                }
            }


            if (!ckeckinputStartLocation && !ckeckinputDestination && !checkBoth)
            {
                Console.WriteLine($"No drivers were found matching your start location as: {inputStartLocation} and destination as: {inputDestination}.");
            }


            Console.ReadLine();

        }


        public void SearchPassengerStartDestination()
        {
            //asking for the passenger starting location/city
            Console.Clear();
            Console.WriteLine("Searching for a passenger based on your start point and destination");
            Console.Write("Passenger's start location (city): ");
            string inputStartLocation = Console.ReadLine();

            string[] thePassengerlList = File.ReadAllLines(pathFilePassengers);

            bool ckeckinputStartLocation = false;

            foreach (string passengerCheck in thePassengerlList)
            {
                string[] splittetArrayPassenger = passengerCheck.Split(',');

                if (splittetArrayPassenger[2].Equals(inputStartLocation))
                {
                    ckeckinputStartLocation = true;
                }

            }


            //asking for the passenger destination city
            Console.Write("Passenger's destination: ");
            string inputDestination = Console.ReadLine();

            bool ckeckinputDestination = false;

            foreach (string passengerCheck in thePassengerlList)
            {
                string[] splittetArrayPassengerEnd = passengerCheck.Split(',');

                if (splittetArrayPassengerEnd[3].Equals(inputDestination))
                {
                    ckeckinputDestination = true;
                }

            }

            bool checkBoth = false;

            foreach (string passengerDuo in thePassengerlList)
            {
                string[] splittetArrayDuo = passengerDuo.Split(',');

                if (splittetArrayDuo[2].Equals(inputStartLocation) && splittetArrayDuo[3].Equals(inputDestination))
                {
                    checkBoth = true;
                }
            }


            if (checkBoth)
            {
                // string[] theDriverslList = File.ReadAllLines(pathFileDrivers);

                Console.WriteLine($"\nThe following passengers want a ride from {inputStartLocation} with {inputDestination} as destination:\n");
                foreach (string passenger in thePassengerlList)
                {
                    string[] splittetArray = passenger.Split(',');

                    if (splittetArray[2].Equals(inputStartLocation) && splittetArray[3].Equals(inputDestination))
                    {
                        Console.WriteLine($"{splittetArray[1]} wants to ride from {splittetArray[2]} to {splittetArray[3]}. User ID: {splittetArray[0]}");
                    }
                }

            }

            if (ckeckinputStartLocation && !checkBoth)
            {
                Console.WriteLine($"\nThe following passengers want a ride from {inputStartLocation} but have a different destination than {inputDestination}:\n");
                string[] thePassengerslListStartOne = File.ReadAllLines(pathFilePassengers);
                foreach (string passenger in thePassengerslListStartOne)
                {
                    string[] splittetArray = passenger.Split(',');
                    if (splittetArray[2].Equals(inputStartLocation))
                    {
                        Console.WriteLine($"{splittetArray[1]} wants to ride from {splittetArray[2]} to {splittetArray[3]}. User ID: {splittetArray[0]}");
                    }
                }
            }


            if (ckeckinputDestination && !checkBoth)
            {
                string[] thePassengerslListEnd = File.ReadAllLines(pathFilePassengers);
                Console.WriteLine($"\nThe following passengers are looking for a ride to {inputDestination} but have a different pick up point than {inputStartLocation}:\n");

                foreach (string passenger in thePassengerslListEnd)
                {
                    string[] splittetArray = passenger.Split(',');


                    if (splittetArray[3].Equals(inputDestination))
                    {
                        Console.WriteLine($"{splittetArray[1]} wants to ride from {splittetArray[2]} to {splittetArray[3]}. User ID: {splittetArray[0]}");
                    }
                }
            }


            if (!ckeckinputStartLocation && !ckeckinputDestination && !checkBoth)
            {
                Console.WriteLine($"No passengers were found matching your start location as: {inputStartLocation} and destination as: {inputDestination}.");
            }


            Console.ReadLine();

        }

    }
}





