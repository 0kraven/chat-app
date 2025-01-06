using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Collections.Generic;

namespace ServerApp
{
    public partial class MainWindow : Window
    {
        private TcpListener _tcpListener;
        private Thread _listenerThread;
        private List<TcpClient> _clients;

        public MainWindow()
        {
            InitializeComponent();
            _tcpListener = new TcpListener(System.Net.IPAddress.Any, 12345);
            _clients = new List<TcpClient>();
        }

        // Start server button click handler
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartServer();
        }

        // Stop server button click handler
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopServer();
        }

        // Start server in a separate thread
        private void StartServer()
        {
            ServerStatus.Text = "Starting...";
            ServerStatus.Foreground = System.Windows.Media.Brushes.Yellow;

            try
            {
                _tcpListener.Start();
                _listenerThread = new Thread(() => ListenForClients());
                _listenerThread.IsBackground = true;
                _listenerThread.Start();

                ServerStatus.Text = "Running";
                ServerStatus.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                ServerStatus.Text = "Error: " + ex.Message;
                ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        // Stop server
        private void StopServer()
        {
            try
            {
                _tcpListener.Stop();
                _listenerThread?.Join();

                ServerStatus.Text = "Stopped";
                ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
            catch (Exception ex)
            {
                ServerStatus.Text = "Error: " + ex.Message;
                ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        // Listening for incoming client connections
        private void ListenForClients()
        {
            try
            {
                while (true)
                {
                    var client = _tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    // Show a popup message when a client is connected
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("New client connected!", "Client Connected", MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ServerStatus.Text = "Error: " + ex.Message;
                    ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
                });
            }
        }


        // Handle incoming client
        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            try
            {
                // Read the client's username
                string username = reader.ReadLine();

                // Acknowledge the client
                writer.WriteLine("OK");
                writer.Flush();

                // Add the client to the list of connected clients
                _clients.Add(client);
                UpdateClientListUI();

                // Keep listening for messages from the client
                while (true)
                {
                    string message = reader.ReadLine();
                    if (message == null) break;

                    // Handle message from client (e.g., broadcast)
                    Console.WriteLine("Received message from " + username + ": " + message);
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ServerStatus.Text = "Error: " + ex.Message;
                    ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
                });
            }
            finally
            {
                // Remove client and update UI when client disconnects
                _clients.Remove(client);
                UpdateClientListUI();
                client.Close();
            }
        }

        // Update the UI with the list of connected clients
        private void UpdateClientListUI()
        {
            Dispatcher.Invoke(() =>
            {
                ClientList.Items.Clear();
                foreach (var client in _clients)
                {
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    string username = reader.ReadLine();  // Assuming the first message is the username
                    ClientList.Items.Add(username);  // Display username in the UI
                }
            });
        }
    }
}
