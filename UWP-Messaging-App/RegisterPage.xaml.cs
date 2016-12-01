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
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        // registers the user details given
        private async void registerBT_Click(object sender, RoutedEventArgs e)
        {
            if (registerUsernameTextBox.Text != "" && registerPasswordTextBox.Password != "")
            {
                // create a new user
                await createUser();

            }
            else // otherwise
            {
                // report error
                errorTextBlock.Text = "Error! A Username AND Password must be entered!";

            } // if
        } // registerBT_Click()


        // goes to the login page
        private void loginBT_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the login page
            registerPageFrame.Navigate(typeof(LoginPage));

        } // loginBT_Click()


        // Creates a new CouchDB User
        private async Task createUser()
        {
            string _id = "org.couchdb.user:";
            try
            {
                // connect to _session endpoint
                using (var client = new MyCouchClient("http://admin:Balloon2016@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "_users"))
                {
                    // create new user object
                    var json = new JObject();
                    json.Add("_id", _id + registerUsernameTextBox.Text);
                    json.Add("name", registerUsernameTextBox.Text);
                    json.Add("password", registerPasswordTextBox.Password);
                    json.Add("roles", new JArray());
                    json.Add("type", "user");

                    // send post to CouchDB to create user
                    var result = await client.Documents.PostAsync(json.ToString());


                    /*System.Diagnostics.Debug.WriteLine("Results: " + result.IsSuccess);
                    System.Diagnostics.Debug.WriteLine("Error: " + result.Error);
                    System.Diagnostics.Debug.WriteLine("Reason: " + result.Reason);*/

                    // if successful
                    if (result.IsSuccess)
                    {
                        // save login details
                        // save to localstorage
                        var localSettings = ApplicationData.Current.LocalSettings;
                        localSettings.Values["CurrentUsername"] = registerUsernameTextBox.Text;
                        localSettings.Values["CurrentUserpassword"] = registerPasswordTextBox.Password;

                        // navigate to main page
                        registerPageFrame.Navigate(typeof(MainPage));

                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Conflict) // if username is already taken
                    {
                        // display error message
                        errorTextBlock.Text = "Username already taken! Try a different one!";
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


        } // createUser()
    }
}
