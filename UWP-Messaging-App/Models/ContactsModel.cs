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

                    // check that contact is not already added
                    if (result.Content != null)
                    {

                        var keyvalues = client.Serializer.Deserialize<IDictionary<string, dynamic>>(result.Content);

                        System.Diagnostics.Debug.WriteLine(keyvalues.Keys);
                    }

                   
                } // using

            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // try

        } // getContacts()

    } // class
}
