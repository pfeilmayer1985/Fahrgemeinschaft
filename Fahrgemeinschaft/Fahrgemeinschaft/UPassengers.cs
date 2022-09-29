using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public class UPassengers : Users
    {
        public int HowManyPassengers { get; set; }
        public List<UPassengers> PassengersList { get; set; }

        public UPassengers(string name, string startCity, float timeStart, string destination, int howManyPassengers)
        {
            Name = name;
            StartingCity = startCity;
            TimeStart = timeStart;
            Destination = destination;
            HowManyPassengers = howManyPassengers;
        }

        public UPassengers()
        {
            PassengersList = new List<UPassengers>();

        }

        public void AddRequest()
        {

            Console.Clear();
            Console.WriteLine("You are now adding a Carpool request to the market.");
            Console.Write("What's your name?: ");
            string name = Console.ReadLine();
            Console.Write("Will you be travelling alone (y,n)?: ");
            string userChoice = Console.ReadLine();
            int howManyPassengers = 0;
            bool rightAnswer = true;

            while (rightAnswer)
            {
                if (userChoice == "y")
                {
                    howManyPassengers = 1;
                    rightAnswer = false;
                }
                else if (userChoice == "n")
                {
                    Console.Write("How many companions will you have?: ");
                    howManyPassengers = Convert.ToInt32(Console.ReadLine());
                    rightAnswer = false;

                }
                else
                {
                    continue;
                }

            }

            Console.Write("Where do you want to be picked up (City): ");
            string startCity = Console.ReadLine();
            Console.Write("What is your destination (City): ");
            string destination = Console.ReadLine();
            Console.Write("When do you want to be picked up (use point to separate HH.MM, please): ");
            float timeStart = float.Parse(Console.ReadLine());

            UPassengers myPassenger = new UPassengers(name, startCity, timeStart, destination, howManyPassengers);
            PassengersList.Add(myPassenger);

        }

        public void ListAllRequests()
        {
            Console.Clear();
            Console.WriteLine("The available carpool requests are :");
            for (int i = 0; i < PassengersList.Count; i++)
            {
                if (PassengersList[i].HowManyPassengers == 1)
                {
                    Console.WriteLine($"{i}. The main passenger is {PassengersList[i].Name}, he is travelling to {PassengersList[i].Destination} alone." +
                        $"\n{PassengersList[i].Name} wants to be picked up in {PassengersList[i].StartingCity} at {PassengersList[i].TimeStart} o'clock.");
                }
                else
                {
                    Console.WriteLine($"{i}. The main passenger is {PassengersList[i].Name}, he is travelling to {PassengersList[i].Destination} with {PassengersList[i].HowManyPassengers - 1} companions." +
                        $"\n{PassengersList[i].Name} wants to be picked up in {PassengersList[i].StartingCity} at {PassengersList[i].TimeStart} o'clock.");
                }
            }
        }


    }
}
