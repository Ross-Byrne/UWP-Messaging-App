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
    public sealed partial class SettingsPage : Page
    {
        public string username { get; set; }
        public SettingsPage()
        {
            this.InitializeComponent();

            var localSettings = ApplicationData.Current.LocalSettings;
            username = localSettings.Values["CurrentUsername"] as string;

        }

        // sends the user back
        private void backBT_Click(object sender, RoutedEventArgs e)
        {
            // navigate back to the contacts page
            Frame.GoBack();

        } // backBT_Click()

        // logs the current user out and sends them to the login screen
        private void logoutBT_Click(object sender, RoutedEventArgs e)
        {
            // clear user login details from localstorage
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["CurrentUsername"] = null;
            localSettings.Values["CurrentUserpassword"] = null;

            // send the user back to login screen
            Frame.Navigate(typeof(LoginPage));

        } // logoutBT_Click()
    }
}
