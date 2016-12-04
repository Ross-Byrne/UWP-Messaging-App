using MyCouch;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace UWP_Messaging_App.Models
{
    // model for managing contacts
    public class ContactsModel
    {
        public List<Contact> contacts = new List<Contact>();

        public ContactsModel()
        {

            // get the contacts for logged in user
            init();
        } // Constructor

        private async Task init()
        {
            // clear contacts list
            contacts.Clear();

            // get the contacts from couch
            await getContactsFromCouch();
        }

        public async Task<List<Contact>> getContacts()
        {
            // get the contacts from couch
            await getContactsFromCouch();
            return contacts;
        }

        // gets then contacts for current logged in user
        public async Task getContactsFromCouch()
        {
            List<Contact> tempContacts = new List<Contact>();

            // get logged in users details for authenticate calls
            var localSettings = ApplicationData.Current.LocalSettings;
            var user = localSettings.Values["CurrentUsername"] as string;
            var pass = localSettings.Values["CurrentUserpassword"] as string;

            try
            {
                // get contacts from couchDB
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "contacts"))
                {

                    // get all contacts
                    var result = await client.Documents.GetAsync("_all_docs");

                    /*System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);*/

                    // check that contact is not already added
                    if (result.Content != null)
                    {

                        var keyvalues = client.Serializer.Deserialize<JObject>(result.Content);

                        //System.Diagnostics.Debug.WriteLine(keyvalues["rows"].Count());

                        foreach(var value in keyvalues["rows"]) // for each contact
                        {
                            // get id
                            var id = value["id"];

                           // System.Diagnostics.Debug.WriteLine(id.ToString());

                            // get the contact
                            var contact = await client.Documents.GetAsync(id.ToString());

                            // deserialize result
                            var c = client.Serializer.Deserialize<Contact>(contact.Content);

                            if (c != null)
                            {
                                // check if logged in user is user one or two
                                if (user == c.UserOne || user == c.UserTwo)
                                {
                                    // add to list of temps
                                    tempContacts.Add(c);
                                    //System.Diagnostics.Debug.WriteLine("UserOne: " + c.UserOne + "\nUserTwo: " + c.UserTwo);
                                } // if

                            } // if
                        } // foreach
                    } // if

                    // add loaded contacts from couch to contacts
                    contacts = tempContacts;

                } // using

            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // try

        } // getContacts()

        // Creates a new CouchDB contact
        public async Task<string> addContact(string username)
        {
            string _id = "org.couchdb.user:";
            try
            {
                // check that contact isn't already added

                // get contacts
                List<Contact> temp = await getContacts();

                // check if the contact is added
                foreach (var contact in temp)
                {
                    // if contact is added
                    if (username == contact.UserOne || username == contact.UserTwo)
                    {
                        // say so
                        return "Oops! That contact is already added...";

                    } // if
                } // foreach

                // check that username is a value username
                using (var client = new MyCouchClient("http://admin:Balloon2016@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "_users"))
                {
                    var result = await client.Documents.GetAsync(_id + username);

                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error + "\nReason: " + result.Reason + "\nStatus code: " + result.StatusCode);

                    // handle the result
                    if (result.StatusCode == System.Net.HttpStatusCode.NotFound) // if not found
                    {
                        // show message
                        return "Oops! That user cannot be found!";
                    }
                    else if (result.IsSuccess == false) // if call did not work
                    {
                        return "Oops! Something went wrong. Please try again.";

                    } // if

                }

                // get logged in users details for authenticate calls
                var localSettings = ApplicationData.Current.LocalSettings;
                var user = localSettings.Values["CurrentUsername"] as string;
                var pass = localSettings.Values["CurrentUserpassword"] as string;

                // create a contact object
                Contact c = new Contact();

                // set ID for contact
                c.ContactId = Guid.NewGuid().ToString();

                // create ID for conversation
                c.ConversationId = Guid.NewGuid().ToString();

                // add username
                c.UserOne = username;
                c.UserTwo = user;
                c.UserOneAccepted = false;
                c.UserTwoAccepted = true;

                // create the conversation object
                Conversation convo = new Conversation();
                convo.id = c.ConversationId;
                convo.userIds = new List<string>();
                convo.userIds.Add(_id + user);
                convo.userIds.Add(_id + username);

                // create conversation object
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "conversations"))
                {
                    // create new conversation object 
                    var json = new JObject();
                    json.Add("_id", convo.id);
                    var ids = new JArray(convo.userIds);
                    json.Add("userIds", ids);

                    // add conversation object to couchDB
                    var createResult = await client.Documents.PostAsync(json.ToString());

                    System.Diagnostics.Debug.WriteLine("Results: " + createResult.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + createResult.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + createResult.Reason);

                    // if not successful
                    if (createResult.IsSuccess == false)
                    {
                        // display an error
                        return "An Error occured. Please try again...";
                        
                    } // if

                } // using

                // add new contact to couch
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "contacts"))
                {
                    // create new contact object 
                    var json = new JObject();
                    json.Add("_id", c.ContactId);
                    json.Add("conversationId", c.ConversationId);
                    json.Add("userOne", c.UserOne);
                    json.Add("UserTwo", c.UserTwo);
                    json.Add("userOneAccepted", c.UserOneAccepted);
                    json.Add("userTwoAccepted", c.UserTwoAccepted);

                    // send post to CouchDB to create contact
                    var createResult = await client.Documents.PostAsync(json.ToString());


                    System.Diagnostics.Debug.WriteLine("Results: " + createResult.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + createResult.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + createResult.Reason);

                    // if successful
                    if (createResult.IsSuccess)
                    {
                        // return "" string to indicate success
                        return "";

                    }
                    else
                    {
                        // display an error
                        return "An Error occured. Please try again...";

                    } // if
                } // using

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                // display an error
                return "An Error occured. Please try again...";
            } // try


        } // addContact()

    } // class
}
