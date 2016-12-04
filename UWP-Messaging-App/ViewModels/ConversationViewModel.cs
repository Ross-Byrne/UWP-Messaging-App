using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using UWP_Messaging_App.Models;
using Windows.UI.Xaml.Controls;

namespace UWP_Messaging_App.ViewModels
{
    // model to handle conversations in app
    public class ConversationViewModel : NotificationBase
    {

        private ConversationModel convoModel = new ConversationModel();
        Conversation conversation { get; set; }

        ObservableCollection<MessageViewModel> _Messages
           = new ObservableCollection<MessageViewModel>();


        public ConversationViewModel(string id)
        {
            // initialise the view model
            init(id);
        } // Constructor()

        // initialise the view model
        public async Task init(string id)
        {
            // get the conversation object
            this.conversation = await convoModel.getConversationByID(id);

            if(conversation == null)
            {
                return;
            }

            // load the messages
            foreach (var mes in conversation.messages)
            {
                var m = new MessageViewModel(mes);
                m.PropertyChanged += Message_OnNotifyPropertyChanged;
                _Messages.Add(m);
            } // for

        } // init()

        public ObservableCollection<MessageViewModel> Messages
        {
            get { return _Messages; }
            set { SetProperty(ref _Messages, value); }
        }

        //public ObservableCollection<Message> Messages
        //{
        //    get { return getMessages(); }
        //} 

        // old method
        public List<Message> getMessages()
        {
            return conversation.messages;
        }

        // add message to conversation
        public void sendMessage(string senderId, string message)
        {
            //Message m = new Message();
            //m.id = Guid.NewGuid().ToString();
            //m.senderId = senderId;
            //m.message = message;

            // get timestamp
            long timestamp = DateTime.UtcNow.Ticks;

            // create a new message view model
            var m = new MessageViewModel();
            m.Id = Guid.NewGuid().ToString();
            m.SenderId = senderId;
            m.Timestamp = timestamp;
            m.ConversationId = conversation.id;
            m.Message = message;

            m.PropertyChanged += Message_OnNotifyPropertyChanged;
            Messages.Add(m);
            conversation.messages.Add(m); // just for now
            // breeds.Add(m); // conversation model add method (adds to couchDB)

        } // sendMessage()

        public void addUser(string id)
        {
            conversation.userIds.Add(id);
        }

        void Message_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            //conversation.Update((MessageViewModel)sender); // method to update couchDB
        }
    } // class

} // namespace
