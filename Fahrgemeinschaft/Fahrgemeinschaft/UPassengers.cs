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

        public void SeePassenger()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=====================================");
            Console.WriteLine("| See your passenger acount details |");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            //asking for the passenger ID
            Console.Write("\nEnter passenger ID (PID): ");
            string inputPassengerID = Console.ReadLine();

            List<string> thePassengersList = File.ReadAllLines(pathFilePassengers).ToList();
            var findPassengerInPassengers = thePassengersList.Where(e => e.Contains("PID" + inputPassengerID)).ToList();

            bool exists = false;
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
                List<string> theEditedUserDetails = new List<string>();
                foreach (var passenger in thePassengersList)
                {
                    string[] position = passenger.Split(',');
                    if (position[0] == ("PID" + inputPassengerID))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("====================================================================");
                        Console.WriteLine($"| The following user informations are registered with your account |");
                        Console.WriteLine("====================================================================");
                        Console.ResetColor();

                        Console.WriteLine($"\nPassenger ID: \t\t\t\t{position[0]}");
                        Console.WriteLine($"Passenger name: \t\t\t{position[1]}");
                        Console.WriteLine($"Current pick-up location: \t\t{position[2]}");
                        Console.WriteLine($"Current registered destination: \t{position[3]}");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
                        Console.ResetColor();

                        Console.ReadLine();

                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The passenger PID does not exist, account content can't be retrieved.");
                Console.ResetColor();

                Console.ReadLine();
            }



        }

        public void ManagePassengerAccount()
        {
            bool userClassBool = true;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("================================");
            Console.WriteLine("| Manage your passenger acount |");
            Console.WriteLine("================================");
            Console.ResetColor();

            //asking for the passenger ID
            Console.Write("\nEnter passenger ID (PID): ");
            string inputPassengerID = Console.ReadLine();

            List<string> thePassengersList = File.ReadAllLines(pathFilePassengers).ToList();
            var findPassengerInPassengers = thePassengersList.Where(e => e.Contains("PID" + inputPassengerID)).ToList();

            bool exists = false;
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
                List<string> theEditedUserDetails = new List<string>();
                foreach (var passenger in thePassengersList)
                {
                    string[] position = passenger.Split(',');
                    if (position[0] == ("PID" + inputPassengerID))
                    {

                        do
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"=======================================================");
                            Console.WriteLine($"| The following user details are allowed to be edited |");
                            Console.WriteLine($"=======================================================");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\nThe User ID ({position[0]}) can't be changed.\nIf you want to change it, delete the Passenger and make a new account.");
                            Console.ResetColor();
                            Console.WriteLine($"\n( 1 )\tPassenger name: {position[1]}");
                            Console.WriteLine($"( 2 )\tCurrent pick-up location: {position[2]}");
                            Console.WriteLine($"( 3 )\tCurrent destination: {position[3]}");
                            Console.WriteLine($"( 4 )\tBoth pick-up location and destination");
                            Console.WriteLine($"( 5 )\tAll fields above (name, pickup & destination)");
                            
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\n( 9 )\tDon't perform any changes, return to previous menu");
                            Console.ResetColor();

                            int userChoice;
                            bool pressedRightKey = false;
                            do
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("\nChoose one of the options above: ");
                                Console.ResetColor();

                                //string userInput = Console.ReadLine();

                                ConsoleKeyInfo userInputKey = Console.ReadKey();
                                string userInput = Convert.ToString(userInputKey.KeyChar);

                                if (!int.TryParse(userInput, out userChoice))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Would you like to try again, this time with your brain switched on before typing?");
                                    Console.ResetColor();
                                    pressedRightKey = true;
                                }
                                else
                                {
                                    pressedRightKey = false;
                                    continue;
                                }
                            } while (pressedRightKey);

                            switch (userChoice)
                            {
                                case 1:
                                    string newUserName, editedPassenger;
                                    List<string> addAllOtherEntriesBack;
                                    EditPassengerName(inputPassengerID, thePassengersList, position, out newUserName, out editedPassenger, out addAllOtherEntriesBack);
                                    userClassBool = false;
                                    continue;
                                case 2:
                                    EditPassengerPickup(inputPassengerID, thePassengersList, position);
                                    userClassBool = false;

                                    continue;
                                case 3:
                                    EditPassengerDestination(inputPassengerID, thePassengersList, position, out editedPassenger, out addAllOtherEntriesBack);
                                    userClassBool = true;
                                    continue;
                                case 4:
                                    EditPassengerPickupDestination(inputPassengerID, thePassengersList, position, out editedPassenger, out addAllOtherEntriesBack);
                                    userClassBool = true;
                                    continue;
                                case 5:
                                    EditPassengerAllData(inputPassengerID, thePassengersList, position, out editedPassenger, out addAllOtherEntriesBack);
                                    userClassBool = true;
                                    continue;

                                case 9:
                                    userClassBool = false;
                                    break;
                                default:
                                    userClassBool = true;
                                    continue;
                            }
                        } while (userClassBool);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The passenger PID does not exist, account can't be edited.");
                Console.ResetColor();

                Console.ReadLine();
            }
        }

        private void EditPassengerAllData(string inputPassengerID, List<string> thePassengersList, string[] position, out string editedPassenger, out List<string> addAllOtherEntriesBack)
        {
            //edit all fields
            Console.Clear();
            //Ask user for the new name
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"===================================================");
            Console.WriteLine($"| You chose to change all of the passenger's data |");
            Console.WriteLine($"===================================================");
            Console.ResetColor();
            Console.Write($"\nHow do you want to be called now: ");
            string newUserNameI = Console.ReadLine();
            Console.Write($"Your new city as pickup location: ");
            string newPickUp = Console.ReadLine();
            Console.Write($"What's your new destination city: ");
            string newDestination = Console.ReadLine();
            //build a new string with all the passengers data
            editedPassenger = $"{position[0]},{newUserNameI},{newPickUp},{newDestination}";
            //select all other lines in the passenger.txt file add add them to a list
            addAllOtherEntriesBack = thePassengersList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
            //to the previously list you built with all other passengers - current, add the current edited passenger
            addAllOtherEntriesBack.Add(editedPassenger);
            //rewrite the passengers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the following: \n" +
                                $"1. Name from \" {position[1]} \" to \" {newUserNameI} \n" +
                                $"2. Pickup city from \" {position[2]} \" to \" {newPickUp} \"");
            Console.WriteLine($"3. Destination city from \" {position[3]} \" to \" {newDestination} \"");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditPassengerPickupDestination(string inputPassengerID, List<string> thePassengersList, string[] position, out string editedPassenger, out List<string> addAllOtherEntriesBack)
        {
            //edit pickup and destination
            Console.Clear();
            //Ask user for the new name
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"============================================================================");
            Console.WriteLine($"| You chose to change both the passenger's pickup location and destination |");
            Console.WriteLine($"============================================================================");
            Console.ResetColor();
            Console.Write($"\nYour new city as pickup location: ");
            string newPickUp = Console.ReadLine();
            Console.Write($"What's your new destination city: ");
            string newDestination = Console.ReadLine();
            //build a new string with all the passengers data
            editedPassenger = $"{position[0]},{position[1]},{newPickUp},{newDestination}";
            //select all other lines in the passenger.txt file add add them to a list
            addAllOtherEntriesBack = thePassengersList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
            //to the previously list you built with all other passengers - current, add the current edited passenger
            addAllOtherEntriesBack.Add(editedPassenger);
            //rewrite the passengers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the passenger's pickup city from \" {position[2]} \" to \" {newPickUp} \"");
            Console.WriteLine($"and the destination city from \" {position[3]} \" to \" {newDestination} \"");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditPassengerDestination(string inputPassengerID, List<string> thePassengersList, string[] position, out string editedPassenger, out List<string> addAllOtherEntriesBack)
        {
            //edit destination
            Console.Clear();
            //Ask user for the new name
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"===================================================");
            Console.WriteLine($"| You chose to change the passenger's destination |");
            Console.WriteLine($"===================================================");
            Console.ResetColor();
            Console.Write($"\nWhat is your new destination: ");
            string newDestination = Console.ReadLine();
            //build a new string with all the passengers data
            editedPassenger = $"{position[0]},{position[1]},{position[2]},{newDestination}";
            //select all other lines in the passenger.txt file add add them to a list
            addAllOtherEntriesBack = thePassengersList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
            //to the previously list you built with all other passengers - current, add the current edited passenger
            addAllOtherEntriesBack.Add(editedPassenger);
            //rewrite the passengers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the passenger's destination city from \" {position[3]} \" to \" {newDestination} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditPassengerPickup(string inputPassengerID, List<string> thePassengersList, string[] position)
        {
            //edit pickup location
            Console.Clear();
            //Ask user for the new name
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"=======================================================");
            Console.WriteLine($"| You chose to change the passenger's pickup location |");
            Console.WriteLine($"=======================================================");
            Console.ResetColor();
            Console.Write($"\nWhere do you want to be picked up from now: ");
            string newUserPickUp = Console.ReadLine();
            //build a new string with all the passengers data
            string editedPassengerPickUp = $"{position[0]},{position[1]},{newUserPickUp},{position[3]}";
            //select all other lines in the passenger.txt file add add them to a list
            var addAllOtherEntriesBackPick = thePassengersList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
            //to the previously list you built with all other passengers - current, add the current edited passenger
            addAllOtherEntriesBackPick.Add(editedPassengerPickUp);
            //rewrite the passengers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBackPick);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the passenger's pickup location from \" {position[2]} \" to \" {newUserPickUp} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditPassengerName(string inputPassengerID, List<string> thePassengersList, string[] position, out string newUserName, out string editedPassenger, out List<string> addAllOtherEntriesBack)
        {
            //edit passenger name
            Console.Clear();
            //Ask user for the new name
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"============================================");
            Console.WriteLine($"| You chose to change the passenger's name |");
            Console.WriteLine($"============================================");
            Console.ResetColor();
            Console.Write($"\nHow do you want to be called now: ");
            newUserName = Console.ReadLine();
            //build a new string with all the passengers data
            editedPassenger = $"{position[0]},{newUserName},{position[2]},{position[3]}";
            //select all other lines in the passenger.txt file add add them to a list
            addAllOtherEntriesBack = thePassengersList.Where(e => !e.Contains("PID" + inputPassengerID)).ToList();
            //to the previously list you built with all other passengers - current, add the current edited passenger
            addAllOtherEntriesBack.Add(editedPassenger);
            //rewrite the passengers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFilePassengers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the passenger's name from \" {position[1]} \" to \" {newUserName} \" !");
            Console.ResetColor();
            Console.ReadLine();
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
        }


    }
}
