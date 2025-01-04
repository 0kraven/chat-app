using System;
using System.Windows;

namespace chat_app
{
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();  // This ensures that the XAML is properly initialized
            LoadConnectedUsers();
        }

        // Sample connected users to populate the sidebar
        private void LoadConnectedUsers()
        {
            // Add some sample users to the list
            UserList.Items.Add("User1");
            UserList.Items.Add("User2");
            UserList.Items.Add("User3");
        }

        // Event handler for the Send button click
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageInput.Text;

            // Ensure there's something to send
            if (!string.IsNullOrEmpty(message) && message != "Type your message here")
            {
                AddMessageToChat("You", message); // Add the message to the chat
                MessageInput.Clear(); // Clear the input field after sending
            }
        }

        // Method to add a new message to the chat area
        private void AddMessageToChat(string username, string message)
        {
            var messageTextBlock = new System.Windows.Controls.TextBlock
            {
                Text = $"{username}: {message}",
                Foreground = System.Windows.Media.Brushes.LimeGreen,
                FontSize = 14,
                Margin = new Thickness(5),
                TextWrapping = System.Windows.TextWrapping.Wrap
                
            };
            var messageBorder = new System.Windows.Controls.Border
                {
                    BorderBrush = System.Windows.Media.Brushes.Gray, // Border color
                    BorderThickness = new Thickness(1), // Border thickness
                    Padding = new Thickness(5), // Space between border and text
                    Margin = new Thickness(5), // Space between messages
                    Child = messageTextBlock // Add TextBlock as the content of the Border
                };
            // Add the message to the message panel
            MessagesPanel.Children.Insert(0, messageTextBlock);
        }

        // Event handler for GotFocus (when the TextBox is focused)
        private void MessageInput_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the placeholder text when the TextBox is focused
            if (MessageInput.Text == "Type your message here")
            {
                MessageInput.Text = "";
                MessageInput.Foreground = System.Windows.Media.Brushes.White; // Change text color to white when typing
            }
        }

        // Event handler for LostFocus (when the TextBox loses focus)
        private void MessageInput_LostFocus(object sender, RoutedEventArgs e)
        {
            // Restore the placeholder text if the TextBox is empty
            if (string.IsNullOrEmpty(MessageInput.Text))
            {
                MessageInput.Text = "Type your message here";
                MessageInput.Foreground = System.Windows.Media.Brushes.Gray; // Set text color to gray for placeholder
            }
        }
    }
}