using MyCouch;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        // fires when the login button is clicked
        private async void loginBT_Click(object sender, RoutedEventArgs e)
        {
            if(loginUsernameTextBox.Text != "" && loginPasswordTextBox.Password != "")
            {
                // Validate login details
                await validateLoginDetails();

            }
            else // otherwise
            {
                // report error
                errorTextBlock.Text = "Error! Username AND Password must be entered!";

            } // if

        } // loginBT_Click()


        // fires when the register button is clicked
        private void registerBT_Click(object sender, RoutedEventArgs e)
        {
            // navigate to register page
            Frame.Navigate(typeof(RegisterPage));

        } // registerBT_Click()


        // validates the users login details.
        // saves them if correct, displays error if wrong.
        private async Task validateLoginDetails()
        {
            try
            {
                // connect to _session endpoint
                using (var client = new MyCouchClient("http://uwp-couchdb.westeurope.cloudapp.azure.com:5984", "_session"))
                {
                    // create username and password object
                    var json = new JObject();
                    json.Add("name", loginUsernameTextBox.Text);
                    json.Add("password", loginPasswordTextBox.Password);

                    // send post to get session cookie
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
                        localSettings.Values["CurrentUsername"] = loginUsernameTextBox.Text;
                        localSettings.Values["CurrentUserpassword"] = loginPasswordTextBox.Password;

                        // navigate to contacts page
                        Frame.Navigate(typeof(ContactsPage));

                    }
                    else if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // display error message
                        errorTextBlock.Text = "Unauthorized! Username or Password incorrect!";
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
           

        } // validateLoginDetails()
    }
}
