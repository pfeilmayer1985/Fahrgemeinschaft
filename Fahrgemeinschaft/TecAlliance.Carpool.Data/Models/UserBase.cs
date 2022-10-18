using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
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
        public string Name { get; set; }
        public string StartingCity { get; set; }
        public string Destination { get; set; }

        private List<UserBase> AllUserList { get; set; }

        /// <summary>
        /// user class constructor
        /// </summary>


        public UserBase(string id, string name, string start, string dest)
        {
            ID = id;
            Name = name;
            StartingCity = start;
            Destination = dest;

        }

        public UserBase()
        {
            AllUserList = new List<UserBase>();

        }



    }


}
