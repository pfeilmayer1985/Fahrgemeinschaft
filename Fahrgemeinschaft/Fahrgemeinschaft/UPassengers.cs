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
        public List<UPassengers> PassengersList { get; set; }
        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";

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

        public void AddRequest()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You are now adding a Carpool request to the market.");
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

        public void ListAllRequests()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The available carpool requests are :");
            Console.ResetColor();

            string[] showPassengerList = File.ReadAllLines(pathFilePassengers);

            int counter = 1;
            foreach (string passenger in showPassengerList)
            {
                string[] splittetPassengerArray = passenger.Split(',');

                Console.WriteLine($"\n{counter}.\t{splittetPassengerArray[1]} wants to go from {splittetPassengerArray[2]} to {splittetPassengerArray[3]}.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\tPassenger ID: {splittetPassengerArray[0]}");
                Console.ResetColor();

                counter++;

            }
        }


    }
}
