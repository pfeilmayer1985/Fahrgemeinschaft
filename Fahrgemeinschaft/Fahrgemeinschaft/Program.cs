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
                Console.WriteLine("Welcome to the Free Carpool");
                Console.WriteLine("Choose your user class: ");
                Console.WriteLine("1. Driver ");
                Console.WriteLine("2. Passenger ");
                Console.WriteLine("3. Exit ");
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
                Console.WriteLine("2. Take a passenger (add a passenger PID to your existing Driver DID)");
                Console.WriteLine("3. Search for a passenger by departure city and destination city");
                Console.WriteLine("4. See the entire list of Passengers to find a match");
                Console.WriteLine("5. Back to the main menu");
                UDrivers offers = new UDrivers();
                UPassengers requests = new UPassengers();


                int driverClass = Convert.ToInt32(Console.ReadLine());
                switch (driverClass)
                {
                    case 1:
                        offers.AddOffer();
                        userDriverBool = false;
                        break;

                    case 2:

                        userDriverBool = false;
                        break;
                    case 3:

                        userDriverBool = false;
                        break;
                    case 4:
                        requests.ListAllRequests();
                        Console.ReadLine();
                        userDriverBool = false;
                        break;
                    case 5:

                        userDriverBool = false;
                        break;
                    default:

                        userDriverBool = true;
                        break;
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
                Console.WriteLine("3. Search for a driver/carpool by departure city and destination city");
                Console.WriteLine("4. See the entire list of Drivers to find a match");
                Console.WriteLine("5. Back to the main menu");
                UPassengers requests = new UPassengers();
                UDrivers offers = new UDrivers();


                int passengerClass = Convert.ToInt32(Console.ReadLine());
                switch (passengerClass)
                {
                    case 1:
                        requests.AddRequest();
                        userPassengerBool = false;
                        break;

                    case 2:

                        userPassengerBool = false;
                        break;
                    case 3:

                        userPassengerBool = false;
                        break;
                    case 4:
                        offers.ListAllOffers();
                        Console.ReadLine();
                        userPassengerBool = false;
                        break;
                    case 5:
                        Console.WriteLine("You choose to leave. Have a great one!");
                        userPassengerBool = false;
                        break;
                    default:

                        userPassengerBool = true;
                        break;
                }
            } while (userPassengerBool);

        }
    }

}
