using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class DriverModelDto : UserBase
    {
        public string ID { get; set; }
        public int FreePlaces { get; set; }
        public string Name { get; set; }
        public string CarTypeMake { get; set; }
        public string StartingCity { get; set; }
        public string Destination { get; set; }



    }
}
