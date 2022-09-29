using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fahrgemeinschaft
{
    public class UDrivers : Users
    {
        public string CarTypeMake { get; set; }
        public List<string> CitiesToVisit { get; set; }
        public int HowManyPlacesStandard { get; set; }
        public int HowManyPlacesFree { get; set; }
        public List<UDrivers> DriversList { get; set; }

        public UDrivers(string name, string startCity, float timeStart, string destination, string carTypeMake, List<string> citiesToVisit, int howManyPlacesStandard, int howManyPlacesFree)
        {
            Name = name;
            StartingCity = startCity;
            TimeStart = timeStart;
            Destination = destination;
            CarTypeMake = carTypeMake;
            CitiesToVisit = citiesToVisit;
            HowManyPlacesStandard = howManyPlacesStandard;
            HowManyPlacesFree = howManyPlacesFree;

        }

        public UDrivers()
        {
            DriversList = new List<UDrivers>();
        }

        public void AddOffer()
        {
            
            Console.Clear();
            Console.WriteLine("You are now adding a Carpool offer to the market.");
            Console.Write("Who is driving the car (driver's name): ");
            string name = Console.ReadLine();
            Console.Write("What is the make and model of the car: ");
            string carTypeMake = Console.ReadLine();
            Console.Write("How many places are available, without the driver's seat: ");
            int howManyPlacesStandard = Convert.ToInt32(Console.ReadLine());
            Console.Write("How many places, from total available amount, are you offering: ");
            int howManyPlacesFree = Convert.ToInt32(Console.ReadLine());
            Console.Write("Where are you driving from (City): ");
            string startCity = Console.ReadLine();
            Console.Write("What is your destination (City): ");
            string destination = Console.ReadLine();
            Console.Write("Through which cities will you be driving?\nPlease separate cities with one comma:  ");
            List<string>citiesToVisit = Console.ReadLine().Split(',').ToList();
            Console.Write("When will you begin driving (use point to separate HH.MM, please): ");
            float timeStart = float.Parse(Console.ReadLine());

            UDrivers myDriver = new UDrivers(name, startCity, timeStart, destination, carTypeMake, citiesToVisit, howManyPlacesStandard, howManyPlacesFree);
            DriversList.Add(myDriver);

        }

        public void ListAllOffers()
        {
            Console.Clear ();
            Console.WriteLine("The available carpool offers are :");
            for (int i = 0; i < DriversList.Count; i++)
            {
                Console.WriteLine($"{i}. The driver is {DriversList[i].Name}, he is driving a {DriversList[i].CarTypeMake}, has {DriversList[i].HowManyPlacesFree} free places," +
                    $"\ncoming from {DriversList[i].StartingCity} with the destination {DriversList[i].Destination}. " +
                    $"\nIn his journey he is also passing trough the following cities : {string.Join(" ", DriversList[i].CitiesToVisit)}. " +
                    $"\nHe will begin driving at {DriversList[i].TimeStart} o'clock.");
            }
        }

       
    }
}
