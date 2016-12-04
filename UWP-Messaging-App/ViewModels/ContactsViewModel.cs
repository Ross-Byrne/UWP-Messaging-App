using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Messaging_App.Models;

namespace UWP_Messaging_App.ViewModels
{
    // view model to display all contacts
    public class ContactsViewModel : NotificationBase
    {
        private ContactsModel contactsModel = new ContactsModel();

        // collection of contacts
        ObservableCollection<ContactViewModel> _Contacts
           = new ObservableCollection<ContactViewModel>();


        public ContactsViewModel(string id)
        {
            // initialise the view model
            init();

        } // Constructor()

        private async Task init()
        {
            var contacts = await contactsModel.getContacts();

            // load contacts
            foreach (var contact in contacts)
            {
                var c = new ContactViewModel(contact);
                // c.PropertyChanged += Contact_OnNotifyPropertyChanged;
                _Contacts.Add(c);
            }
        }

        public ObservableCollection<ContactViewModel> Contacts
        {
            get { return _Contacts; }
            set { SetProperty(ref _Contacts, value); }
        }

 
        
        // add contact Method
        public void addContact(string id)
        {
           // contactsModel.Add(id);
        }

        void Contact_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            //conversation.Update((MessageViewModel)sender); // method to update couchDB
        }

    } // class
}
