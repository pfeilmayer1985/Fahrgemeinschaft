using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Models
{
    public class CarpoolModelDto
    {
        public string Driver { get; set; }
        public List<String> Passengers { get; set; }      

    }

    
}
