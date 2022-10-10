using System;

namespace Fahrgemeinschaft
{
    public class Program
    {
        static void Main(string[] args)
        {

            MainScreen();

        }

        /// <summary>
        /// The MainScreen method is the main menu of the carpool app, where the user chooses it's class (driver/passenger)
        /// </summary>

        public static void MainScreen()
        {
            bool userClassBool = true;

            do
            {
                //Clearing console and showing the main menu in a loop. User can choose tthe drivers or passengers menu, list all the carpools or exit
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("===============================");
                Console.WriteLine("| Welcome to the Free Carpool |");
                Console.WriteLine("===============================");
                Console.ResetColor();
                Console.WriteLine("\n( 1 )\tDrivers");
                Console.WriteLine("( 2 )\tPassengers");
                Console.WriteLine("( 3 )\tSee existing carpools");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 4 )\tExit");
                Console.ResetColor();
                int userClass;
                CarpoolC carpools = new CarpoolC();

                //if user chooses a non-existing menu item stays in loop until he enters a existing value
                bool pressedRightKey = false;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nChoose one of the options above: ");
                    Console.ResetColor();

                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);


                    if (!int.TryParse(userInput, out userClass))
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
                        carpools.ListAllCarpools();
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

        /// <summary>
        /// This is the main menu for the Drivers class, managing account, taking a passenger (making a carpool), kick passenger, see the passengers, drivers and carpool lists
        /// </summary>

        public static void DriverMenu()
        {
            bool userDriverBool = true;

            do
            {

                UDrivers driversClass = new UDrivers();
                UPassengers passengersClass = new UPassengers();
                CarpoolC carpoolsClass = new CarpoolC();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("===============================");
                Console.WriteLine("| You are in the Drivers Menu |");
                Console.WriteLine("===============================");
                Console.ResetColor();
                Console.WriteLine("\n( 1 )\tManage your Driver Account");
                Console.WriteLine("( 2 )\tTake a passenger (you must be registered)");
                Console.WriteLine("( 3 )\tKick a passenger from your carpool");
                Console.WriteLine("( 4 )\tSearch for a passenger by departure city and destination city");
                Console.WriteLine("( 5 )\tSee the entire list of Passengers to find a match");
                Console.WriteLine("( 6 )\tAre you registered? See the list of Drivers");
                Console.WriteLine("( 7 )\tSee existing carpools");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the main menu");
                Console.ResetColor();

                //if user chooses a non-existing menu item stays in loop until he enters a existing value

                int driverMenu;
                bool pressedRightKey = false;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nChoose one of the options above: ");
                    Console.ResetColor();

                    //string userInput = Console.ReadLine();

                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);


                    if (!int.TryParse(userInput, out driverMenu))
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

                switch (driverMenu)
                {
                    case 1:
                        DriverAccountMenu();
                        userDriverBool = true;
                        continue;

                    case 2:
                        carpoolsClass.OfferCarpoolToPassenger();
                        userDriverBool = true;
                        continue;
                    case 3:
                        carpoolsClass.RemovePassengerFromCarpool();
                        userDriverBool = true;
                        continue;
                    case 4:
                        carpoolsClass.SearchPassengerStartDestination();
                        userDriverBool = true;
                        continue;
                    case 5:
                        passengersClass.ListAllPassengers();
                        Console.ReadLine();
                        userDriverBool = true;
                        continue;
                    case 6:
                        driversClass.ListAllDrivers();
                        Console.ReadLine();
                        userDriverBool = true;
                        continue;
                    case 7:
                        carpoolsClass.ListAllCarpools();
                        userDriverBool = true;
                        continue;

                    case 9:
                        userDriverBool = false;
                        break;
                    default:

                        userDriverBool = true;
                        continue;
                }
            } while (userDriverBool);

        }

        /// <summary>
        /// This is the account management for the drivers class, register, edit, delete account
        /// </summary>

