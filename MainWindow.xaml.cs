using System;
using System.Windows;
using System.Windows.Threading;

namespace chat_app
{
    public partial class MainWindow : Window
    {
        private string message = "Welcome To <ZeroDayCrew> Chat!";  // The message to display (without the exclamation mark)
        private DispatcherTimer cursorTimer;  // Timer to handle blinking effect

        public MainWindow()
        {
            InitializeComponent();
            InitializeBlinkingEffect();
        }

        // Initialize the blinking effect for "!"
        private void InitializeBlinkingEffect()
        {
            cursorTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)  // Blinking effect interval for "!"
            };
            cursorTimer.Tick += CursorTimer_Tick;  // Event handler to toggle "!" visibility
        }

        // This method starts the blinking effect and shows the full message
        private void StartBlinkingEffect()
        {
            TypingText.Text = message;  // Display the full message
            EnterButton.Visibility = Visibility.Visible;  // Show the "Enter" button immediately
            cursorTimer.Start();  // Start blinking the "!" at the end of the message
        }

        // Blinking cursor effect (toggle visibility of "!")
        private void CursorTimer_Tick(object sender, EventArgs e)
        {
            if (TypingText.Text.EndsWith("!"))
            {
                // Remove "!" if it's currently visible
                TypingText.Text = TypingText.Text.Substring(0, TypingText.Text.Length - 1);
                TypingText.Text += " ";
            }
            else
            {
                // Add "!" at the end of the message
                TypingText.Text = TypingText.Text.Substring(0, TypingText.Text.Length - 1);
                TypingText.Text += "!";
            }
        }

        // Start the effect when the window loads
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartBlinkingEffect();  // Start displaying the message with blinking "!" and show the button
        }

        // Enter button click handler
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle what happens when the Enter button is pressed
            // Create an instance of ServerConfig from Views folder
            Views.ServerConfig serverConfigWindow = new Views.ServerConfig();
            serverConfigWindow.Show();  // Show the ServerConfig window
            this.Close();  // Close the MainWindow  // Example action (can be replaced with other functionality)
        }
    }
}
