using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;

namespace UWP_Messaging_App.ViewModels
{
    // model to handle conversations in app
    public class ConversationViewModel : NotificationBase
    {

        private ConversationService convoService = new ConversationService();
        Conversation conversation { get; set; }
        

        public ConversationViewModel(string id)
        {
            this.conversation = convoService.getConversationByID(id);

        } // Constructor()

        public ObservableCollection<Message> Messages
        {
            get { return getMessages(); }
        } 

        public ObservableCollection<Message> getMessages()
        {
            return conversation.messages;
        }

        // add message to conversation
        public void sendMessage(string senderId, string message)
        {
            Message m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = senderId;
            m.message = message;

            conversation.messages.Add(m);

        } // sendMessage()

        public void addUser(string id)
        {
            conversation.userIds.Add(id);
        }
    } // class

} // namespace
