using System;
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


            File.AppendAllText(pathFileDrivers, ("\n" + id + "," + freePlaces + "," + name + "," + carTypeMake + "," + startCity + "," + destination));

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
