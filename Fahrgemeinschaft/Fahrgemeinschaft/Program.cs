using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fahrgemeinschaft
{
    public class Program
    {
        static void Main(string[] args)
        {
            UDrivers offers = new UDrivers();
            UPassengers requests = new UPassengers();
            CarpoolC carpools = new CarpoolC();



            TheMainScreen();





        }

        public static void TheMainScreen()
        {
            bool userClassBool = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Free Carpool.");
                Console.WriteLine("\n1. Driver");
                Console.WriteLine("2. Passenger");
                Console.WriteLine("3. See existing carpools");
                Console.WriteLine("4. Exit");
                Console.Write("\nChoose one of the options above:");
                int userClass = Convert.ToInt32(Console.ReadLine());
                switch (userClass)
                {
                    case 1:
                        DriverMenu();
                        userClassBool = true;
                        continue;
                    case 2:
                        PassengerMenu();
                        userClassBool = true;
                        continue;
                    case 3:

                        userClassBool = true;
                        continue;
                    case 4:
                        Console.WriteLine("You choose to leave. Have a great one!");
                        userClassBool = false;
                        break;
                    default:

                        userClassBool = true;
                        continue;
                }
            } while (userClassBool);


        }

        public static void DriverMenu()
        {
            bool userDriverBool = true;

            do
            {

                Console.Clear();
                Console.WriteLine("You are a Driver. What would you like to do: ");
                Console.WriteLine("1. Register as a driver for the Carpool");
                Console.WriteLine("2. Take a passenger (add a passenger PID to create a carpool)");
                Console.WriteLine("3. Kick a passenger (remove a passenger PID from your existing carpool)");
                Console.WriteLine("4. Search for a passenger by departure city and destination city");
                Console.WriteLine("5. See the entire list of Passengers to find a match");
                Console.WriteLine("6. Back to the main menu");
                UDrivers offers = new UDrivers();
                UPassengers requests = new UPassengers();
                CarpoolC carpools = new CarpoolC();


                int driverClass = Convert.ToInt32(Console.ReadLine());
                switch (driverClass)
                {
                    case 1:
                        offers.AddOffer();
                        userDriverBool = true;
                        continue;

                    case 2:
                        carpools.OfferCarpoolToPassenger();
                        userDriverBool = true;
                        continue;
                    case 3:
                        carpools.RemovePassengerFromCarpool();
                        userDriverBool = true;
                        continue;
                    case 4:
                        carpools.SearchPassengerStartDestination();
                        userDriverBool = true;
                        continue;
                    case 5:
                        requests.ListAllRequests();
                        Console.ReadLine();
                        userDriverBool = true;
                        continue;
                    case 6:
                        userDriverBool = false;
                        break;
                    default:

                        userDriverBool = true;
                        continue;
                }
            } while (userDriverBool);

        }
        public static void PassengerMenu()
        {

            bool userPassengerBool = true;

            do
            {
                Console.Clear();
                Console.WriteLine("You are a Passenger. What would you like to do: ");
                Console.WriteLine("1. Register as a passenger for the Carpool");
                Console.WriteLine("2. Take a ride (add your passenger PID to an existing driver DID)");
                Console.WriteLine("3. Remove a passenger PID from an existing carpool");
                Console.WriteLine("4. Search for a driver/carpool by departure city and destination city");
                Console.WriteLine("5. See the entire list of Drivers to find a match");
                Console.WriteLine("6. Back to the main menu");
                UPassengers requests = new UPassengers();
                UDrivers offers = new UDrivers();
                CarpoolC carpools = new CarpoolC();


                int passengerClass = Convert.ToInt32(Console.ReadLine());
                switch (passengerClass)
                {
                    case 1:
                        requests.AddRequest();
                        userPassengerBool = true;
                        continue;
                    case 2:
                        carpools.AddPassengerToCarpool();
                        Console.ReadLine();
                        userPassengerBool = true;
                        continue;
                    case 3:
                        carpools.RemovePassengerFromCarpool();
                        userPassengerBool = true;
                        continue;
                    case 4:
                        carpools.SearchCarpoolStartDestination();
                        userPassengerBool = true;
                        continue;
                    case 5:
                        offers.ListAllOffers();
                        Console.ReadLine();
                        userPassengerBool = true;
                        continue;
                    case 6:
                        userPassengerBool = false;
                        break;
                    default:

                        userPassengerBool = true;
                        continue;
                }
            } while (userPassengerBool);

        }
    }

}
