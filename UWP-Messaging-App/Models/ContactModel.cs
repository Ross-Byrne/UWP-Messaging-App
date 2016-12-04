using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;

namespace UWP_Messaging_App.Models
{
    class ContactModel
    {
        private Contact contact = new Contact();

        public string ContactId
        {
            get { return contact.ContactId; }
            set { contact.ContactId = value; }
        }

        public string ConversationId
        {
            get { return contact.ConversationId; }
            set { contact.ConversationId = value; }
        }

        public string UserOne
        {
            get { return contact.UserOne; }
            set { contact.UserOne = value; }
        }

        public string UserTwo
        {
            get { return contact.UserTwo; }
            set { contact.UserTwo = value; }
        }

        public bool UserOneAccepted
        {
            get { return contact.UserOneAccepted; }
            set { contact.UserOneAccepted = value; }
        }

        public bool UserTwoAccepted
        {
            get { return contact.UserTwoAccepted; }
            set { contact.UserTwoAccepted = value; }
        }
    }
}
