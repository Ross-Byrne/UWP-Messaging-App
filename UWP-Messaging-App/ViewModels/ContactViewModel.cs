using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using UWP_Messaging_App.Models;
using Windows.Storage;

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

        // get the contact name that is not the logged in user
        public string ContactName
        {
            get
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                var username = localSettings.Values["CurrentUsername"] as string;

                if (username == UserOne)
                {
                    return UserTwo;
                }
                else
                {
                    return UserOne;
                } // if
            } // get
        }

    } // class
}
