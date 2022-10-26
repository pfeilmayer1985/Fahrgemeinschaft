using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    /// <summary>
    /// Main class for users (drivers and passengers)
    /// </summary>
    public abstract class UserBase
    {
        /// <summary>
        /// user class properties
        /// </summary>
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartingCity { get; set; }
        public string Destination { get; set; }

        /// <summary>
        /// user class constructor
        /// </summary>
        public UserBase()
        {
        }
    }
}
