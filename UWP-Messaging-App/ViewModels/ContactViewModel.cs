using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using UWP_Messaging_App.Models;

namespace UWP_Messaging_App.ViewModels
{
    // view model for contact
    class ContactViewModel : NotificationBase<Contact>
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

        public string Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }
        }

    } // class
}
