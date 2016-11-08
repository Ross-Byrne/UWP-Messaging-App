using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;

namespace UWP_Messaging_App.ViewModels
{
    // model to handle conversations in app
    class ConversationViewModel
    {

        private ConversationService convoService;
        private Conversation conversation;

        public ConversationViewModel(string id)
        {
            this.conversation = convoService.getConversationByID(id);

        } // Constructor()

        public Conversation getConversation()
        {
            return conversation;
        }

        public List<Message> getMessages()
        {
            return conversation.messages;
        }

        // add message to conversation
        public void sendMessage(string senderId, string recipientId, string message)
        {
            Message m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = senderId;
            m.recipientId = recipientId;
            m.message = message;

            conversation.messages.Add(m);

        } // sendMessage()

    } // class

} // namespace
