# 🚀 ZeroDayCrew ChatRoom

This repository contains the implementation of a server-client chatroom system built using C# and WPF. The application allows multiple users to connect to a central server and exchange messages in real-time.  
Designed for a chatroom application with a hacker-inspired theme. 🕶️

---

## ✨ Features

- **🛠️ Server Management**:
  - Start and configure the server with custom IP and port.
  - Display server logs for monitoring connections and activities.

- **💬 Client Chatroom**:
  - User-friendly chat interface.
  - Displays connected users in a sidebar.
  - Supports sending and receiving messages in real-time.

---

## 📂 Files and Structure

### Server Components

#### `MainWindow.xaml.cs`
- Handles the server's main functionalities, such as starting the server with user-defined configurations.
- Includes validation for IP addresses and ports to ensure proper server setup.
- Triggers the `ServerLogs` window to monitor activities.
- Provides user-friendly interactions with placeholder text management for input fields.

#### `ServerLogs.xaml`
- Displays logs for all server-side activities, including connections, disconnections, and message exchanges.

### Client Components

#### `MainWindow.xaml`
- 🖥️ A welcome screen for clients that asks users for confirmation.

#### `ChatSection.xaml`
- Represents the main chatroom interface for the client.
- Includes:
  - 🟢 Sidebar for displaying connected users.
  - 📜 Main chat area for messages.
  - 📝 Textbox at the bottom for typing and sending messages.

---

## 🚀 Usage

### 🖥️ Server Setup

1. Launch the server application.
2. Enter the desired server IP and port in the configuration fields.
3. Click the "Start Server" button to initialize the server.
4. Monitor server logs in the `ServerLogs` window.

### 💻 Client Usage

1. Open the client application.
2. Connect to the server using the server IP and port.
3. Begin chatting with other connected users.

---

## 🛠️ Requirements

- **Framework**: .NET Framework 4.7.2 or later (Recommended: 9.0).
- **Environment**: Visual Studio for building and running the project.

---

## 🏃 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/0kraven/ZeroDayCrew
   ```
2. Open the project in Visual Studio.
3. Build the solution or just run the provided `.exe` in the `Release` folder:
   - For the server: Open the `Server` project and build.
   - For the client: Open the `Chatroom` project and build.
4. Run the server and client applications separately.

---

## 🌟 Contributions

Contributions are welcome! Feel free to fork the repository and submit pull requests.  
💡 **Collaborate, enhance, and share your ideas!**



---

## 😊Future Improvements
Right now i am just postponding this project for a short time because i am exhausted of this project :)

However in future:
- I will add some sounds effects in this application
- I will add optional features like fonts and themes etc.
- I will create setup file for this application as well.
- And who knows what else I will do xD.
