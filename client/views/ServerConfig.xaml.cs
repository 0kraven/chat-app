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
                if (textBox.Text == "Enter Username" || textBox.Text == "Enter Server IP" || textBox.Text == "Enter Port")
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
                    else if (textBox.Name == "ServerPortBox")
                    {
                        textBox.Text = "Enter Port";
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
            string serverPortText = ServerPortBox.Text;

            // Validation for Username, IP, and Port
            if (string.IsNullOrEmpty(username) || username == "Enter Username")
            {
                MessageBox.Show("Please enter a valid username.");
                resetFields();
                return;
            }

            if (string.IsNullOrEmpty(serverIP) || serverIP == "Enter Server IP")
            {
                MessageBox.Show("Please enter a valid server IP.");
                resetFields();
                return;
            }

            if (string.IsNullOrEmpty(serverPortText) || serverPortText == "Enter Port" || !int.TryParse(serverPortText, out int serverPort))
            {
                MessageBox.Show("Please enter a valid port number.");
                resetFields();
                return;
            }

            try
            {
                var client = new TcpClient();
                var result = client.BeginConnect(serverIP, serverPort, null, null);

                try
                {
                    // Block until the connection is complete
                    client.EndConnect(result);
                    MessageBox.Show("Connected to the server!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to connect: " + ex.Message);
                }

                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(username);  // Send username to server
                writer.Flush();

                // Optionally receive a response from the server
                StreamReader reader = new StreamReader(stream);
                string serverResponse = reader.ReadLine();

                    // Pass the TcpClient to the ChatWindow
                ChatWindow chatWindow = new ChatWindow(client, username);
                chatWindow.Show();
                this.Close();
                //client.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
                resetFields();

            }

        }
        private void resetFields()
        {
            UsernameBox.Text = "Enter Username";
            ServerIPBox.Text = "Enter Server IP";
            ServerPortBox.Text = "Enter Port";
            UsernameBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            ServerIPBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            ServerPortBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
        }
    }
}
