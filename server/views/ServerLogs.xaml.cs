using System;
using System.Windows;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace server.views
{
    public partial class ServerLogs : Window
    {
        private TcpListener _tcpListener;
        private List<TcpClient> _clients = new List<TcpClient>();
        private List<StreamWriter> _clientWriters = new List<StreamWriter>();
        private Dictionary<TcpClient, string> _clientUsernames = new Dictionary<TcpClient, string>();
        private bool _isRunning = false;

        public ServerLogs()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            try
            {
                _tcpListener = new TcpListener(System.Net.IPAddress.Any, 5000);
                _tcpListener.Start();
                _isRunning = true;
                AppendLog("Server started on port 5000.");

                // Start the listener thread
                Thread listenerThread = new Thread(ListenForClients)
                {
                    IsBackground = true
                };
                listenerThread.Start();
            }
            catch (Exception ex)
            {
                AppendLog("Error starting server: " + ex.Message);
            }
        }

        private void ListenForClients()
        {
            // Start broadcasting the client list
            Thread clientListBroadcasterThread = new Thread(BroadcastClientListContinuously)
            {
                IsBackground = true
            };
            clientListBroadcasterThread.Start();

            while (_isRunning)
            {
                try
                {
                    var client = _tcpListener.AcceptTcpClient();
                    _clients.Add(client);

                    var writer = new StreamWriter(client.GetStream());
                    lock (_clientWriters)
                    {
                        _clientWriters.Add(writer);
                    }

                    Dispatcher.Invoke(() => ClientList.Items.Add(client.Client.RemoteEndPoint.ToString()));
                    AppendLog("New user connected: " + client.Client.RemoteEndPoint);

                    // Start a new thread to handle the client
                    Thread clientThread = new Thread(() => HandleClient(client, writer))
                    {
                        IsBackground = true
                    };
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    AppendLog("Error accepting client: " + ex.Message);
                }
            }
        }

        private void HandleClient(TcpClient client, StreamWriter writer)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);

            try
            {
                // Receive username from client
                string username = reader.ReadLine();
                lock (_clientUsernames)
                {
                    _clientUsernames[client] = username;
                }

                // Send welcome message to the client
                writer.WriteLine("Welcome " + username + "!");
                writer.Flush();

                // Log the new connection
                Dispatcher.Invoke(() => AppendLog("New user connected: " + username));
                BroadcastMessage($"Server: {username} has joined the chat.");

                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    // Ensure message is broadcasted only once
                    Dispatcher.Invoke(() => AppendLog($"{username}: {message}"));
                    BroadcastMessage($"{username}: {message}");
                }
            }
            catch (Exception ex)
            {
                AppendLog("Error handling client: " + ex.Message);
            }
            finally
            {
                // Clean up when client disconnects
                lock (_clients)
                {
                    _clients.Remove(client);
                }
                lock (_clientWriters)
                {
                    _clientWriters.Remove(writer);
                }

                // Broadcast user departure message
                if (_clientUsernames.ContainsKey(client))
                {
                    string disconnectedUser;
                    lock (_clientUsernames)
                    {
                        disconnectedUser = _clientUsernames[client];
                        _clientUsernames.Remove(client);
                    }
                    BroadcastMessage($"Server: {disconnectedUser} has left the chat.");
                }

                // Update client list UI
                Dispatcher.Invoke(() => ClientList.Items.Remove(client.Client.RemoteEndPoint.ToString()));

                // Close the connection
                client.Close();
                AppendLog("Client disconnected.");
            }
        }

        private void BroadcastMessage(string message)
        {
            List<StreamWriter> failedWriters = new List<StreamWriter>();

            lock (_clientWriters)
            {
                foreach (var writer in _clientWriters)
                {
                    try
                    {
                        writer.WriteLine(message);
                        writer.Flush();
                    }
                    catch (Exception ex)
                    {
                        AppendLog("Error broadcasting message: " + ex.Message);
                        failedWriters.Add(writer); // Track failed writers
                    }
                }

                // Remove failed writers after broadcasting
                foreach (var failedWriter in failedWriters)
                {
                    _clientWriters.Remove(failedWriter);
                }
            }
        }

        private void BroadcastClientListContinuously()
        {
            try
            {
                while (_isRunning)
                {
                    List<StreamWriter> failedWriters = new List<StreamWriter>();

                    // Lock client usernames and writers while broadcasting the list
                    lock (_clientUsernames)
                    {
                        var clientList = new List<string>(_clientUsernames.Values);
                        string clientListJson = System.Text.Json.JsonSerializer.Serialize(clientList);

                        lock (_clientWriters)
                        {
                            foreach (var writer in _clientWriters)
                            {
                                try
                                {
                                    writer.WriteLine("CLIENT_LIST:" + clientListJson);
                                    writer.Flush();
                                }
                                catch (Exception ex)
                                {
                                    AppendLog("Error broadcasting client list: " + ex.Message);
                                    failedWriters.Add(writer);
                                }
                            }

                            // Remove failed writers after broadcasting
                            foreach (var failedWriter in failedWriters)
                            {
                                _clientWriters.Remove(failedWriter);
                            }
                        }
                    }

                    Thread.Sleep(1000); // Broadcast every second
                }
            }
            catch (Exception ex)
            {
                AppendLog("Error in client list broadcaster: " + ex.Message);
            }
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string command = CommandInput.Text;
            if (!string.IsNullOrWhiteSpace(command))
            {
                AppendLog("Admin: " + command);
                BroadcastMessage("Admin: " + command);
                CommandInput.Clear();
            }
        }

        private void AppendLog(string message)
        {
            Dispatcher.Invoke(() =>
            {
                ServerLogsPanel.Children.Add(new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.LightGreen,
                    FontFamily = new FontFamily("Courier New"),
                    Margin = new Thickness(5)
                });
                ScrollViewer.ScrollToEnd();
            });
        }

        private void CommandInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CommandInput.Text == "Enter server command")
            {
                CommandInput.Text = "";
                CommandInput.Foreground = Brushes.White;
            }
        }

        private void CommandInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommandInput.Text))
            {
                CommandInput.Text = "Enter server command";
                CommandInput.Foreground = Brushes.Gray;
            }
        }
    }
}
