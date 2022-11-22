using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Models
{
    public class CarpoolsModelDto
    {
        public int? CarpoolID { get; set;  }
        public UserBaseModelDto? DriverByID { get; set; }
        public int? FreeSeatsRemaining { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        
        public CarpoolsModelDto(int carpoolID, UserBaseModelDto driverByID, int freeSeatsRemaining, string origin, string destination, DateTime departureDate)
        {
            CarpoolID = carpoolID;
            DriverByID = driverByID;
            FreeSeatsRemaining = freeSeatsRemaining;
            Origin = origin;
            Destination = destination;
            DepartureDate = departureDate;
        
        }

        public CarpoolsModelDto()
        {

        }
    }
}
