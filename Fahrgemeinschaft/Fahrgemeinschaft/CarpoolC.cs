using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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

            bool AddLoop = false;
            do
            {
                //asking for the passenger ID and checking if the ID exists in the passenger list
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("==================================================================");
                Console.WriteLine("| Take a ride (add your passenger PID to an existing driver DID) |");
                Console.WriteLine("==================================================================");
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
                    checkBothDriverAndPassenger = SMCheckBothExistance(linesInCarpool, inputDriverID, inputPassengerID, checkBothDriverAndPassenger);

                    //reading the passenger id and then adding his start point to the string to be compared later
                    //reading the passenger id and then adding his destination to the string to be compared later
                    string passStart = "";
                    string passDest = "";
                    string[] thePassengerList = File.ReadAllLines(pathFilePassengers);
                    foreach (string line in thePassengerList)
                    {
                        string[] linesInPassengerArray = line.Split(',');
                        if (linesInPassengerArray[0] == ("PID" + inputPassengerID))
                        {
                            passStart = linesInPassengerArray[2];
                            passDest = linesInPassengerArray[3];

                        }

                    }

                    //reading the driver id and then adding his start point to the string to be compared later
                    //reading the driver id and then adding his destination to the string to be compared later
                    string driveStart = "";
                    string driveDest = "";
                    string[] theDriversList = File.ReadAllLines(pathFileDrivers);
                    foreach (string line in theDriversList)
                    {
                        string[] linesInDriversArray = line.Split(',');
                        if (linesInDriversArray[0] == ("DID" + inputDriverID))
                        {
                            driveStart = linesInDriversArray[4];
                            driveDest = linesInDriversArray[5];

                        }

                    }


                    bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains("DID" + inputDriverID));
                    if (ckeckInputDriverID)
                    {

                        if (passStart == driveStart || passDest == driveDest)
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
                                                SMAddPassengerToCarpool(inputPassengerID, inputDriverID);

                                                //changing the free seats fo freeseats-1
                                                SMRemoveFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);


                                                Console.WriteLine($"You were added to the existing carpool created by driver {"DID" + inputDriverID}.");
                                                Console.ReadLine();
                                                AddLoop = false;

                                            }
                                            else
                                            {
                                                File.AppendAllText(pathFileCarpools, ("\nDID" + inputDriverID + ",PID" + inputPassengerID));
                                                Console.WriteLine($"A new carpool was created by driver {"DID" + inputDriverID} and {"PID" + inputPassengerID} as passenger.");

                                                //changing the free seats fo freeseats-1
                                                SMRemoveFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);
                                                Console.ReadLine();
                                                AddLoop = false;


                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The driver and passenger should have at least the start point or destination common.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This Driver ID does not exist.");
                        Console.ResetColor();
                        Console.ReadLine();
                        AddLoop = true;

                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This Passenger ID does not exist.");
                    Console.ResetColor();
                    bool choiceUserNew = false;

                    do
                    {
                        Console.WriteLine("\nWould you like to register a new passenger? (y/n)");
                        ConsoleKeyInfo newUserChoice = Console.ReadKey();


                        if (Convert.ToString(newUserChoice.KeyChar) == "y")
                        {
                            UPassengers passengers = new UPassengers();
                            passengers.AddPassenger();
                            AddLoop = true;
                            choiceUserNew = false;
                        }
                        else if (Convert.ToString(newUserChoice.KeyChar) == "n")
                        {
                            choiceUserNew = false;
                            AddLoop = false;
                        }
                        else if (Convert.ToString(newUserChoice.KeyChar) != "y" && Convert.ToString(newUserChoice) != "n")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nWould you like to try again, this time with your brain switched on before typing?");
                            Console.ResetColor();
                            choiceUserNew = true;
                        }

                    } while (choiceUserNew);
                }
            } while (AddLoop);
            // Console.ReadLine();
        }

        public void OfferCarpoolToPassenger()
        {
            bool AddLoop = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=================================================================");
                Console.WriteLine("| Offer a ride (add your driver ID to an existing passenger ID) |");
                Console.WriteLine("=================================================================");
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

                    checkBothDriverAndPassenger = SMCheckBothExistance(linesInCarpool, inputDriverID, inputPassengerID, checkBothDriverAndPassenger);


                    //reading the passenger id and then adding his start point to the string to be compared later
                    //reading the passenger id and then adding his destination to the string to be compared later
                    string passStart = "";
                    string passDest = "";
                    string[] thePassengerList = File.ReadAllLines(pathFilePassengers);
                    foreach (string line in thePassengerList)
                    {
                        string[] linesInPassengerArray = line.Split(',');
                        if (linesInPassengerArray[0] == ("PID" + inputPassengerID))
                        {
                            passStart = linesInPassengerArray[2];
                            passDest = linesInPassengerArray[3];

                        }

                    }

                    //reading the driver id and then adding his start point to the string to be compared later
                    //reading the driver id and then adding his destination to the string to be compared later
                    string driveStart = "";
                    string driveDest = "";
                    string[] theDriversList = File.ReadAllLines(pathFileDrivers);
                    foreach (string line in theDriversList)
                    {
                        string[] linesInDriversArray = line.Split(',');
                        if (linesInDriversArray[0] == ("DID" + inputDriverID))
                        {
                            driveStart = linesInDriversArray[4];
                            driveDest = linesInDriversArray[5];

                        }

                    }


                    if (ckeckInputPassengerID)
                    {
                        if (passStart == driveStart || passDest == driveDest)
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
                                                SMAddPassengerToCarpool(inputPassengerID, inputDriverID);

                                                //changing the free seats fo freeseats-1
                                                SMRemoveFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);

                                                Console.WriteLine($"You were added the the existing carpool created by driver {"DID" + inputDriverID}.");
                                                Console.ReadLine();
                                                AddLoop = false;

                                            }
                                            else
                                            {
                                                File.AppendAllText(pathFileCarpools, ("\nDID" + inputDriverID + ",PID" + inputPassengerID));
                                                Console.WriteLine($"A new carpool was created by driver {"DID" + inputDriverID} and {"PID" + inputPassengerID} as passenger.");

                                                //changing the free seats fo freeseats-1
                                                SMRemoveFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);
                                                Console.ReadLine();
                                                AddLoop = false;

                                            }
                                        }
                                    }
                                }

                            }

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The driver and passenger should have at least the start point or destination common.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This Passenger ID does not exist.");
                        Console.ResetColor();
                        Console.ReadLine();
                        AddLoop = true;

                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This Driver ID does not exist.");
                    Console.ResetColor();
                    bool choiceUserNew = false;

                    do
                    {
                        Console.WriteLine("\nWould you like to register as a new driver? (y/n)");
                        ConsoleKeyInfo newUserChoice = Console.ReadKey();


                        if (Convert.ToString(newUserChoice.KeyChar) == "y")
                        {
                            UDrivers drivers = new UDrivers();
                            drivers.AddDriver();
                            AddLoop = true;
                            choiceUserNew = true;
                        }
                        else if (Convert.ToString(newUserChoice.KeyChar) == "n")
                        {
                            AddLoop = false;
                            choiceUserNew = false;
                        }
                        else if (Convert.ToString(newUserChoice.KeyChar) != "y" && Convert.ToString(newUserChoice) != "n")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nWould you like to try again, this time with your brain switched on before typing?");
                            Console.ResetColor();
                            choiceUserNew = true;
                        }

                    } while (choiceUserNew);
                }
            } while (AddLoop);

            // Console.ReadLine();
        }

        public void SearchCarpoolStartDestination()
        {

            //asking for the passenger starting location/city
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================================================");
            Console.WriteLine("| Searching for a driver based on your start point and destination |");
            Console.WriteLine("====================================================================");
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
            Console.WriteLine("=======================================================================");
            Console.WriteLine("| Searching for a passenger based on your start point and destination |");
            Console.WriteLine("=======================================================================");
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

        public void ListAllCarpools()
        {
            string[] theCarpoolList = File.ReadAllLines(pathFileCarpools);
            string[] thePassengerList = File.ReadAllLines(pathFilePassengers);
            string[] theDriversList = File.ReadAllLines(pathFileDrivers);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"==============================================");
            Console.WriteLine($"| The entire list with the current carpools: |");
            Console.WriteLine($"==============================================");
            Console.ResetColor();

            foreach (string line in theCarpoolList)
            {
                string[] linesInCarpoolArray = line.Split(',');

                //The driver
                string[] splitCurrentDriverDetails = SMFindCurrentUserInCarpool(theDriversList, linesInCarpoolArray, 0);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n============================================================================================");
                Console.WriteLine($"\tCarpool {splitCurrentDriverDetails[0]}");
                Console.WriteLine($"============================================================================================");
                Console.ResetColor();
                Console.WriteLine($"The driver of this carpool is {splitCurrentDriverDetails[2]} and he owns a {splitCurrentDriverDetails[3]} with {splitCurrentDriverDetails[1]} free places. " +
                    $"\nThe start point is {splitCurrentDriverDetails[4]} and the destination is {splitCurrentDriverDetails[5]}.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"The following passengers are present:");
                Console.ResetColor();


                for (int i = 1; i < linesInCarpoolArray.Length; i++)
                {
                    //The passengers
                    string[] splitCurrentPassengerDetails = SMFindCurrentUserInCarpool(thePassengerList, linesInCarpoolArray, i);

                    Console.Write($"\n{splitCurrentPassengerDetails[1]}, with ID {splitCurrentPassengerDetails[0]}, riding along from {splitCurrentPassengerDetails[2]} to {splitCurrentPassengerDetails[3]}.");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        public void RemovePassengerFromCarpool()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=====================================");
            Console.WriteLine("| Remove a passenger from a carpool |");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            string[] linesInCarpool = File.ReadAllLines(pathFileCarpools);

            //asking for the driver ID and checking if the ID exists in the drivers list
            Console.Write("Enter the carpool (driver ID (DID)): ");
            string inputDriverID = Console.ReadLine();
            bool ckeckInputDriverID = File.ReadLines(pathFileCarpools).Any(line => line.Contains("DID" + inputDriverID));


            if (ckeckInputDriverID)
            {

                //asking for the passenger ID and checking if the ID exists in the passenger list
                Console.Write("Enter the passenger to be removed from the ride (PID): ");
                string inputPassengerID = Console.ReadLine();
                bool ckeckInputPassengerID = File.ReadLines(pathFileCarpools).Any(line => line.Contains("PID" + inputPassengerID));

                bool checkBothDriverAndPassenger = false;
                checkBothDriverAndPassenger = SMCheckBothExistance(linesInCarpool, inputDriverID, inputPassengerID, checkBothDriverAndPassenger);

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
                                //removing the new passenger from the carpool
                                SMRemovePassengerInCarpoolByDriverIDandPassengerID(inputDriverID, inputPassengerID);

                                //changing the free seats fo freeseats+1
                                SMAddFreeSeat(inputDriverID, splittedLinesInDriversArray, numberOfFreeSeats);

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

        /* public void RemovePassengerAccount()
         {
             Console.Clear();
             Console.ForegroundColor = ConsoleColor.Blue;
             Console.WriteLine("=====================================================================");
             Console.WriteLine("| Remove a passenger acount from passenger as well as carpool lists |");
             Console.WriteLine("=====================================================================");
             Console.ResetColor();

             //asking for the passenger ID
             Console.Write("Enter passenger ID (PID): ");
             string inputPassengerID = Console.ReadLine();
             SMRemovePassengerAccountByPassengerID(inputPassengerID);

         }*/

        public void RemoveDriverAccount()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==============================================================");
            Console.WriteLine("| Remove a driver acount from both drivers and carpool lists |");
            Console.WriteLine("==============================================================");
            Console.ResetColor();

            //asking for the driver ID
            Console.Write("Enter driver ID (DID): ");
            string inputDriverID = Console.ReadLine();
            SMRemoveDriverAccountByDriverID(inputDriverID);

        }

        public void SMAddFreeSeat(string inputDriverID, string[] splittedLinesInDriversArray, int numberOfFreeSeats)
        {
            List<string> theDriverslList = File.ReadAllLines(pathFileDrivers).ToList();
            string j = $"{"DID" + inputDriverID},{numberOfFreeSeats + 1},{splittedLinesInDriversArray[2]},{splittedLinesInDriversArray[3]},{splittedLinesInDriversArray[4]},{splittedLinesInDriversArray[5]}";
            var addAllOtherEntriesBackToDrivers = theDriverslList.Where(f => !f.Contains("DID" + inputDriverID)).ToList();
            addAllOtherEntriesBackToDrivers.Add(j);
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBackToDrivers);
        }

        public void SMRemoveFreeSeat(string inputDriverID, string[] splittedLinesInDriversArray, int numberOfFreeSeats)
        {
            List<string> theDriverslList = File.ReadAllLines(pathFileDrivers).ToList();
            string j = $"{"DID" + inputDriverID},{numberOfFreeSeats - 1},{splittedLinesInDriversArray[2]},{splittedLinesInDriversArray[3]},{splittedLinesInDriversArray[4]},{splittedLinesInDriversArray[5]}";
            var addAllOtherEntriesBackToDrivers = theDriverslList.Where(f => !f.Contains("DID" + inputDriverID)).ToList();
            addAllOtherEntriesBackToDrivers.Add(j);
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBackToDrivers);
        }

        public void SMAddPassengerToCarpool(string inputPassengerID, string inputDriverID)
        {
            List<string> theCarpoolToList = File.ReadAllLines(pathFileCarpools).ToList();
            var findDriverInCarpool = theCarpoolToList.Where(e => e.Contains("DID" + inputDriverID)).Select(e => e + ",PID" + inputPassengerID).ToList();
            var addAllOtherEntriesBack = theCarpoolToList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            findDriverInCarpool.AddRange(addAllOtherEntriesBack);
            File.WriteAllLines(pathFileCarpools, findDriverInCarpool);
        }

        public static bool SMCheckBothExistance(string[] linesInCarpool, string inputDriverID, string inputPassengerID, bool checkBothDriverAndPassenger)
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

        public void SMRemovePassengerInCarpoolByDriverIDandPassengerID(string inputDriverID, string inputPassengerID)
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

        public void SMRemoveDriverAccountByDriverID(string inputDriverID)
        {

            //removing a driver from the drivers list
            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();

            var findDriverInDrivers = theDriversList.FirstOrDefault(e => e.Contains("DID" + inputDriverID));

            bool exists = false;
            bool existsInCarpool = false;

            foreach (var driver in theDriversList)
            {
                string[] strings = driver.Split(',');
                if (strings[0] == ("DID" + inputDriverID))
                {
                    exists = true;
                }
            }


            if (exists)
            {

                var addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Driver DID{inputDriverID} was removed from the registered drivers list.");
                Console.ResetColor();

                //removing a driver from the carpool list
                List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();

                foreach (var driver in theCarpoolList)
                {
                    string[] strings = driver.Split(',');
                    if (strings[0] == ("DID" + inputDriverID))
                    {
                        existsInCarpool = true;
                    }
                }
                if (existsInCarpool)
                {
                    var findDriverInCarpools = theCarpoolList.FirstOrDefault(e => e.Contains("DID" + inputDriverID));

                    string[] arrayWithSplittedCarpoolLines = findDriverInCarpools.Split(',');

                    var addAllOtherEntriesBackToCarpoolList = theCarpoolList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
                    File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBackToCarpoolList);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Driver DID{inputDriverID} was also removed from the carpool list and since the carpool was dissolved all his passengers were removed as well.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("The driver DID doesn't have any carpools created, the carpool list remains untouched.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The driver DID does not exist, account can't be deleted.");
                Console.ResetColor();
            }

            Console.ReadLine();



        }

        public static string[] SMFindCurrentUserInCarpool(string[] userList, string[] linesInCarpoolArray, int position)
        {
            string currentUser = "";
            foreach (var userLine in userList)
            {
                if (userLine.Contains(linesInCarpoolArray[position]))
                {
                    currentUser = userLine;
                    break;
                }
            }
            string[] splitCurrentDriverDetails = currentUser.Split(',');
            return splitCurrentDriverDetails;
        }


    }
}





