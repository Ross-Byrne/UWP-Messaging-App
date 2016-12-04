using MyCouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using Windows.Storage;

namespace UWP_Messaging_App.Models
{
    // model to manage conversation data
    class ConversationModel
    {
        public async Task<Conversation> getConversationByID(string id)
        {
            // get the conversation from couchDB
            Conversation c = await getConvoFromCouch(id);

            if(c == null)
            {
                return null;
            }

            // update messages
            c.messages = await getMessages(id);

           
           // c.id = "c1";
           // c.userIds = new List<string>();
           // c.userIds.Add("u1");
           // c.userIds.Add("u2");
           //// c.messages = new List<Message>();

           // m = new Message();
           // m.id = Guid.NewGuid().ToString();
           // m.senderId = "u2";
           // m.message = "Hey there";
           // m.timestamp = 636164041309011230;
           //// c.messages.Add(m);

           // m = new Message();
           // m.id = Guid.NewGuid().ToString();
           // m.id = Guid.NewGuid().ToString();
           // m.message = "Blah blah blah";
           // m.timestamp = 636164041309011239;
           // //c.messages.Add(m);

           // m = new Message();
           // m.id = Guid.NewGuid().ToString();
           // m.senderId = "u2";
           // m.message = "Damn I hate you";

           // m.timestamp = 636164041309011240;
           // c.messages.Add(m);

           // m = new Message();
           // m.id = Guid.NewGuid().ToString();
           // m.senderId = "u1";
           // m.message = "Thanks, you too.";
           // m.timestamp = 636164041309011249;
           // c.messages.Add(m);

           // m = new Message();
           // m.id = Guid.NewGuid().ToString();
           // m.senderId = "u2";
           // m.message = "<3";
           // m.timestamp = 636164041309011241;
           // c.messages.Add(m);

           // IOrderedEnumerable<Message> l = c.messages.OrderBy(t => t.timestamp);
           // c.messages = new List<Message>(l);

            return c;

        } // getConversationByID()

   
        // updates the messages for a conversation
        public async Task<List<Message>> getMessages(string convoId)
        {
            return null;
        }

        private async Task<Conversation> getConvoFromCouch(string id)
        {
            try
            {
                // get logged in users details for authenticate calls
                var localSettings = ApplicationData.Current.LocalSettings;
                var user = localSettings.Values["CurrentUsername"] as string;
                var pass = localSettings.Values["CurrentUserpassword"] as string;

                // get conversation object
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "conversations"))
                {
                   
                    // get conversation from couchDB
                    var result = await client.Documents.GetAsync(id);

                    System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);

                    // if successful
                    if (result.IsSuccess)
                    {
                        if (result.Content != null)
                        {
                            // deserialise conversation
                            var convo = client.Serializer.Deserialize<Conversation>(result.Content);

                           // System.Diagnostics.Debug.WriteLine(convo.id);

                            return convo;
                        } // if

                    } // if

                    return null;

                } // using

            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            } // try
        } // getConvoFromCouch()

    } // class
}
