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
            Console.WriteLine("You are now adding a Carpool request to the market.");

            bool userInUse = false;
            string id;
            do
            {
                Console.Write("Choose your unique passenger ID (PID), 4 chars long: ");
                id = "PID" + Console.ReadLine();

                bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains(id));
                if (ckeckInputPassengerID)
                {
                    //asking for the driver ID and checking if the ID exists in the drivers list
                    Console.WriteLine("This ID is allready in use. Choose another ID ");
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
            Console.WriteLine("The available carpool requests are :");

            string[] showPassengerList = File.ReadAllLines(pathFilePassengers);

            int counter = 1;
            foreach (string passenger in showPassengerList)
            {
                string[] splittetPassengerArray = passenger.Split(',');

                Console.WriteLine($"\n{counter}. {splittetPassengerArray[1]} wants to go from {splittetPassengerArray[2]} to {splittetPassengerArray[3]}. Passenger ID: {splittetPassengerArray[0]}");
                counter++;

            }
        }


    }
}
