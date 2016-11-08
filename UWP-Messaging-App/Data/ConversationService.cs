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

            return null;

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
