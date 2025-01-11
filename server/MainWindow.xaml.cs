using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using server.views;

namespace server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for the "Save Configuration" button click
        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            string serverIP = ServerIPConfigBox.Text;    // Get the IP entered by the user
            string serverPort = ServerPortConfigBox.Text; // Get the port entered by the user

            // Validation for IP and Port
            if (!IPAddress.TryParse(serverIP, out _))
            {
                MessageBox.Show("Please enter a valid IP address.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Try to parse the port to ensure it's a valid number
            if (!int.TryParse(serverPort, out int port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Please enter a valid port number between 1 and 65535.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Here, you can save the configuration to a file or any persistent storage.
            // For example, storing the IP and Port in a configuration file (e.g., App.config, settings.json, etc.)

            // Example:
            // SaveConfig(serverIP, port);

            MessageBox.Show($"Server IP: {serverIP}\nPort: {port}", "Configuration Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            server.views.ServerLogs serverLogs = new server.views.ServerLogs(serverIP, port);
            serverLogs.Show();
            // Optionally, you can close the window after saving
            this.Close();
        }

        // Focus event to clear placeholder text when the user clicks into the textbox
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Enter Server IP" || textBox.Text == "Enter Server Port"))
            {
                textBox.Clear();
                textBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }

        // Lost focus event to restore placeholder text if the textbox is empty
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                if (textBox.Name == "ServerIPConfigBox")
                    textBox.Text = "Enter Server IP";
                else if (textBox.Name == "ServerPortConfigBox")
                    textBox.Text = "Enter Server Port";

                textBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            }
        }

        
    }
}
