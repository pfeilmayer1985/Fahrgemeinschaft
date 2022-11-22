using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class CarpooslModelData
    {
        public int? CarpoolID { get; }
        public int DriverID { get; set; }
        public int FreeSeatsRemaining { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateOnly DepartureDate { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public CarpooslModelData(int carpoolID, int driverID, int freeSeatsRemaining, string origin, string destination, DateOnly departureDate, TimeOnly departureTime)
        {
            CarpoolID = carpoolID;
            DriverID = driverID;
            FreeSeatsRemaining = freeSeatsRemaining;
            Origin = origin;
            Destination = destination;
            DepartureDate = departureDate;
            DepartureTime = departureTime;
        }

        public CarpooslModelData()
        {

        }
    }
}
