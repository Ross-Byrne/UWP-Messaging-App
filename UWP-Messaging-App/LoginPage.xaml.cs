using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private void loginBT_Click(object sender, RoutedEventArgs e)
        {
            if(loginUsernameTextBox.Text != "" && loginPasswordTextBox.Password != "")
            {
                // Do login

                // save to localstorage
                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["CurrentUsername"] = loginUsernameTextBox.Text;
                localSettings.Values["CurrentUserpassword"] = loginPasswordTextBox.Password;

                System.Diagnostics.Debug.WriteLine("Saved Username: " + localSettings.Values["CurrentUsername"]);
                System.Diagnostics.Debug.WriteLine("Saved Password: " + localSettings.Values["CurrentUserpassword"]);

                // navigate to main page
                loginPageFrame.Navigate(typeof(MainPage));

            }
            else // otherwise
            {
                // report error
                errorTextBlock.Text = "Error! Username AND Password must be entered!";

            } // if

        }


        // fires when the login button is clicked
        private void registerBT_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
