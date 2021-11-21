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
        List<string> users;
        List<string> connectedUsers = new List<string>();
        List<string> sweets = new List<string>();

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        void readUserFile()
        {
            users = File.ReadAllLines("user-db.txt").ToList<string>();
        }

        void readSweetsFile()
        {
            sweets = File.ReadAllLines("sweets.txt").ToList<string>();
        }

        bool userExist(string username)
        {
            foreach(string u in users)
            {
                if (u == username)
                    return true;
            }
            return false;
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            readUserFile();
            readSweetsFile();

            int serverPort;

            if (Int32.TryParse(textbox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_start.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                richtextbox_log.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richtextbox_log.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);

                    Byte[] buffer = new Byte[128];
                    newClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));


                    richtextbox_log.AppendText(incomingMessage + " is trying to connect\n");

                    if (userExist(incomingMessage))
                    {
                        if (connectedUsers.Exists(x => x.Equals(incomingMessage))) {
                            richtextbox_log.AppendText("User already connected!\n");
                            newClient.Close();
                            clientSockets.Remove(newClient);
                        }
                        else
                        {
                            connectedUsers.Add(incomingMessage);
                            richtextbox_log.AppendText(incomingMessage + " connected!\n");
                        }
                    }
                    else
                    {
                        richtextbox_log.AppendText("User does not exist!\n");
                        newClient.Close();
                        clientSockets.Remove(newClient);
                    }

                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.Start();
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

        void AddSweet(string sweet)
        {
            sweets.Add(sweet);

            File.WriteAllLines("sweets.txt", sweets);
        }

        private void Receive(Socket thisClient) // updated
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

                    if (incomingMessage == "request")
                    {
                        string allSweets = "";
                        foreach(string sweet in sweets)
                        {
                            allSweets += sweet + "\n";
                        }

                        richtextbox_log.AppendText("A client requested all sweets.\n");

                        Byte[] sweetsBuffer = Encoding.Default.GetBytes(allSweets);
                        thisClient.Send(sweetsBuffer);
                    }

                    else
                    {
                        if(incomingMessage.Split(';').Length >= 2) { 
                            string user = incomingMessage.Split(';')[0];
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
                    if (!terminating)
                    {
                        richtextbox_log.AppendText("A client has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
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
