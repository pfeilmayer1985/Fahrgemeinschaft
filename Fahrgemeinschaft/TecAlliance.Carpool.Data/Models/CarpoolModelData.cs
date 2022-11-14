using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class CarpoolModel
    {
        public string Driver { get; set; }
        public List<String> Passengers { get; set; }
        public CarpoolModel(string driver, List<String> passenger)
        {
            Driver = driver;
            Passengers = passenger;
        }

        public CarpoolModel()
        {
        }

        public string ToString()
        {
            string passengers = "";
            foreach (var passenger in Passengers)
            {
                passengers += "," + passenger.ToString();
            }

            return ($"{Driver}{passengers}");
        }
    }
}
