using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class Carpool 
    {
        public string Driver { get; set; }
        public List<String> Passengers { get; set; }


        public Carpool(string driver, List<String> passenger)
        {
            Driver = driver;
            Passengers = passenger;
        }

        public Carpool()
        {
            
        }

    }
}
