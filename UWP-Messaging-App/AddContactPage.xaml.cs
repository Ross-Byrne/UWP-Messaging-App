﻿using MyCouch;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Messaging_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContactPage : Page
    {
        public AddContactPage()
        {
            this.InitializeComponent();
        }

        // navigates back to last page
        private void backBT_Click(object sender, RoutedEventArgs e)
        {
            // go back
            Frame.GoBack();

        } // backBT_Click()

        // adds contact
        private void AddContactBT_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var user = localSettings.Values["CurrentUsername"] as string;

            // check that a username was entered first
            if (addContactUsernameTextBox.Text != "")
            {
                // make sure not trying to add self as contact
                if(addContactUsernameTextBox.Text == user)
                {
                    // cannot add self as contact
                    errorTextBlock.Text = "Oops! You can't add youself as a contact...";

                    return;
                } // if

                System.Diagnostics.Debug.WriteLine("Trying to add contact");

                // add contact
                addContact(addContactUsernameTextBox.Text); 


            } else // if no text
            {
                errorTextBlock.Text = "Oops! You forgot to enter a Username...";
            } // if

        } // AddContactBT_Click()

        // Creates a new CouchDB contact
        private async Task addContact(string username)
        {
            string _id = "org.couchdb.user:";
            try
            {
                // check that username is a value username
                using (var client = new MyCouchClient("http://admin:Balloon2016@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "_users"))
                {
                    var result = await client.Documents.GetAsync(_id + username);

                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error + "\nReason: " + result.Reason + "\nStatus code: " + result.StatusCode);

                    // handle the result
                    if (result.StatusCode == System.Net.HttpStatusCode.NotFound) // if not found
                    {
                        // show message
                        errorTextBlock.Text = "Oops! That user cannot be found!";
                    }
                    else if (result.IsSuccess == false) // if call did not work
                    {
                        errorTextBlock.Text = "Oops! Something went wrong. Please try again.";

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
                    // add conversation object to couchDB
                } // using

                // get contacts and add new contact to couch
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "contacts"))
                {
                    
                    // get all contacts
                    var result = await client.Documents.GetAsync("_all_docs");

                    // check that contact is not already added
                    if(result.Content != null) {

                        var keyvalues = client.Serializer.Deserialize<IDictionary<string, dynamic>>(result.Content);

                        System.Diagnostics.Debug.WriteLine(keyvalues.Keys);
                    }
                    

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
                    if (result.IsSuccess)
                    {
                        // navigate to contacts page
                        Frame.Navigate(typeof(ContactsPage));

                    }
                    else
                    {
                        // display an error
                        errorTextBlock.Text = "An Error occured. Please try again...";

                    } // if
                } // using

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                // display an error
                errorTextBlock.Text = "An Error occured. Please try again...";
            } // try


        } // addContact()
    }
}
