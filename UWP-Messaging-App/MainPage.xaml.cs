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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Messaging_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ConversationViewModel conversation { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            // create the view model for the conversation
            conversation = new ConversationViewModel("c1");

        }

        // fires when the send button is clicked
        private void sendMessageBT_Click(object sender, RoutedEventArgs e)
        {
            // only if there is a message
            if (messageTB.Text != "")
            {
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
