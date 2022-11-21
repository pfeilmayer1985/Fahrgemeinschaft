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
    public class NewUserBaseModel
    {
        /// <summary>
        /// user class properties
        /// </summary>
        public string? ID { get; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDriver { get; set; }


       
       
    }
}
