using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "_id")]
        public string id { get; set; }              // id of message
        public string conversationId { get; set; }  // id of conversation message belongs
        public string message { get; set; }         // the message text
        public string senderId { get; set; }        // id of user who sent message
        public long timestamp { get; set; }       // timestamp

    } // class

} // namespace
