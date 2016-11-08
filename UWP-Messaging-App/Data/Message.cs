using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Messaging_App.Data
{
    // class for holding message data
    public class Message
    {
        public string id { get; set; }              // id of message
        public string message { get; set; }         // the message text
        public string senderId { get; set; }        // id of user who sent message

    } // class

} // namespace
