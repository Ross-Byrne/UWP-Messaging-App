using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Conversation c;
            Message m;

            c = new Conversation();
            c.id = "c1";
            c.userIds = new List<string>();
            c.userIds.Add("u1");
            c.userIds.Add("u2");
            c.messages = new List<Message>();

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u2";
            m.message = "Hey there";
            m.timestamp = 636164041309011230;
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.id = Guid.NewGuid().ToString();
            m.message = "Blah blah blah";
            m.timestamp = 636164041309011239;
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u2";
            m.message = "Damn I hate you";

            m.timestamp = 636164041309011240;
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "Thanks, you too.";
            m.timestamp = 636164041309011249;
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u2";
            m.message = "<3";
            m.timestamp = 636164041309011241;
            c.messages.Add(m);

            IOrderedEnumerable<Message> l = c.messages.OrderBy(t => t.timestamp);
            c.messages = new List<Message>(l);

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
