using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls; // Add this line

namespace chat_app.views
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
            string username = UsernameBox.Text;
            string serverIP = ServerIPBox.Text;

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
                var client = new TcpClient();
                var result = client.BeginConnect(serverIP, 5000, null, null);

                try {
                    

                    // Block until the connection is complete
                    client.EndConnect(result);

                    MessageBox.Show("Connected to the server!");
                } catch (Exception ex) {
                    MessageBox.Show("Failed to connect: " + ex.Message);
                }

                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(username);  // Send username to server
                writer.Flush();

                // Optionally receive a response from the server
                StreamReader reader = new StreamReader(stream);
                string serverResponse = reader.ReadLine();

                if (serverResponse == "OK")
                {
                    // Pass the TcpClient to the ChatWindow
                    ChatWindow chatWindow = new ChatWindow(client, username);
                    chatWindow.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Connection failed. Server responded with: " + serverResponse);
                }

                // client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

    }
}