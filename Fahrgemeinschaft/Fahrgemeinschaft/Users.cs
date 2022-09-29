using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public abstract class Users
    {
        public string Name { get; set; }
        public string StartingCity { get; set; }
        public float TimeStart { get; set; }
        public string Destination { get; set; }
        public List<Users> AllUserList { get; set; }

        public Users(string name, string startCity, float timeStart)
        {
            Name = name;
            StartingCity = startCity;
            TimeStart = timeStart;
        }


        public Users()
        {
            AllUserList = new List<Users>();

        }




    }


}
