using MyCouch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWP_Messaging_App.Data;
using UWP_Messaging_App.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Messaging_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvoPage : Page
    {
        public ConversationViewModel conversation { get; set; }

        public ConvoPage()
        {
            this.InitializeComponent();

            // create the view model for the conversation
            conversation = new ConversationViewModel("c1");


            // create connection to couchDB

            //test();

        }

        private async Task test()
        {
            string username = "";
            string password = "";

            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;

                username = localSettings.Values["CurrentUsername"].ToString();
                password = localSettings.Values["CurrentUserpassword"].ToString();

            } catch(Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            try
            {

                System.Diagnostics.Debug.WriteLine(username + " " + password);
                using (var store = new MyCouchStore("http://" + username + ":" + password + "@uwp-couchdb.westeurope.cloudapp.azure.com:5984", "_users"))
                {
                    
                    // get the admin user
                    var user = await store.GetByIdAsync<User>("org.couchdb.user:rossbyrne");

                    // display the admin username
                    System.Diagnostics.Debug.WriteLine(user.name);
                } // using

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Oops. Something didn't go to plan!");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // try

        } // test()
        // fires when the send button is clicked
        private void sendMessageBT_Click(object sender, RoutedEventArgs e)
        {
            // only if there is a message
            if (messageTB.Text != "")
            {
                // use "u1" for current user for now
                // send the message. Trim the string for leading and trailing spaces.
                conversation.sendMessage("u1", messageTB.Text.Trim());

                // clear the message box
                messageTB.Text = "";

                // scroll to the bottom of the scroll area
                scrollView.ChangeView(scrollView.HorizontalOffset, scrollView.ScrollableHeight, scrollView.ZoomFactor, false);

            } // if

        } // sendMessageBT_Click()


        // fires when the list of messages is finished loading
        private void MessageList_Loaded(object sender, RoutedEventArgs e)
        {
            // scroll to the bottom of the scroll area
            scrollView.ChangeView(scrollView.HorizontalOffset, scrollView.ScrollableHeight, scrollView.ZoomFactor, false);

        } // MessageList_Loaded()
    }
}
