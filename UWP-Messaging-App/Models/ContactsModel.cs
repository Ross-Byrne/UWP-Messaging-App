using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;

namespace UWP_Messaging_App.Models
{
    // model for managing contacts
    public class ContactsModel
    {
        public List<Contact> contacts = new List<Contact>();

        public ContactsModel()
        {

            Contact c = new Contact();
            c.ContactId = "org.couchdb.user:john";
            c.ConversationId = "c1";
            c.Name = "john";

            contacts.Add(c);

            c = new Contact();
            c.ContactId = "org.couchdb.user:tom";
            c.ConversationId = "c1";
            c.Name = "tom";

            contacts.Add(c);

            c = new Contact();
            c.ContactId = "org.couchdb.user:bob";
            c.ConversationId = "c1";
            c.Name = "bob";

            contacts.Add(c);

        } // Constructor

        // gets then contacts for current logged in user
        public List<Contact> getContacts()
        {
            // get contacts from couchDB

            return contacts;
        } // getContacts()

    } // class
}
