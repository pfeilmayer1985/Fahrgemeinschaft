using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class DriverModelDto : UserBaseDto
    {
        public string? ID { get; }
        public string? FreePlaces { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CarTypeMake { get; set; }
        public string StartingCity { get; set; }
        public string Destination { get; set; }
    }
}
