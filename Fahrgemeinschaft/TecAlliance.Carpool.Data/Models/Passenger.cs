using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class Passenger : UserBase
    {
        public Passenger(string iD, string firstName, string lastName, string startCity, string destination)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            StartingCity = startCity;
            Destination = destination;
        }
        public Passenger()
        {
        }
        public string ToString()
        {
            return ($"\n{ID},{FirstName},{LastName},{StartingCity},{Destination}");
        }
    }
}
