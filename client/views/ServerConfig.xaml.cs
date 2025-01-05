using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls; // Add this line

namespace chat_app.Views
{
    public partial class ServerConfig : Window
    {
        public ServerConfig()
        {
            InitializeComponent();
        }

        // Event handler for UsernameBox GotFocus
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text == "Enter Username" || textBox.Text == "Enter Server IP")
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = System.Windows.Media.Brushes.White;
                }
            }
        }

        // Event handler for UsernameBox LostFocus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox.Name == "UsernameBox")
                    {
                        textBox.Text = "Enter Username";
                    }
                    else if (textBox.Name == "ServerIPBox")
                    {
                        textBox.Text = "Enter Server IP";
                    }
                    textBox.Foreground = System.Windows.Media.Brushes.Gray;
                }
            }
        }

        // Event handler for Connect button click
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from the textboxes
            string username = UsernameBox.Text;
            string serverIP = ServerIPBox.Text;

            // Simple validation to ensure fields aren't empty
            if (string.IsNullOrEmpty(username) || username == "Enter Username")
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

            if (string.IsNullOrEmpty(serverIP) || serverIP == "Enter Server IP")
            {
                MessageBox.Show("Please enter a valid server IP.");
                return;
            }

            try
            {
                // Attempt to connect to the server with timeout handling
                var client = new TcpClient();
                var result = client.BeginConnect(serverIP, 12345, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5)); // 5-second timeout

                if (!success)
                {
                    throw new Exception("Connection timed out.");
                }

                client.EndConnect(result); // Complete connection

                // Send the username to the server
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(username);  // Sending username to the server
                writer.Flush();

                // Optionally, you can receive a response from the server (e.g., success/failure message)
                StreamReader reader = new StreamReader(stream);
                string serverResponse = reader.ReadLine();

                if (serverResponse == "OK")
                {
                    // Successfully connected and authenticated, open the chat window
                    ChatWindow chatWindow = new ChatWindow();
                    chatWindow.Show();

                    // Close the ServerConfig window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Connection failed. Server responded with: " + serverResponse);
                }

                // Close the TCP client connection
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }
    }
}
