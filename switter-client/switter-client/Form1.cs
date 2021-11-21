using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace switter_client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        string username;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            username = textBox_username.Text;

            int portNum;

            if (username == "")
            {
                logs.AppendText("Username cannot be empty !!\r\n");
            }
            
            else if (Int32.TryParse(textBox_port.Text, out portNum) && IP != "")
            {

                try
                {
                    clientSocket.Connect(IP, portNum);
                    button_connect.Enabled = false;
                    button_send.Enabled = true;
                    connected = true;
                    logs.AppendText("Trying to connect the server!\n");

                    Byte[] buffer = Encoding.Default.GetBytes(username);
                    clientSocket.Send(buffer);

                    Console.WriteLine("sent username");

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\r\n");
                }
            }
            else
            {
                logs.AppendText("Check the port and IP number\n");
            }

        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[4096];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (incomingMessage.Equals("This user does not exist") || incomingMessage.Equals("This user is already connected"))
                    {
                        logs.AppendText("Server: " + incomingMessage + "\n");
                        connected = false;
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;

                    } else if (incomingMessage.Equals("Connected successfully")){
                        logs.AppendText("Server: " + incomingMessage + "\n");
                        button_disconnect.Enabled = true;
                        textBox_message.Enabled = true;
                        button_request.Enabled = true;
                        button_connect.Enabled = false;

                    }
                    else if(incomingMessage != "")
                    {
                        logs.AppendText("Server: " + incomingMessage + "\n");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        button_request.Enabled = false;
                        button_disconnect.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

      

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = username + ";" + textBox_message.Text;

            if (message != "")
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
                textBox_message.Text = "";
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Byte[] buffer = Encoding.Default.GetBytes("disconnect");
                clientSocket.Send(buffer);
                
            }
            catch
            {
                logs.AppendText("Error trying to disconnect from the server\n");
            }
            finally
            {
                logs.AppendText("Disconnected from the server\n");
                connected = false;
                button_connect.Enabled = true;
                textBox_message.Enabled = false;
                button_send.Enabled = false;
                button_disconnect.Enabled = false;
                button_request.Enabled = false;
                clientSocket.Close();
            }
           
        }

        private void button_request_Click(object sender, EventArgs e)
        {
            logs.AppendText("Requested all sweets from the server\n");
            Byte[] buffer = Encoding.Default.GetBytes("request");
            clientSocket.Send(buffer);

        }
    }
}
