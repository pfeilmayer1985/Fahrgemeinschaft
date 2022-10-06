using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class UPassengers : Users
    {
        public string StartingCity { get; set; }
        public string Destination { get; set; }
        public int HowManyPassengers { get; set; }

        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";

        public List<UPassengers> PassengersList { get; set; }

        CarpoolC carpoolClass = new CarpoolC();

        public UPassengers(string iD, string name, string startCity, string destination)
        {
            ID = iD;
            Name = name;
            StartingCity = startCity;
            Destination = destination;
            HowManyPassengers = 1;
        }

        public UPassengers()
        {
            PassengersList = new List<UPassengers>();

        }

        public void AddPassenger()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("| You are now registering as a Passenger and adding a Carpool request to the market |");
            Console.WriteLine("=====================================================================================");
            Console.ResetColor();

            bool userInUse = false;
            string id;
            do
            {
                Console.Write("Choose your unique passenger ID (PID), 4 chars long: ");
                id = "PID" + Console.ReadLine();

                bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains(id));
                if (ckeckInputPassengerID || id.Length != 7)
                {
                    //asking for the driver ID and checking if the ID exists in the drivers list
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This ID is allready in use or you used more/less characters than allowed. Choose another ID!");
                    Console.ResetColor();
                    userInUse = true;
                }
                else
                {
                    userInUse = false;
                }

            } while (userInUse == true);

            Console.Write("What's your name?: ");
            string name = Console.ReadLine();
            Console.Write("Where do you want to be picked up (Departure City): ");
            string startCity = Console.ReadLine();
            Console.Write("Destination City: ");
            string destination = Console.ReadLine();
            File.AppendAllText(pathFilePassengers, ("\n" + id + "," + name + "," + startCity + "," + destination));
            Console.WriteLine($"\nThe new user ID {id} for {name} was successfully added to the list. You can now look for a carpool ride.");
            Console.ReadLine();
        }

        public void ManagePassengerAccount()
        {

        }

        public void RemovePassengerAccount()
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

        }

        private void SMRemovePassengerAccountByPassengerID(string inputPassengerID)
        {

            //removing a passenger from the passengers list
            List<string> thePassengersList = File.ReadAllLines(pathFilePassengers).ToList();

            var findPassengerInPassengers = thePassengersList.FirstOrDefault(e => e.Contains("PID" + inputPassengerID));

            bool exists = false;
            bool existsInCarpool = false;
            foreach (var passenger in thePassengersList)
            {
                string[] strings = passenger.Split(',');
                if (strings[0] == ("PID" + inputPassengerID))
                {
                    exists = true;
                }
            }

            if (exists)
            {
                var addAllOtherEntriesBack = thePassengersList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
                File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Passenger PID{inputPassengerID} was removed from the registered passengers list.");
                Console.ResetColor();

                //removing a passenger from the carpool list
                List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();



                foreach (var passenger in theCarpoolList)
                {
                    string[] strings = passenger.Split(',');
                    for (int i = 0; i < strings.Length; i++)
                    {
                        if (strings[i] == ("PID" + inputPassengerID))
                        {
                            existsInCarpool = true;
                        }
                    }
                }

                if (existsInCarpool)
                {

                    var findPassengerInCarpools = theCarpoolList.FirstOrDefault(e => e.Contains("PID" + inputPassengerID));

                    string[] arrayWithSplittedCarpoolLines = findPassengerInCarpools.Split(',');

                    var foo = new List<string>();

                    if (arrayWithSplittedCarpoolLines.Length != 2)
                    {
                        for (int i = 1; i < arrayWithSplittedCarpoolLines.Length; i++)
                        {
                            if (arrayWithSplittedCarpoolLines[i] != "PID" + inputPassengerID)
                                foo.Add(arrayWithSplittedCarpoolLines[i]);
                        }
                        var result = string.Join(",", foo.ToArray());
                        var finalResult = arrayWithSplittedCarpoolLines[0] + "," + result;

                        //find the driver
                        var findTheDriver = theCarpoolList.Where(e => e.Contains("PID" + inputPassengerID)).ToList();
                        foreach (string driver in findTheDriver)
                        {
                            string[] arrayWithDrivers = driver.Split(',');
                            string currentDriverInCarpool = arrayWithDrivers[0];

                            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();
                            foreach (string driverInDrivesList in theDriversList)
                            {
                                var findTheDriverAgain = theDriversList.FirstOrDefault(e => e.Contains(currentDriverInCarpool));

                                string[] findTheDriverAgainSplitted = findTheDriverAgain.Split(',');
                                string currentDriverInDrivers = findTheDriverAgainSplitted[0];

                                //var findTheDriverInDrivers = theCarpoolList.Where(e => e.Contains("PID" + inputPassengerID)).ToList();

                                int actualFreeSeats = Convert.ToInt32(findTheDriverAgainSplitted[1]);
                                carpoolClass.SMAddFreeSeat(currentDriverInDrivers.TrimStart(new char[] { 'D', 'I' }), findTheDriverAgainSplitted, actualFreeSeats);


                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"One free seat was returned to the driver {currentDriverInCarpool}.");
                            Console.ResetColor();
                        }


                        var addAllOtherEntriesBackToCarpoolList = theCarpoolList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
                        addAllOtherEntriesBackToCarpoolList.Add(finalResult);
                        File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBackToCarpoolList);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Passenger was entirely removed from the carpool.");
                        Console.ResetColor();

                    }
                    else
                    {

                        var addAllOtherEntriesBackToCarpoolList = theCarpoolList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
                        File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBackToCarpoolList);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Passenger was removed from the carpool and since it was the only passenger, the carpool was dissolved.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("The passenger PID isn't riding in any of the carpools, the carpool list remains untouched.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The passenger PID does not exist, account can't be deleted.");
                Console.ResetColor();
            }

            Console.ReadLine();



        }

        public void ListAllRequests()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("======================================");
            Console.WriteLine("| The available carpool requests are |");
            Console.WriteLine("======================================");
            Console.ResetColor();

            string[] showPassengerList = File.ReadAllLines(pathFilePassengers);

            int counter = 1;
            foreach (string passenger in showPassengerList)
            {
                string[] splittetPassengerArray = passenger.Split(',');
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n==================================================================");
                Console.ResetColor();
                Console.WriteLine($"{counter}.\t{splittetPassengerArray[1]} wants to go from {splittetPassengerArray[2]} to {splittetPassengerArray[3]}.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\tPassenger ID: {splittetPassengerArray[0]}");
                Console.ResetColor();

                counter++;

            }
        }


    }
}
