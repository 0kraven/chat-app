using System;
using System.Windows;
using System.Windows.Controls;
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
            // Assuming you want to retrieve the values from the textboxes
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

            // Logic to handle the server connection goes here
            // Example: Call a method to connect to the server using the entered IP and username
            ChatWindow chatWindow = new ChatWindow();
            chatWindow.Show();

            // Optionally, close the current window (MainWindow) after opening ChatWindow
            this.Close();

            // Proceed with the server connection or transition to another page
            // Example: new ChatPage().Show(); this.Close();
        }
    }
}
