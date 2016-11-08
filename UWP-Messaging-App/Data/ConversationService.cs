using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Messaging_App.Data
{
    // service for getting conversations from couchDB
    class ConversationService
    {
        // the conversation by its id
        private List<Conversation> conversations;

        public Conversation getConversationByID(string id)
        {
            // get the conversation from couchDB

            Conversation c = new Conversation();
            c.id = "c1";
            c.userIds = new List<string>();
            c.userIds.Add("u1");
            c.userIds.Add("u2");
            c.messages = new List<Message>();

            Message m = new Message();
            m.id = "1";
            m.senderId = "u1";
            m.recipientId = "u2";
            m.message = "Hello";
            c.messages.Add(m);

            return c;

        } // getConversationByID()

        // gets all of the conversations for a user
        public List<Conversation> getUsersCOnversations(string userId)
        {
            // get the conversation from couchDB
            // use the getConversationByID method

            return null;

        } // getUsersCOnversations()

    } // class

} // namespace
