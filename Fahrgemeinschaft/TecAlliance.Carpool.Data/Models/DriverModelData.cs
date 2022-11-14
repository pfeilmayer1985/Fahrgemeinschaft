using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class DriverModelData : UserBaseModelData
    {
        public string ID { get; set; }
        public string CarTypeMake { get; set; }
        public int FreePlaces { get; set; }

        public DriverModelData(string id, int freePlaces, string firstName, string lastName, string carTypeMake, string startCity, string destination)
        {
            ID = id;
            FreePlaces = freePlaces;
            FirstName = firstName;
            LastName = lastName;
            CarTypeMake = carTypeMake;
            StartingCity = startCity;
            Destination = destination;
        }

        public DriverModelData()
        {
        }

        public string ToString()
        {
            return ($"\n{ID},{FreePlaces},{FirstName},{LastName},{CarTypeMake},{StartingCity},{Destination}");
        }
    }
}
