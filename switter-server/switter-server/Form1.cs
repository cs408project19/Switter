using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace switter_server
{
    public partial class Form1 : Form
    {
        bool terminating = false;   
        bool listening = false;
        List<string> users;            // All saved users
        List<string> connectedUsers = new List<string>();   // List of all connected users
        List<string> sweets = new List<string>();           // List of all sweets

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        void readUserFile()         // Add all users in txt to users list
        {
            users = File.ReadAllLines("user-db.txt").ToList<string>();
        }

        void readSweetsFile()       // When all sweets options called, we read this txt file and print it
        {
            sweets = File.ReadAllLines("sweets.txt").ToList<string>();
        }

        bool userExist(string username)         // Search if this user is in users list
        {
            foreach(string u in users)
            {
                if (u == username)
                    return true;
            }
            return false;
        }

        // server is initialized
        private void button_start_Click(object sender, EventArgs e)
        {
            readUserFile();
            readSweetsFile();

            int serverPort;

            // if port is fine
            if (Int32.TryParse(textbox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_start.Enabled = false;

                // started to accepting users
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                richtextbox_log.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richtextbox_log.AppendText("Please check port number \n");
            }
        }


        // listening for users (clients)
        private void Accept()
        {
            while (listening)
            {
                try
                {
                    // if new user is came
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);

                    Byte[] buffer = new Byte[128];
                    newClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));


                    richtextbox_log.AppendText(username + " is trying to connect\n");

                    if (userExist(username))        // If the username exists in the users list
                    {
                        if (connectedUsers.Exists(x => x.Equals(username)))     // If the same username tries to connect
                        {   
                            richtextbox_log.AppendText("User already connected!\n");
                            Byte[] sweetsBuffer = Encoding.Default.GetBytes("This user is already connected");
                            newClient.Send(sweetsBuffer);
                            newClient.Close();
                            clientSockets.Remove(newClient);
                        }
                        else            
                        {
                            // if user is not logged in and logged successfully now
                            connectedUsers.Add(username);
                            richtextbox_log.AppendText(username + " connected!\n");
                            Byte[] sweetsBuffer = Encoding.Default.GetBytes("Connected successfully");
                            newClient.Send(sweetsBuffer);
                            Thread receiveThread = new Thread(() => Receive(newClient, username)); // updated
                            receiveThread.Start();
                        }
                    }
                    else        
                    {
                        // if user does not exist in the database
                        richtextbox_log.AppendText("User does not exist!\n");
                        Byte[] sweetsBuffer = Encoding.Default.GetBytes("This user does not exist");
                        newClient.Send(sweetsBuffer);
                        newClient.Close();
                        clientSockets.Remove(newClient);
                    }

                   
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richtextbox_log.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }
        // adding new sweet to database
        void AddSweet(string sweet)
        {
            sweets.Add(sweet);

            File.WriteAllLines("sweets.txt", sweets);
        }

        // listening for new commands from users
        private void Receive(Socket thisClient, string user) // updated
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (incomingMessage == "request")       // if "request" message comes from client(user), print all the sweets except user's sweets
                    {
                        string allSweets = "";
                        foreach(string sweet in sweets)        // Accumulate all sweets in one string
                        {
                            if (sweet.Split(';')[0] != user)
                            {
                                allSweets += sweet + "\n";
                            }
                        }

                        richtextbox_log.AppendText(user +" requested all sweets.\n");

                        Byte[] sweetsBuffer = Encoding.Default.GetBytes(allSweets);
                        thisClient.Send(sweetsBuffer);
                    }
                    // if user sends disconnect command, removes from connectedUsers
                    else if(incomingMessage == "disconnect")
                    {
                        thisClient.Close();
                        connectedUsers.Remove(user);
                        clientSockets.Remove(thisClient);
                        connected = false;
                    }

                    else
                    {
                        // putting new sweet to correct form and adds to database
                        if(incomingMessage.Split(';').Length >= 2) { 
                            user = incomingMessage.Split(';')[0];
                            string msg = incomingMessage.Split(';')[1];

                            if (userExist(user))
                            {
                                string sweet = user + ";" + msg + ";" + DateTime.Now.ToUniversalTime().ToString() + ";" + (sweets.Count + 1).ToString();
                                AddSweet(sweet);
                                richtextbox_log.AppendText(sweet + "\n");
                            }
                            else
                            {
                                richtextbox_log.AppendText("Invalid username!\n");
                            }
                        }

                        else
                        {
                            richtextbox_log.AppendText("Invalid message!\n");
                        }
                    }

                }
                catch
                {
                    // if user disconnects by closing the program without clicking disconnect
                    if (!terminating)
                    {
                        richtextbox_log.AppendText(user + " has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connectedUsers.Remove(user);
                    connected = false;
                }
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
    }
}
