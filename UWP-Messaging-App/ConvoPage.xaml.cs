using MyCouch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
        public ContactViewModel contact { get; set; }

        private Timer timer;

        public ConvoPage()
        {
            this.InitializeComponent();
            // adapted from this question
            // http://stackoverflow.com/questions/34271100/timer-in-uwp-app-which-isnt-linked-to-the-ui

            var _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 15);

            _dispatcherTimer.Start();
        }

        private void _dispatcherTimer_Tick(object sender, object e)
        {
            if (contact != null)
            {
                if (conversation.isUpdatingMessages != true)
                {
                    // check for messages
                    conversation.updateMessages(contact.ConversationId);

                    System.Diagnostics.Debug.WriteLine("Timer!");

                } else
                {

                    System.Diagnostics.Debug.WriteLine("Skiped update");
                }

            } // if
        }

        private async void dispatcherTimer_Tick()
        {
            // do some work not connected with UI
            if (contact != null)
            {
                

                await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => {
                    // do some work on UI here;
                    // check for messages
                    conversation.updateMessages(contact.ConversationId);

                    System.Diagnostics.Debug.WriteLine("Timer!");
                });

            } // if
           
        } // timerCallback

        // runs when page is navigated to
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var localSettings = ApplicationData.Current.LocalSettings;
            var username = localSettings.Values["CurrentUsername"] as string;

            // get contact view model from param
            contact = e.Parameter as ContactViewModel;

            // create the view model for the conversation
            conversation = new ConversationViewModel(contact.ConversationId);

        } // OnNavigatedTo()

        // fires when the send button is clicked
        private void sendMessageBT_Click(object sender, RoutedEventArgs e)
        {
            // only if there is a message
            if (messageTB.Text != "")
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                string userid = localSettings.Values["CurrentUsername"] as string;

                // send username.
                // send the message. Trim the string for leading and trailing spaces.
                conversation.sendMessage(userid, messageTB.Text.Trim());

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

        // goes back to last page
        private void backBT_Click(object sender, RoutedEventArgs e)
        {
            // navigate back a page
            Frame.GoBack();

        } // backBT_Click()
    }
}
