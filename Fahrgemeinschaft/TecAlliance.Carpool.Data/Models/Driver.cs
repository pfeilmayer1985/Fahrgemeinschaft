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

        public Driver(string iD, int freePlaces, string name, string carTypeMake, string startCity, string destination)
        {
            ID = iD;
            FreePlaces = freePlaces;
            Name = name;
            CarTypeMake = carTypeMake;
            StartingCity = startCity;
            Destination = destination;


        }

    }
}
