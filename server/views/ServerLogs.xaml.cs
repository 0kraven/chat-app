using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace server.views
{
    public partial class ServerLogs : Window
    {
        private TcpListener _tcpListener;
        private List<TcpClient> _clients = new List<TcpClient>();
        private List<StreamWriter> _clientWriters = new List<StreamWriter>();
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

                Thread listenerThread = new Thread(ListenForClients);
                listenerThread.IsBackground = true;
                listenerThread.Start();
            }
            catch (Exception ex)
            {
                AppendLog("Error starting server: " + ex.Message);
            }
        }

        private void ListenForClients()
        {
            while (_isRunning)
            {
                try
                {
                    var client = _tcpListener.AcceptTcpClient();
                    _clients.Add(client);
                    _clientWriters.Add(new StreamWriter(client.GetStream()));
                    Dispatcher.Invoke(() => ClientList.Items.Add(client.Client.RemoteEndPoint.ToString()));
                    AppendLog("New user connected: " + client.Client.RemoteEndPoint);

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    AppendLog("Error accepting client: " + ex.Message);
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("OK");
            writer.Flush();
            try
            {
                // Read username from client
                string username = reader.ReadLine();
                Dispatcher.Invoke(() => AppendLog("New user connected: " + username));
                BroadcastMessage("Server: " + username + " has joined the chat.");

                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    Dispatcher.Invoke(() => AppendLog(username + ": " + message));
                    BroadcastMessage(username + ": " + message);
                }
            }
            catch (Exception ex)
            {
                AppendLog("Error handling client: " + ex.Message);
            }
            finally
            {
                _clients.Remove(client);
                _clientWriters.Remove(writer);
                Dispatcher.Invoke(() => ClientList.Items.Remove(client.Client.RemoteEndPoint.ToString()));
                client.Close();
                AppendLog("Client disconnected.");
                BroadcastMessage("Server: A user has left the chat.");
            }
        }

        private void BroadcastMessage(string message)
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
                }
            }
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string command = CommandInput.Text;
            if (!string.IsNullOrWhiteSpace(command))
            {
                AppendLog("Server command: " + command);
                BroadcastMessage("Server: " + command);
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
            });
            ScrollViewer.ScrollToEnd();
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
