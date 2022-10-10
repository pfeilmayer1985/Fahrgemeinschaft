using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fahrgemeinschaft
{
    public class UPassengers : UsersC
    {
        public string StartingCity { get; set; }
        public string Destination { get; set; }
        public int HowManyPassengers { get; set; }

        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";

        public List<UPassengers> PassengersList { get; set; }

        HandleUserInputC h = new HandleUserInputC();

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

        /// <summary>
        /// This method saves a passenger account to the passenger.txt file
        /// </summary>

        public void AddPassenger()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("| You are now registering as a Passenger and adding a Carpool request to the market |");
            Console.WriteLine("=====================================================================================");
            Console.ResetColor();

            //if this file does not exist in the specified path
            if (!File.Exists(pathFilePassengers))
            {
                //the file will be created in the specified path
                File.Create(pathFilePassengers);
            }


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
            string name = h.HandleUserTextInput(true);
            Console.Write("Where do you want to be picked up (Departure City): ");
            string startCity = h.HandleUserTextInput(true);
            Console.Write("Destination City: ");
            string destination = h.HandleUserTextInput(true); ;
            File.AppendAllText(pathFilePassengers, ("\n" + id + "," + name + "," + startCity + "," + destination));
            Console.WriteLine($"\nThe new user ID {id} for {name} was successfully added to the list. You can now look for a carpool ride.");
            Console.ReadLine();
        }

        /// <summary>
        /// This method shows a passenger account details from the passenger.txt file, searching for passenger by passenger ID
        /// </summary>

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

        /// <summary>
        /// This is the main method of the manage passenger account, where all the choices of the menu are shown
        /// </summary>

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
                                    userClassBool = false;
                                    continue;
                                case 4:
                                    EditPassengerPickupDestination(inputPassengerID, thePassengersList, position, out editedPassenger, out addAllOtherEntriesBack);
                                    userClassBool = false;
                                    continue;
                                case 5:
                                    EditPassengerAllData(inputPassengerID, thePassengersList, position, out editedPassenger, out addAllOtherEntriesBack);
                                    userClassBool = false;
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

        /// <summary>
        /// This is a sub method of the manage passenger account method, where all of the passenger fields are editable, except ID
        /// </summary>

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
            string newUserNameI = h.HandleUserTextInput(true);
            Console.Write($"Your new city as pickup location: ");
            string newPickUp = h.HandleUserTextInput(true);
            Console.Write($"What's your new destination city: ");
            string newDestination = h.HandleUserTextInput(true);
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

        /// <summary>
        /// This is a sub method of the manage passenger account method, where only the passenger origin and destination fields are editable
        /// </summary>

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
            string newPickUp = h.HandleUserTextInput(true);
            Console.Write($"What's your new destination city: ");
            string newDestination = h.HandleUserTextInput(true);
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

        /// <summary>
        /// This is a sub method of the manage passenger account method, where only the passenger destination field is editable
        /// </summary>

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
            string newDestination = h.HandleUserTextInput(true);
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

        /// <summary>
        /// This is a sub method of the manage passenger account method, where only the origin of passenger field is editable
        /// </summary>

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
            string newUserPickUp = h.HandleUserTextInput(true);
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

        /// <summary>
        /// This is a sub method of the manage passenger account method, where only the passenger name field is editable
        /// </summary>

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
            newUserName = h.HandleUserTextInput(true);
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
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }


        /// <summary>
        /// This method lists all the drivers saved in the drivers.txt
        /// </summary>

        public void ListAllPassengers()
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
