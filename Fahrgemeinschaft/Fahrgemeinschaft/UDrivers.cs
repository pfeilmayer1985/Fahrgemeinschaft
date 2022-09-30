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
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        public UDrivers(string iD, string name, string startCity, float timeStart, string destination, string carTypeMake, int freePlaces)
        {
            ID = iD;
            Name = name;
            StartingCity = startCity;
            TimeStart = timeStart;
            Destination = destination;
            CarTypeMake = carTypeMake;
            FreePlaces = freePlaces;

        }

        public UDrivers()
        {
            DriversList = new List<UDrivers>();

        }

        public void AddOffer()
        {

            Console.Clear();
            Console.WriteLine("You are now adding a Carpool offer to the market.");
            Console.Write("Choose your unique Driver ID (DID): ");
            string id = "DID" + Console.ReadLine();
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
            Console.Write("When will you begin driving (use point to separate HH.MM, please): ");
            float timeStart = float.Parse(Console.ReadLine());

            File.AppendAllText(pathFileDrivers, ("\n" + id + "," + freePlaces + "," + name + "," + carTypeMake + "," + startCity + "," + destination + "," + timeStart));
        }

        public void ListAllOffers()
        {
            Console.Clear();
            Console.WriteLine("The available carpool offers are :");
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);
            
            foreach (string s in showDriversList)
            {
                Console.WriteLine(s);
            }

        }




    }
}
