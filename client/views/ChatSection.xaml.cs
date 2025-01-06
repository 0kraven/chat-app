using System;
using System.Windows;
using System.Net.Sockets;
using System.IO;

namespace chat_app
{
    public partial class ChatWindow : Window
    {
        private TcpClient _client;  // Store the TcpClient

        // Constructor now accepts a TcpClient
        public ChatWindow(TcpClient client)
        {
            InitializeComponent();  // Initialize UI components
            _client = client;  // Store the TcpClient
            LoadConnectedUsers();  // Load sample connected users
            StartListening();  // Start listening for incoming messages
        }

        // Sample connected users to populate the sidebar
        private void LoadConnectedUsers()
        {
            UserList.Items.Add("User1");
            UserList.Items.Add("User2");
            UserList.Items.Add("User3");
        }

        // Start listening for incoming messages from the server
        private async void StartListening()
        {
            NetworkStream stream = _client.GetStream();
            StreamReader reader = new StreamReader(stream);

            try
            {
                while (true)
                {
                    string message = await reader.ReadLineAsync();  // Async read
                    if (!string.IsNullOrEmpty(message))
                    {
                        AddMessageToChat("Server", message);  // Display message in chat
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving message: " + ex.Message);
            }
        }

        // Event handler for the Send button click
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageInput.Text;

            if (!string.IsNullOrEmpty(message) && message != "Type your message here")
            {
                // Send the message to the server
                SendMessageToServer(message);

                // Add the message to the chat
                AddMessageToChat("You", message);

                // Clear the input field
                MessageInput.Clear();
            }
        }

        // Send the message to the server
        private void SendMessageToServer(string message)
        {
            try
            {
                NetworkStream stream = _client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(message);  // Send message to server
                writer.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
            }
        }

        // Method to add a new message to the chat area
        private void AddMessageToChat(string username, string message)
        {
            var messageTextBlock = new System.Windows.Controls.TextBlock
            {
                Text = $"{username}: {message}",
                Foreground = System.Windows.Media.Brushes.LimeGreen,
                FontSize = 12,
                TextWrapping = System.Windows.TextWrapping.Wrap
            };

            var messageBorder = new System.Windows.Controls.Border
            {
                BorderBrush = System.Windows.Media.Brushes.LimeGreen,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5),
                Margin = new Thickness(3),
                Child = messageTextBlock
            };

            if (MessagesPanel is System.Windows.Controls.Panel panel)
            {
                panel.Children.Insert(0, messageBorder);  // Insert at the top of the panel
            }
        }

        // Event handler for GotFocus (when the TextBox is focused)
        private void MessageInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MessageInput.Text == "Type your message here")
            {
                MessageInput.Text = "";
                MessageInput.Foreground = System.Windows.Media.Brushes.White;
            }
        }

        // Event handler for LostFocus (when the TextBox loses focus)
        private void MessageInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MessageInput.Text))
            {
                MessageInput.Text = "Type your message here";
                MessageInput.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }

}