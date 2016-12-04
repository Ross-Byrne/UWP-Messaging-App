using MyCouch;
using Newtonsoft.Json.Linq;
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
        private Conversation conversation { get; set; }

        public async Task<Conversation> getConversationByID(string id)
        {
            // get the conversation from couchDB
            conversation = await getConvoFromCouch(id);

            if(conversation == null)
            {
                return null;
            }

            // update messages
            conversation.messages = await getMessages(id);

            return conversation;

        } // getConversationByID()

        // adds message to couch and updates all messages
        public async Task addMessage(Message m)
        {
            try
            {
                // get logged in users details for authenticate calls
                var localSettings = ApplicationData.Current.LocalSettings;
                var user = localSettings.Values["CurrentUsername"] as string;
                var pass = localSettings.Values["CurrentUserpassword"] as string;

                // add message
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "messages"))
                {
                    // create json object for message
                    JObject json = new JObject();
                    json.Add("_id", m.id);
                    json.Add("conversationId", m.conversationId);
                    json.Add("message", m.message);
                    json.Add("senderId", m.senderId);
                    json.Add("timestamp", m.timestamp);

                    // save message to couchDB
                    var result = await client.Documents.PostAsync(json.ToString());

                    System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);

                    // if successful
                    if (result.IsSuccess)
                    {
                        // update messages
                        await getMessages(m.conversationId);

                    } // if

                } // using
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // try

        } // addMessage()

        // updates the messages for a conversation
        public async Task<List<Message>> getMessages(string convoId)
        {
            List<Message> temp = new List<Message>();
            try
            {
                // get logged in users details for authenticate calls
                var localSettings = ApplicationData.Current.LocalSettings;
                var user = localSettings.Values["CurrentUsername"] as string;
                var pass = localSettings.Values["CurrentUserpassword"] as string;

                // get messages
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "messages"))
                {

                    // get conversation from couchDB
                    var result = await client.Documents.GetAsync("_all_docs");

                    System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);

                    // if successful
                    if (result.IsSuccess)
                    {
                        if (result.Content != null)
                        {
                            var values = client.Serializer.Deserialize<JObject>(result.Content);

                            //System.Diagnostics.Debug.WriteLine(keyvalues["rows"].Count());

                            foreach (var value in values["rows"]) // for each message
                            {
                                // get id
                                var id = value["id"];

                                 System.Diagnostics.Debug.WriteLine(id.ToString());

                                // get the message with id
                                var message = await client.Documents.GetAsync(id.ToString());

                                // deserialize result
                                var m = client.Serializer.Deserialize<Message>(message.Content);

                                // check if the message is null and if the convo id matches the conversation
                                if (m != null && m.conversationId == convoId)
                                {  
                                    // add to list of temps
                                    temp.Add(m);
                                    System.Diagnostics.Debug.WriteLine("Sender: " + m.senderId + "Message: " + m.message);
                                    
                                } // if
                            } // foreach

                            // order the messages by time stamp
                            IOrderedEnumerable<Message> orderedList = temp.OrderBy(t => t.timestamp);
                            var messages = new List<Message>(orderedList);

                            // set messages var
                            conversation.messages = messages;

                            // return message list
                            return messages;
                            
                        } // if
                    } // if

                    return null;

                } // using

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            } // try

        } // getMessages()

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
