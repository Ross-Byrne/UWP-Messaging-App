using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Messaging_App.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ContactsPage : Page
    {
        ContactsViewModel contacts { get; set;}

        public ContactsPage()
        {
            this.InitializeComponent();

            // initialise viewModel
            contacts = new ContactsViewModel("Me");
        }

        // navigates to settings page
        private void settingsBT_Click(object sender, RoutedEventArgs e)
        {
            // navigate to the settings page
            Frame.Navigate(typeof(SettingsPage));

        } // settingsBT_Click()

        // when item is selected
        private void contactsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // go to conversation with selected contact
            var selectedItem = contactsList.SelectedItem as ContactViewModel;
            System.Diagnostics.Debug.WriteLine(selectedItem.Name);
        }
    }
}
