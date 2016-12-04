using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            // check that a username was entered first
            if(addContactUsernameTextBox.Text != "")
            {

            } else // if no text
            {
                errorTextBlock.Text = "Oops! You forgot to enter a Username...";
            } // if

        } // AddContactBT_Click()
    }
}
