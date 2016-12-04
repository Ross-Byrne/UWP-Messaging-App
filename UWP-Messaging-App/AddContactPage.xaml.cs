using MyCouch;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        // Creates a new CouchDB User
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

                // get contacts for couch and add new contact to list
                using (var client = new MyCouchClient("http://" + user + ":" + pass + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "contacts"))
                {
                    // create new user object 
                    var json = new JObject();
                    /* json.Add("_id", _id + registerUsernameTextBox.Text);
                     json.Add("name", registerUsernameTextBox.Text);
                     json.Add("password", registerPasswordTextBox.Password);
                     json.Add("roles", new JArray());
                     json.Add("type", "user");*/

                    // send post to CouchDB to create user
                   // var result = await client.Documents.PostAsync(json.ToString());


                    /*System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);*/

                    //// if successful
                    //if (result.IsSuccess)
                    //{
                    //    // save login details
                    //    // save to localstorage
                    //    var localSettings = ApplicationData.Current.LocalSettings;
                    //    // localSettings.Values["CurrentUsername"] = registerUsernameTextBox.Text;
                    //    // localSettings.Values["CurrentUserpassword"] = registerPasswordTextBox.Password;

                    //    // navigate to main page
                    //    // registerPageFrame.Navigate(typeof(ConvoPage));

                    //}
                    //else if (result.StatusCode == System.Net.HttpStatusCode.Conflict) // if username is already taken
                    //{
                    //    // display error message
                    //    errorTextBlock.Text = "Username already taken! Try a different one!";
                    //}
                    //else
                    //{
                    //    // display an error
                    //    errorTextBlock.Text = "An Error occured. Please try again...";

                    //} // if
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