        public static void DriverAccountMenu()
        {
            bool userDriverBool = true;

            do
            {

                UDrivers driversClass = new UDrivers();
                UPassengers passengersClass = new UPassengers();
                CarpoolC carpoolsClass = new CarpoolC();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("==============================");
                Console.WriteLine("| Manage your Driver Account |");
                Console.WriteLine("==============================");
                Console.ResetColor();
                Console.WriteLine("\n( 1 )\tRegister as a new driver for the Carpool");
                Console.WriteLine("( 2 )\tSee your existing account details");
                Console.WriteLine("( 3 )\tEdit your existing account");
                Console.WriteLine("( 4 )\tDelete your driver account completely");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the drivers menu");
                Console.ResetColor();

                //if user chooses a non-existing menu item stays in loop until he enters a existing value

                int driverMenu;
                bool pressedRightKey = false;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nChoose one of the options above: ");
                    Console.ResetColor();

                    //string userInput = Console.ReadLine();

                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);

                    if (!int.TryParse(userInput, out driverMenu))
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

                switch (driverMenu)
                {
                    case 1:
                        driversClass.AddDriver();
                        userDriverBool = true;
                        continue;

                    case 2:
                        driversClass.SeeDriver();
                        continue;
                    case 3:
                        driversClass.ManageDriverAccount();
                        userDriverBool = true;
                        continue;
                    case 4:
                        driversClass.RemoveDriverAccount();
                        userDriverBool = true;
                        continue;
                    case 9:
                        userDriverBool = false;
                        break;
                    default:

                        userDriverBool = true;
                        continue;
                }
            } while (userDriverBool);

        }

        /// <summary>
        /// This is the main menu for the Passengers class, managing account, joining a carpool, cancelling a carpool, see the passengers, drivers and carpool lists 
        /// </summary>

        public static void PassengerMenu()
        {

            bool userPassengerBool = true;

            do
            {

                UPassengers passengersClass = new UPassengers();
                UDrivers driversClass = new UDrivers();
                CarpoolC carpoolsClass = new CarpoolC();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("==================================");
                Console.WriteLine("| You are in the Passengers Menu |");
                Console.WriteLine("==================================");
                Console.ResetColor();
                Console.WriteLine("\n( 1 )\tManage your Passenger Account");
                Console.WriteLine("( 2 )\tTake a ride (you must be registered)");
                Console.WriteLine("( 3 )\tCancel a ride - remove yourself from a carpool");
                Console.WriteLine("( 4 )\tSearch for a driver/carpool by departure city and destination city");
                Console.WriteLine("( 5 )\tSee the entire list of Drivers to find a match");
                Console.WriteLine("( 6 )\tAre you registered? See the list of Passengers");
                Console.WriteLine("( 7 )\tSee existing carpools");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the main menu");
                Console.ResetColor();

                //if user chooses a non-existing menu item stays in loop until he enters a existing value

                int passengerMenu;
                bool pressedRightKey = false;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nChoose one of the options above: ");
                    Console.ResetColor();

                    //string userInput = Console.ReadLine();

                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);

                    if (!int.TryParse(userInput, out passengerMenu))
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


                switch (passengerMenu)
                {
                    case 1:
                        PassengerAccountMenu();
                        userPassengerBool = true;
                        continue;
                    case 2:
                        carpoolsClass.AddPassengerToCarpool();
                        Console.ReadLine();
                        userPassengerBool = true;
                        continue;
                    case 3:
                        carpoolsClass.RemovePassengerFromCarpool();
                        userPassengerBool = true;
                        continue;
                    case 4:
                        carpoolsClass.SearchCarpoolStartDestination();
                        userPassengerBool = true;
                        continue;
                    case 5:
                        driversClass.ListAllDrivers();
                        Console.ReadLine();
                        userPassengerBool = true;
                        continue;
                    case 6:
                        passengersClass.ListAllPassengers();
                        Console.ReadLine();
                        userPassengerBool = true;
                        continue;
                    case 7:
                        carpoolsClass.ListAllCarpools();
                        userPassengerBool = true;
                        continue;

                    case 9:
                        userPassengerBool = false;
                        break;
                    default:

                        userPassengerBool = true;
                        continue;
                }
            } while (userPassengerBool);

        }

        /// <summary>
        /// This is the account management for the passengers class, register, edit, delete account
        /// </summary>

        public static void PassengerAccountMenu()
        {

            bool userPassengerBool = true;

            do
            {

                UPassengers passengersClass = new UPassengers();
                UDrivers driversClass = new UDrivers();
                CarpoolC carpoolsClass = new CarpoolC();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=================================");
                Console.WriteLine("| Manage your Passenger Account |");
                Console.WriteLine("=================================");
                Console.ResetColor();
                Console.WriteLine("\n( 1 )\tRegister as a new passenger for the Carpool");
                Console.WriteLine("( 2 )\tSee your existing account details");
                Console.WriteLine("( 3 )\tEdit your existing account");
                Console.WriteLine("( 4 )\tDelete your passenger account completely");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the passengers menu");
                Console.ResetColor();

                //if user chooses a non-existing menu item stays in loop until he enters a existing value

                int passengerMenu;
                bool pressedRightKey = false;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nChoose one of the options above: ");
                    Console.ResetColor();

                    //string userInput = Console.ReadLine();

                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);

                    if (!int.TryParse(userInput, out passengerMenu))
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


                switch (passengerMenu)
                {
                    case 1:
                        passengersClass.AddPassenger();
                        userPassengerBool = true;
                        continue;
                    case 2:
                        passengersClass.SeePassenger();
                        userPassengerBool = true;
                        continue;
                    case 3:
                        passengersClass.ManagePassengerAccount();
                        userPassengerBool = true;
                        continue;
                    case 4:
                        carpoolsClass.RemovePassengerAccount();
                        userPassengerBool = true;
                        continue;
                    case 9:
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
