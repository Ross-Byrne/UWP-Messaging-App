using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using UWP_Messaging_App.Models;

namespace UWP_Messaging_App.ViewModels
{
    // view model for an individual contact
    public class ContactViewModel : NotificationBase<Contact>
    {

        public ContactViewModel(Contact contact = null) : base(contact) { }

        public string ContactId
        {
            get { return This.ContactId; }
            set { SetProperty(This.ContactId, value, () => This.ContactId = value); }
        }

        public string ConversationId
        {
            get { return This.ConversationId; }
            set { SetProperty(This.ConversationId, value, () => This.ConversationId = value); }
        }

        public string UserOne
        {
            get { return This.UserOne; }
            set { SetProperty(This.UserOne, value, () => This.UserOne = value); }
        }

        public string UserTwo
        {
            get { return This.UserTwo; }
            set { SetProperty(This.UserTwo, value, () => This.UserTwo = value); }
        }

        public bool UserOneAccepted
        {
            get { return This.UserOneAccepted; }
            set { SetProperty(This.UserOneAccepted, value, () => This.UserOneAccepted = value); }
        }

        public bool UserTwoAccepted
        {
            get { return This.UserTwoAccepted; }
            set { SetProperty(This.UserTwoAccepted, value, () => This.UserTwoAccepted = value); }
        }

    } // class
}
