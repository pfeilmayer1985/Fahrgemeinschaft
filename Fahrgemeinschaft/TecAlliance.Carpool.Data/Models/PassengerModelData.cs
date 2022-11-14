using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class PassengerModelData : UserBaseModelData
    {
        public PassengerModelData(string iD, string firstName, string lastName, string startCity, string destination)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            StartingCity = startCity;
            Destination = destination;
        }
        public PassengerModelData()
        {
        }
        public string ToString()
        {
            return ($"\n{ID},{FirstName},{LastName},{StartingCity},{Destination}");
        }
    }
}
