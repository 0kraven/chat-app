using System;
using System.Windows;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Media;

namespace chat_app.views
{
    public partial class ChatWindow : Window
    {
        private TcpClient _client;  // Store the TcpClient
        private string username;

        // Constructor now accepts a TcpClient
        public ChatWindow(TcpClient client, string user)
        {
            username = user;
            InitializeComponent();  // Initialize UI components
            _client = client;  // Store the TcpClient
            StartListening();  // Start listening for incoming messages
        }

        // Load connected users into the sidebar
        private void LoadConnectedUsers(List<string> users)
        {
            Dispatcher.Invoke(() =>
            {
                // Clear existing user list
                ConnectedUsersPanel.Children.Clear();

                // Add a title to the connected users list
                var titleTextBlock = new System.Windows.Controls.TextBlock
                {
                    Text = "Connected Users",
                    Foreground = System.Windows.Media.Brushes.LimeGreen,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5, 10, 5, 10)
                };
                ConnectedUsersPanel.Children.Add(titleTextBlock);

                // Add each user with a border
                foreach (string user in users)
                {
                    var userTextBlock = new System.Windows.Controls.TextBlock
                    {
                        Text = user,
                        Foreground = System.Windows.Media.Brushes.LimeGreen,
                        FontSize = 12,
                        TextWrapping = TextWrapping.Wrap,
                        Padding = new Thickness(5)
                    };

                    var userBorder = new System.Windows.Controls.Border
                    {
                        BorderBrush = System.Windows.Media.Brushes.LimeGreen,
                        BorderThickness = new Thickness(1),
                        Padding = new Thickness(5),
                        Margin = new Thickness(3),
                        Background = Brushes.DarkSlateGray,
                        Child = userTextBlock
                    };

                    // Add the bordered user to the ConnectedUsersPanel
                    ConnectedUsersPanel.Children.Add(userBorder);
                }
            });
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
                        // Check if the message contains the client list
                        if (message.StartsWith("CLIENT_LIST:"))
                        {
                            string clientListJson = message.Substring("CLIENT_LIST:".Length);
                            var connectedUsers = JsonSerializer.Deserialize<List<string>>(clientListJson);
                            LoadConnectedUsers(connectedUsers);  // Update the sidebar
                        }
                        else
                        {
                            AddMessageToChat(message);  // Display other messages in chat
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving message: " + ex.Message);
                this.Close();
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
        private void AddMessageToChat(string message)
        {
            Dispatcher.Invoke(() =>
            {
                var messageTextBlock = new System.Windows.Controls.TextBlock
                {
                    Text = $"{message}",
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
                MessagesPanel.Children.Add(messageBorder);
                ScrollViewer.ScrollToEnd();
            });
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
