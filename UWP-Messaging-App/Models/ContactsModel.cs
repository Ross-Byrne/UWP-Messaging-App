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

                    System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);

                    // check that contact is not already added
                    if (result.Content != null)
                    {

                        var keyvalues = client.Serializer.Deserialize<JObject>(result.Content);

                        foreach(var value in keyvalues["rows"]) // for each contact
                        {
                            // get id
                            var id = value["id"];

                            System.Diagnostics.Debug.WriteLine(id.ToString());

                            // get the contact
                            var contact = await client.Documents.GetAsync(id.ToString());

                            // deserialize result
                            var c = client.Serializer.Deserialize<Contact>(contact.Content);

                            // add to list of temps
                            tempContacts.Add(c);

                            System.Diagnostics.Debug.WriteLine("UserOne: " + c.UserOne + "\nUserTwo: " + c.UserTwo);

                        }
                        //System.Diagnostics.Debug.WriteLine(keyvalues.Keys);
                    }

                   
                } // using

            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // try

        } // getContacts()

    } // class
}
