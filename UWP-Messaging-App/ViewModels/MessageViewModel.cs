using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using Windows.Storage;

namespace UWP_Messaging_App.ViewModels
{
    // view model for the class Message
    public class MessageViewModel : NotificationBase<Message>
    {
        public MessageViewModel(Message message = null) : base(message) { }

        public string Id
        {
            get { return This.id; }
            set { SetProperty(This.id, value, () => This.id = value); }
        }

        public string Message
        {
            get { return This.message; }
            set { SetProperty(This.message, value, () => This.message = value); }
        }

        public string SenderId
        {
            get { return This.senderId; }
            set { SetProperty(This.senderId, value, () => This.senderId = value); }
        }

        public string Alignment
        {
            // if message is from currently logged in user, HorizontalAlignment = right
            // otherwise it is equal to left
            get
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                string userId = localSettings.Values["CurrentUsername"] as string; 

                // some method of checking logged in users id
                if (SenderId == userId)
                {
                    return "Right";
                }
                else
                {
                    return "Left";
                }
            }
        }

    }
}
