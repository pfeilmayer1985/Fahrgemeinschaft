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
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
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

        public void AddOffer()
        {

            Console.Clear();
            Console.WriteLine("You are now adding a Carpool offer to the market.");

            bool userInUse = false;
            string id;
            do
            {
                Console.Write("Choose your unique Driver ID (DID), 3 chars long: ");
                id = "DID" + Console.ReadLine();

                bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains(id));
                if (ckeckInputDriverID)
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

        public void ListAllOffers()
        {
            Console.Clear();
            Console.WriteLine("The following carpools are now available: ");
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);
            int counterAvailable = 1;
            int counterUnavailable = 1;
            foreach (string driver in showDriversList)
            {
                string[] splittetDriverArray = driver.Split(',');
                if (Convert.ToInt32(splittetDriverArray[1]) > 0)
                {
                    Console.WriteLine($"\n{counterAvailable}. {splittetDriverArray[2]} has {splittetDriverArray[1]} free places available and is driving a {splittetDriverArray[3]} from {splittetDriverArray[4]} to {splittetDriverArray[5]}. His driver ID is: {splittetDriverArray[0]}");
                    counterAvailable++;

                }

            }

            Console.WriteLine("\n\nThe following carpools are for the moment full and can't takeany passengers: ");


            foreach (string driver in showDriversList)
            {
                string[] splittetDriverArray = driver.Split(',');
                if (Convert.ToInt32(splittetDriverArray[1]) == 0)
                {
                    Console.WriteLine($"\n{counterUnavailable}. {splittetDriverArray[2]} is driving a {splittetDriverArray[3]} from {splittetDriverArray[4]} to {splittetDriverArray[5]}. Unfortunately he does not have any free seats available.");
                    counterUnavailable++;
                }



            }



        }




    }
}
