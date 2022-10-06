﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fahrgemeinschaft
{
    public class UDrivers : Users
    {
        public string CarTypeMake { get; set; }
        public string StartingCity { get; set; }
        public float TimeStart { get; set; }
        public string Destination { get; set; }
        public int FreePlaces { get; set; }
        public List<UDrivers> DriversList { get; set; }

        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";
        public UDrivers(string iD, string name, string startCity, string destination, string carTypeMake, int freePlaces)
        {
            ID = iD;
            Name = name;
            StartingCity = startCity;
            Destination = destination;
            CarTypeMake = carTypeMake;
            FreePlaces = freePlaces;

        }

        public UDrivers()
        {
            DriversList = new List<UDrivers>();

        }

        public void AddDriver()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("========================================");
            Console.WriteLine("| You are now  registering as a driver |");
            Console.WriteLine("========================================");
            Console.ResetColor();

            bool userInUse = false;
            string id;

            do
            {
                Console.Write("Choose your unique Driver ID (DID), 3 chars long: ");
                id = "DID" + Console.ReadLine();

                bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains(id));
                if (ckeckInputDriverID || id.Length != 6)
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

            Console.Write("Who is driving the car (driver's name): ");
            string name = Console.ReadLine();
            Console.Write("What is the make and model of the car: ");
            string carTypeMake = Console.ReadLine();
            Console.Write("How many places are free in the car: ");
            int freePlaces = Convert.ToInt32(Console.ReadLine());
            Console.Write("Departure from City: ");
            string startCity = Console.ReadLine();
            Console.Write("Destination City: ");
            string destination = Console.ReadLine();


            File.AppendAllText(pathFileDrivers, (id + "," + freePlaces + "," + name + "," + carTypeMake + "," + startCity + "," + destination));

            Console.WriteLine($"\nThe new user ID {id} for {name} was successfully added to the list. You can now receive passengers.");
            Console.ReadLine();

        }

        public void SeeDriver()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==================================");
            Console.WriteLine("| See your driver acount details |");
            Console.WriteLine("==================================");
            Console.ResetColor();

            //asking for the passenger ID
            Console.Write("\nEnter driver ID (DID): ");
            string inputDriverID = Console.ReadLine();

            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();
            var findDriverInDrivers = theDriversList.Where(e => e.Contains("DID" + inputDriverID)).ToList();

            bool exists = false;
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
                List<string> theEditedUserDetails = new List<string>();
                foreach (var driver in theDriversList)
                {
                    string[] position = driver.Split(',');
                    if (position[0] == ("DID" + inputDriverID))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("====================================================================");
                        Console.WriteLine($"| The following user informations are registered with your account |");
                        Console.WriteLine("====================================================================");
                        Console.ResetColor();

                        Console.WriteLine($"\nDriver ID: \t\t\t\t{position[0]}");
                        Console.WriteLine($"Driver's name: \t\t\t\t{position[2]}");
                        Console.WriteLine($"Registered vehicle: \t\t\t{position[3]}");
                        Console.WriteLine($"Free places available: \t\t\t{position[1]}");
                        Console.WriteLine($"Driving from location: \t\t\t{position[4]}");
                        Console.WriteLine($"Current registered destination: \t{position[5]}");
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
                Console.WriteLine("The driver DID does not exist, account content can't be retrieved.");
                Console.ResetColor();

                Console.ReadLine();
            }



        }

        public void ManageDriverAccount()
        {
            bool userClassBool = true;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=============================");
            Console.WriteLine("| Manage your driver acount |");
            Console.WriteLine("=============================");
            Console.ResetColor();

            //asking for the passenger ID
            Console.Write("\nEnter driver ID (DID): ");
            string inputDriverID = Console.ReadLine();

            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();
            var findDriverInDrivers = theDriversList.Where(e => e.Contains("DID" + inputDriverID)).ToList();

            bool exists = false;
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
                List<string> theEditedUserDetails = new List<string>();
                foreach (var driver in theDriversList)
                {
                    string[] position = driver.Split(',');
                    if (position[0] == ("DID" + inputDriverID))
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
                            Console.WriteLine($"\nThe User ID (' {position[0]} ') and the free places (' {position[1]} ') can't be changed.\nIf you want to change these, delete this Driver Account and make a new account.");
                            Console.ResetColor();
                            Console.WriteLine($"\n( 1 )\tPassenger name: {position[2]}");
                            Console.WriteLine($"( 2 )\tCar make and model: {position[3]}");
                            Console.WriteLine($"( 3 )\tDriving from location: {position[4]}");
                            Console.WriteLine($"( 4 )\tCurrent destination: {position[5]}");
                            Console.WriteLine($"( 5 )\tBoth origin and destination");
                            Console.WriteLine($"( 6 )\tAll fields above (name, car, pickup & destination)");

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

                                    EditDriverName(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 2:

                                    EditDriverCarMakeModel(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 3:

                                    EditDriverOrigin(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 4:

                                    EditDriverDestination(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 5:

                                    EditDriverOriginAndDestination(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 6:

                                    EditDriverAll(inputDriverID, theDriversList, position);
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
                Console.WriteLine("The driver DID does not exist, account can't be edited.");
                Console.ResetColor();

                Console.ReadLine();
            }
        }

        private void EditDriverAll(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit all driver data
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"=============================================================");
            Console.WriteLine($"| You chose to change all the available driver information  |");
            Console.WriteLine($"=============================================================");
            Console.ResetColor();
            //Ask user for the new name
            Console.Write($"\nHow do you want to be called now: ");
            string newUserName = Console.ReadLine();
            //Ask user for the new make and model
            Console.Write($"\nWhat make and model of a car are you driving now: ");
            string newMakeModel = Console.ReadLine();
            //Ask user for the new city - start point
            Console.Write($"\nWhat's your new city, that you are driving from (origin), called: ");
            string newOrigin = Console.ReadLine();
            //Ask user for the new city - destination
            Console.Write($"\nWhat's your new city, that you are driving to (destination), called: ");
            string newDestination = Console.ReadLine();
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{newUserName},{newMakeModel},{newOrigin},{newDestination}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the driver's name from \" {position[2]} \" to \" {newUserName} \",");
            Console.WriteLine($"then the car make and model from \" {position[3]} \" to \" {newMakeModel} \",");
            Console.WriteLine($"and changed the city you are driving from, from \" {position[4]} \" to \" {newOrigin} \",");
            Console.WriteLine($"finally the city you are driving to, from \" {position[5]} \" to \" {newDestination} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditDriverOriginAndDestination(string inputDriverID, List<string> theDriversList, string[] position)
        {

            //edit driving origin and destination
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"=====================================================================");
            Console.WriteLine($"| You chose to change both cities : origin and the destination city |");
            Console.WriteLine($"=====================================================================");
            Console.ResetColor();
            //Ask user for the new city - start point
            Console.Write($"\nWhat's your new city, that you are driving from (origin), called: ");
            string newOrigin = Console.ReadLine();
            //Ask user for the new city - destination
            Console.Write($"\nWhat's your new city, that you are driving to (destination), called: ");
            string newDestination = Console.ReadLine();
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{position[3]},{newOrigin},{newDestination}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the city you are driving from, from \" {position[4]} \" to \" {newOrigin} \",");
            Console.WriteLine($"and the city you are driving to, from \" {position[5]} \" to \" {newDestination} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditDriverDestination(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driving destination
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"============================================");
            Console.WriteLine($"| You chose to change the destination city |");
            Console.WriteLine($"============================================");
            Console.ResetColor();
            //Ask user for the new city - destination
            Console.Write($"\nWhat's your new city, that you are driving to, called: ");
            string newDestination = Console.ReadLine();
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{position[3]},{position[4]},{newDestination}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the city you are driving to, from \" {position[5]} \" to \" {newDestination} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditDriverOrigin(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driving from location
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"=====================================================");
            Console.WriteLine($"| You chose to change the city you are driving from |");
            Console.WriteLine($"====================================================");
            Console.ResetColor();
            //Ask user for the new city - start point
            Console.Write($"\nWhat's your new city, that you are driving from, called: ");
            string newOrigin = Console.ReadLine();
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{position[3]},{newOrigin},{position[5]}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the city you are driving from, from \" {position[4]} \" to \" {newOrigin} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditDriverCarMakeModel(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit car make and model
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"==============================================");
            Console.WriteLine($"| You chose to change the car make and model |");
            Console.WriteLine($"==============================================");
            Console.ResetColor();
            //Ask user for the new make and model
            Console.Write($"\nWhat make and model of a car are you driving now: ");
            string newMakeModel = Console.ReadLine();
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{newMakeModel},{position[4]},{position[5]}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the car make and model from \" {position[3]} \" to \" {newMakeModel} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void EditDriverName(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driver name
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"=========================================");
            Console.WriteLine($"| You chose to change the driver's name |");
            Console.WriteLine($"=========================================");
            Console.ResetColor();
            //Ask user for the new name
            Console.Write($"\nHow do you want to be called now: ");
            string newUserName = Console.ReadLine();
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{newUserName},{position[3]},{position[4]},{position[5]}".TrimEnd();
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack.Select(e=>e.TrimEnd()));
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the driver's name from \" {position[2]} \" to \" {newUserName} \" !");
            Console.WriteLine("\n\nPress <Enter> to return to the previous menu.");
            Console.ResetColor();
            Console.ReadLine();
        }

        public void ListAllOffers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("============================================");
            Console.WriteLine("| The following carpools are now available |");
            Console.WriteLine("============================================");
            Console.ResetColor();
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);
            int counterAvailable = 1;
            int counterUnavailable = 1;
            foreach (string driver in showDriversList)
            {
                string[] splittetDriverArray = driver.Split(',');
                if (Convert.ToInt32(splittetDriverArray[1]) > 0)
                {
                    Console.WriteLine("\n====================================================================================================================");
                    Console.WriteLine($"{counterAvailable}.\t{splittetDriverArray[2]} has {splittetDriverArray[1]} free places available and is driving a {splittetDriverArray[3]} from {splittetDriverArray[4]} to {splittetDriverArray[5]}.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\tHis driver ID is: {splittetDriverArray[0]}");
                    Console.ResetColor();
                    counterAvailable++;

                }

            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n===============================================================================");
            Console.WriteLine("| The following carpools are for the moment full and can't takeany passengers |");
            Console.WriteLine("===============================================================================");
            Console.ResetColor();

            foreach (string driver in showDriversList)
            {
                string[] splittetDriverArray = driver.Split(',');
                if (Convert.ToInt32(splittetDriverArray[1]) == 0)
                {
                    Console.WriteLine($"\n{counterUnavailable}.\t{splittetDriverArray[2]} is driving a {splittetDriverArray[3]} from {splittetDriverArray[4]} to {splittetDriverArray[5]}. Unfortunately he does not have any free seats available.");
                    counterUnavailable++;
                }



            }



        }




    }
}
