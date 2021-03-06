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
        struct userData{
            public string username;
            public List<string> following;
            public List<string> blocked;
        }


        bool terminating = false;   
        bool listening = false;
        List<string> users;            // All saved users
        List<userData> userDatas = new List<userData>();
        List<string> connectedUsers = new List<string>();   // List of all connected users
        List<string> sweets = new List<string>();           // List of all sweets


        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();

            // Add all users in txt to users list
            users = File.ReadAllLines("../../user-db.txt").ToList<string>();

            // Load previously saved sweets from txt file
            if (!File.Exists("sweets.txt"))
            {
                FileStream fs = File.OpenWrite("sweets.txt");
                fs.Close();
            }
                sweets = File.ReadAllLines("sweets.txt").ToList<string>();

            foreach(string user in users)
            {
                userData newData = new userData();
                newData.username = user;
                newData.following = new List<string>();
                newData.blocked = new List<string>();
                userDatas.Add(newData);
            }

            if (!File.Exists("followers.txt"))
            {
                FileStream fs = File.OpenWrite("followers.txt");
                fs.Close();

                UpdateFollowerFile();
            }

            if (!File.Exists("blocked.txt"))
            {
                FileStream fs = File.OpenWrite("blocked.txt");
                fs.Close();


                UpdateBlockedFile();
            }

            // Load previously saved following data from txt file
            List<string> followersContent = File.ReadAllLines("followers.txt").ToList<string>();
            foreach(string line in followersContent)
            {
                string username = line.Substring(0, line.IndexOf(':'));

                userData u = userDatas.Find(x => x.username == username);

                string followingStr = line.Substring(line.IndexOf(':') + 1);
                if(followingStr != "")
                    u.following.AddRange(followingStr.Split(',').ToList<string>());
            }

            List<string> blockedContent = File.ReadAllLines("blocked.txt").ToList<string>();
            foreach (string line in blockedContent)
            {
                string username = line.Substring(0, line.IndexOf(':'));
                userData u = userDatas.Find(x => x.username == username);


                string blockedStr = line.Substring(line.IndexOf(':') + 1);
                if (blockedStr != "")
                    u.blocked.AddRange(blockedStr.Split(',').ToList<string>());
            }
        }

        // initialize the server
        private void button_start_Click(object sender, EventArgs e)
        {
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
                richtextbox_log.AppendText("Please check port number. \n");
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


                    // get the username as a message first
                    Byte[] buffer = new Byte[128];
                    newClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));


                    richtextbox_log.AppendText(username + " is trying to connect.\n");

                    if (users.IndexOf(username) != -1)        // If the username exists in the users list
                    {
                        if (connectedUsers.Exists(x => x.Equals(username)))     // If the same username tries to connect
                        {   
                            richtextbox_log.AppendText("User is already connected.\n");
                            Byte[] sweetsBuffer = Encoding.Default.GetBytes("This user is already connected.");
                            newClient.Send(sweetsBuffer);
                            newClient.Close();
                            clientSockets.Remove(newClient);
                        }
                        else            
                        {
                            // if user is not logged in and logged successfully now
                            connectedUsers.Add(username);
                            richtextbox_log.AppendText(username + " connected.\n");
                            Byte[] sweetsBuffer = Encoding.Default.GetBytes("Connected successfully.");
                            newClient.Send(sweetsBuffer);
                            Thread receiveThread = new Thread(() => Receive(newClient, username)); // updated
                            receiveThread.Start();
                        }
                    }
                    else        
                    {
                        // if user does not exist in the database
                        richtextbox_log.AppendText("User does not exist!\n");
                        Byte[] sweetsBuffer = Encoding.Default.GetBytes("This user does not exist.");
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

            File.AppendAllText("sweets.txt", sweet + "\n");
        }

        void RemoveSweet(string sweetID)
        {
            sweets.Remove(sweets.Find(x => x.Split(';')[0] == sweetID));
            File.WriteAllLines("sweets.txt", sweets);
        }

        void UpdateFollowerFile()
        {
            string allData = "";

            foreach(userData u in userDatas)
            {
                allData += u.username + ":" + String.Join(",", u.following) + "\n";
            }
            File.WriteAllText("followers.txt", allData);
        }

        void UpdateBlockedFile()
        {
            string allData = "";

            foreach (userData u in userDatas)
            {
                allData += u.username + ":" + String.Join(",", u.blocked) + "\n";
            }
            File.WriteAllText("blocked.txt", allData);
        }

        public DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        string formatSweet(string sw)
        {
            string formattedSweet = "[" + sw.Split(';')[0] + "] ";
            formattedSweet += "[" + UnixTimeToDateTime(long.Parse(sw.Split(';')[1])) + "] ";
            formattedSweet += sw.Split(';')[2] + ": ";
            formattedSweet += sw.Substring(sw.IndexOf(';', sw.IndexOf(';', sw.IndexOf(';') + 1) + 1) + 1);
            return formattedSweet;
        }

        string getAllSweets(string user)
        {
            string s = "";
            foreach (string sweet in sweets)        // Accumulate all sweets in one string
                if (sweet.Split(';')[2] != user && !userDatas.Find(x => x.username == sweet.Split(';')[2]).blocked.Contains(user)) 
                    s += formatSweet(sweet) + "\n";
            s = s == "" ? "No sweets found.\n" : "Here is all tweets: \n" + s; 
            return s;
        }

        string getAllFollowingSweets(string user)
        {
            string s = "";
            userData u = userDatas.Find(x => x.username == user);
            foreach (string sweet in sweets.FindAll(x => u.following.Contains(x.Split(';')[2]) && !userDatas.Find(b => b.username == x.Split(';')[2]).blocked.Contains(user)))
                s += formatSweet(sweet) + "\n";
            s = s == "" ? "No sweets found.\n" : "Here is all tweets: \n" + s;
            return s;
        }

        string getUserSweets(string user)
        {
            string s = "";
            foreach (string sweet in sweets.FindAll(x => x.Split(';')[2] == user))
                s += formatSweet(sweet) + "\n";
            return s;
        }

        List<string> getFollows(string user)
        {
            return userDatas.Find(x => x.username == user).following;
        }

        List<string> getFollowers(string user)
        {
            return userDatas.FindAll(x => x.following.Contains(user)).Select(x=>x.username).ToList();
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

                    if (incomingMessage == "requestAllSweets")       // if "request" message comes from client(user), print all the sweets except user's sweets
                    {
                        richtextbox_log.AppendText(user +" requested all sweets.\n");

                        Byte[] sweetsBuffer = Encoding.Default.GetBytes(getAllSweets(user));
                        thisClient.Send(sweetsBuffer);
                    }

                    else if (incomingMessage == "requestFollowingSweets")
                    {
                        richtextbox_log.AppendText(user + " requested all sweets posted by followings.\n");

                        Byte[] sweetsBuffer = Encoding.Default.GetBytes(getAllFollowingSweets(user));
                        thisClient.Send(sweetsBuffer);
                    }

                    else if (incomingMessage == "requestMySweets")
                    {
                        richtextbox_log.AppendText(user + " requested own sweets.\n");

                        Byte[] sweetsBuffer = Encoding.Default.GetBytes("own:" + getUserSweets(user));
                        thisClient.Send(sweetsBuffer);
                    }

                    // if user sends disconnect command, removes from connectedUsers
                    else if(incomingMessage == "disconnect")
                    {
                        thisClient.Close();
                        connectedUsers.Remove(user);
                        clientSockets.Remove(thisClient);
                        connected = false;
                        richtextbox_log.AppendText(user + " disconnected\n");
                    }

                    //get all users
                    else if (incomingMessage == "getAllUsers") {
                        richtextbox_log.AppendText(user + " requested all users.\n");

                        Byte[] usersBuffer = Encoding.Default.GetBytes("All users:\n" + String.Join("\n", users.ToArray()));
                        thisClient.Send(usersBuffer);
                    }

                    else if (incomingMessage == "getFollows")
                    {
                        richtextbox_log.AppendText(user + " requested followed users.\n");

                        string[] follows = getFollows(user).ToArray();
                        Byte[] usersBuffer;

                        if(follows.Length>0)
                            usersBuffer = Encoding.Default.GetBytes("Followed users:\n" + String.Join("\n", follows));
                        else
                            usersBuffer = Encoding.Default.GetBytes("You don't follow anyone!\n");
                        thisClient.Send(usersBuffer);
                    }

                    else if (incomingMessage == "getFollowers")
                    {
                        richtextbox_log.AppendText(user + " requested followers.\n");

                        string[] followers = getFollowers(user).ToArray();
                        Byte[] usersBuffer;

                        if (followers.Length > 0)
                            usersBuffer = Encoding.Default.GetBytes("Followers:\n" + String.Join("\n", followers));
                        else
                            usersBuffer = Encoding.Default.GetBytes("Nobody is following you!\n");
                        thisClient.Send(usersBuffer);
                    }

                    //follow a user
                    else if (incomingMessage.StartsWith("follow:"))
                    {
                        //remove the "follow:" part from message
                        string userToFollow = incomingMessage.Substring(incomingMessage.IndexOf(':') + 1);
                        //find the correct userData
                        userData u = userDatas.Find(x => x.username == user);
                        
                        if(userToFollow == user)
                        {
                            richtextbox_log.AppendText(user + " tried follow himself/herself.\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You cannot follow yourself.");
                            thisClient.Send(usersBuffer);
                        }
                        //check if the user is already following the other user
                        else if(users.IndexOf(userToFollow)==-1)
                        {
                            richtextbox_log.AppendText(userToFollow + " does not exist.\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes(userToFollow + " does not exist.");
                            thisClient.Send(usersBuffer);
                        }
                        else if(u.following.IndexOf(userToFollow) != -1)
                        {
                            richtextbox_log.AppendText(user + " is already following " + userToFollow + ".\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You are already following " + userToFollow + ".");
                            thisClient.Send(usersBuffer);
                        }
                        else if(userDatas.Find(x=>x.username==userToFollow).blocked.Contains(user))
                        {
                            richtextbox_log.AppendText(userToFollow + " is blocked " + user + ", so can't follow.\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You can't follow a user that has blocked you. (" + userToFollow + ")");
                            thisClient.Send(usersBuffer);
                        }
                        else
                        {
                            u.following.Add(userToFollow);
                            UpdateFollowerFile();
                            richtextbox_log.AppendText(user + " is now following " + userToFollow + ".\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You are now following " + userToFollow + ".");
                            thisClient.Send(usersBuffer);
                        }       
                    }

                    else if (incomingMessage.StartsWith("block:"))
                    {
                        //remove the "follow:" part from message
                        string userToBlock = incomingMessage.Substring(incomingMessage.IndexOf(':') + 1);

                        //find the correct userData
                        userData u = userDatas.Find(x => x.username == user);

                        if (userToBlock == user)
                        {
                            richtextbox_log.AppendText(user + " tried block himself/herself.\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You cannot block yourself.");
                            thisClient.Send(usersBuffer);
                        }
                        //check if the user is already following the other user
                        else if (users.IndexOf(userToBlock) == -1)
                        {
                            richtextbox_log.AppendText(userToBlock + " does not exist.\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes(userToBlock + " does not exist.");
                            thisClient.Send(usersBuffer);
                        }
                        else if (u.blocked.IndexOf(userToBlock) != -1)
                        {
                            richtextbox_log.AppendText(user + " is already blocked " + userToBlock + ".\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You have already blocked " + userToBlock + ".");
                            thisClient.Send(usersBuffer);
                        }
                        else
                        {
                            u.blocked.Add(userToBlock);
                            userDatas.Find(x => x.username == userToBlock).following.Remove(user);
                            UpdateBlockedFile();
                            UpdateFollowerFile();
                            richtextbox_log.AppendText(user + " blocked " + userToBlock + ".\n");
                            Byte[] usersBuffer = Encoding.Default.GetBytes("You are now blocked " + userToBlock + ".");
                            thisClient.Send(usersBuffer);
                        }
                    }

                    else if (incomingMessage.StartsWith("msg:")) {
                        // putting new sweet to correct form and adds to database
                        string msg = incomingMessage.Substring(4);

                        string lastID = sweets.Count > 0 ? sweets.Last().Split(';')[0].Trim(new char[] { '[', ']' }) : "0";

                        string sweetID = (Int32.Parse(lastID) + 1).ToString();
                        string sweetTime = ((long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                        string sweet = sweetID + ";" + sweetTime + ";" + user + ";" + msg;
                        AddSweet(sweet);
                        richtextbox_log.AppendText(formatSweet(sweet) + "\n");
                            
                        Byte[] sweetsBuffer = Encoding.Default.GetBytes("Message received.");
                        thisClient.Send(sweetsBuffer);
                    }
                    
                    else if (incomingMessage.StartsWith("delete:"))
                    {
                        string sweetID = incomingMessage.Substring(7);
                        
                        RemoveSweet(sweetID);
                    }

                    else
                        richtextbox_log.AppendText("Invalid request!\n");

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
