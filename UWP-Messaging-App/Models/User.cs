using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Messaging_App.Models
{
    // class for holding user data
    class User
    {
        public string id { get; set; }              // id of user
        public string firstName { get; set; }       // users first name
        public string lastName { get; set; }        // users last name
        public string email { get; set; }           // users email address (used for login)
        public string password { get; set; }        // users password (used for login)

    } // class

} // namespace
