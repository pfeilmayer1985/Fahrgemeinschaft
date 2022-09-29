using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrgemeinschaft
{
    public abstract class Users
    {
        public string ID { get; set; }
        public string Name { get; set; }
        
        public List<Users> AllUserList { get; set; }




        public Users()
        {
            AllUserList = new List<Users>();

        }



    }


}
