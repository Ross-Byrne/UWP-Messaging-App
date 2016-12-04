using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Messaging_App.Data
{
    // class to hold information about indevidual contacts
    public class Contact
    {
        public string ContactId { get; set; }
        public string ConversationId { get; set; }
        public string UserOne { get; set; }
        public string UserTwo { get; set; }
        public bool UserOneAccepted { get; set; }
        public bool UserTwoAccepted { get; set; }
    } // class
}
