using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class Driver : UserBase
    {
        public string CarTypeMake { get; set; }
        public int FreePlaces { get; set; }

        public Driver(string iD, int freePlaces, string firstName, string lastName, string carTypeMake, string startCity, string destination)
        {
            ID = iD;
            FreePlaces = freePlaces;
            FirstName = firstName;
            LastName = lastName;
            CarTypeMake = carTypeMake;
            StartingCity = startCity;
            Destination = destination;


        }

        public Driver()
        {
        }

        public string ToString()
        {
            return ($"\n{ID},{FreePlaces},{FirstName},{LastName},{CarTypeMake},{StartingCity},{Destination}");
        }
    }
}
