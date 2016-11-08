using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Messaging_App.Data
{
    // class to hold the data for a conversation
    class Conversation
    {
        public string id { get; set; }                  // id of conversation
        public List<string> userIds { get; set; }       // list of ids for users involved in conversation
        public List<Message> messages { get; set; }     // list of all messages in conversation

    } // class

} // namespace
