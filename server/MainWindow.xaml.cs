using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace TcpServer
{
    public partial class MainWindow : Window
    {
        private TcpListener _tcpListener;
        private List<TcpClient> _clients = new List<TcpClient>();
        private List<StreamWriter> _clientWriters = new List<StreamWriter>();

        public MainWindow()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            try
            {
                string ipAddress = "192.168.1.4"; // Localhost
                int port = 5000; // The port number for the server

                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                ServerStatus.Text = "Server started, waiting for clients...";
                ServerStatus.Foreground = System.Windows.Media.Brushes.Green;

                Thread listenerThread = new Thread(ListenForClients);
                listenerThread.IsBackground = true;
                listenerThread.Start();
            }
            catch (Exception ex)
            {
                ServerStatus.Text = "Error: " + ex.Message;
                ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void ListenForClients()
        {
            try
            {
                while (true)
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    Dispatcher.Invoke(() =>
                    {
                        ServerStatus.Text = "Client connected!";
                        ServerStatus.Foreground = System.Windows.Media.Brushes.Green;
                    });

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
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

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                // Add client to the list
                _clients.Add(client);
                _clientWriters.Add(writer);

                // First, read the username from the client
                string username = reader.ReadLine();
                Dispatcher.Invoke(() =>
                {
                    ServerMessages.Text += "New user connected: " + username + "\n";
                });

                // Send a welcome message to the client
                writer.WriteLine("OK");
                writer.Flush();

                // Broadcast message to all clients when a new client joins
                BroadcastMessage("Server: " + username + " has joined the chat.");

                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    // Handle the message from the client (display it in the server UI)
                    Dispatcher.Invoke(() =>
                    {
                        ServerMessages.Text += username + ": " + message + "\n";
                    });
                    // Broadcast the message to all other clients
                    BroadcastMessage(username + ": " + message);
                }

                // Remove client from the list when it disconnects
                _clients.Remove(client);
                _clientWriters.Remove(writer);

                client.Close();
                Dispatcher.Invoke(() =>
                {
                    ServerStatus.Text = "Client disconnected.";
                    ServerStatus.Foreground = System.Windows.Media.Brushes.Orange;
                });

                // Broadcast message to all clients when someone leaves
                BroadcastMessage(username + " has left the chat.");
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ServerStatus.Text = "Error during communication: " + ex.Message;
                    ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
                });
            }
        }

        // Method to broadcast a message to all connected clients
        private void BroadcastMessage(string message)
        {
            try
            {
                foreach (StreamWriter writer in _clientWriters)
                {
                    // Send the message to each connected client
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ServerStatus.Text = "Error while broadcasting: " + ex.Message;
                    ServerStatus.Foreground = System.Windows.Media.Brushes.Red;
                });
            }
        }
    }
}
