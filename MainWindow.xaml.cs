using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace chat_app
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the message from the TextBox
            string message = MessageBox.Text;

            if (!string.IsNullOrWhiteSpace(message) && message != "Enter your message")
            {
                // Create a Border to style the message
                Border messageBorder = new Border
                {
                    //Background = Brushes.White,
                    BorderBrush = Brushes.Green,
                    BorderThickness = new Thickness(1,1,1,1),
                    CornerRadius = new CornerRadius(0),
                    Padding = new Thickness(10,2,0,2),
                    //Margin = new Thickness(5),
                    //HorizontalAlignment = HorizontalAlignment.Right // Aligns your message to the right
                };

                // Add the message text
                TextBlock messageText = new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.DarkRed,
                    TextWrapping = TextWrapping.Wrap
                };
                messageBorder.Child = messageText;

                // Add the message to the display area
                MessageDisplayArea.Children.Add(messageBorder);

                // Clear the TextBox
                MessageBox.Text = string.Empty;
            }
            ReceiveMessage("Hello from the other side!");
        }

        private void MessageBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Text == "Enter your message")
            {
                MessageBox.Text = string.Empty;
                MessageBox.Foreground = Brushes.Green;
            }
        }

        private void MessageBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageBox.Text))
            {
                MessageBox.Text = "Enter your message";
                MessageBox.Foreground = Brushes.Gray;
            }
        }

        // Simulate receiving messages from other users
        public void ReceiveMessage(string message)
        {
            Border messageBorder = new Border
            {
                //Background = Brushes.LightBlue,
                BorderBrush = Brushes.BlueViolet,
                BorderThickness = new Thickness(1,1,1,1),
                //CornerRadius = new CornerRadius(5),
                Padding = new Thickness(10,2,0,2),
                Margin = new Thickness(0,10,0,10),
                //HorizontalAlignment = HorizontalAlignment.Left // Aligns other user's messages to the left
            };

            TextBlock messageText = new TextBlock
            {
                Text = message,
                Foreground = Brushes.DarkOrange,
                TextWrapping = TextWrapping.Wrap
            };
            messageBorder.Child = messageText;

            MessageDisplayArea.Children.Add(messageBorder);
            

        }
    }
}
