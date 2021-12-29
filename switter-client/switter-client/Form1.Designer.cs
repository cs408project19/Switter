namespace switter_client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label_username = new System.Windows.Forms.Label();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_requestFollowing = new System.Windows.Forms.Button();
            this.textBox_follow = new System.Windows.Forms.TextBox();
            this.button_follow = new System.Windows.Forms.Button();
            this.button_getAllSweets = new System.Windows.Forms.Button();
            this.button_getUsers = new System.Windows.Forms.Button();
            this.button_getFollows = new System.Windows.Forms.Button();
            this.button_getFollowers = new System.Windows.Forms.Button();
            this.button_block = new System.Windows.Forms.Button();
            this.textBox_block = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button_getMySweets = new System.Windows.Forms.Button();
            this.button_deleteSweet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(89, 40);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(88, 20);
            this.textBox_ip.TabIndex = 3;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(89, 73);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(88, 20);
            this.textBox_port.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(59, 152);
            this.button_connect.Margin = new System.Windows.Forms.Padding(2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(70, 22);
            this.button_connect.TabIndex = 6;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.HideSelection = false;
            this.logs.Location = new System.Drawing.Point(384, 27);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(363, 360);
            this.logs.TabIndex = 8;
            this.logs.Text = "";
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(187, 202);
            this.button_send.Margin = new System.Windows.Forms.Padding(2);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(65, 26);
            this.button_send.TabIndex = 10;
            this.button_send.Text = "Sweet";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 209);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message:";
            // 
            // textBox_message
            // 
            this.textBox_message.Enabled = false;
            this.textBox_message.Location = new System.Drawing.Point(72, 206);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(98, 20);
            this.textBox_message.TabIndex = 9;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(89, 108);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(88, 20);
            this.textBox_username.TabIndex = 5;
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(25, 108);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(58, 13);
            this.label_username.TabIndex = 13;
            this.label_username.Text = "Username:";
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(151, 151);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(75, 23);
            this.button_disconnect.TabIndex = 14;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_requestFollowing
            // 
            this.button_requestFollowing.Enabled = false;
            this.button_requestFollowing.Location = new System.Drawing.Point(13, 325);
            this.button_requestFollowing.Name = "button_requestFollowing";
            this.button_requestFollowing.Size = new System.Drawing.Size(109, 26);
            this.button_requestFollowing.TabIndex = 15;
            this.button_requestFollowing.Text = "Followings\' Sweets";
            this.button_requestFollowing.UseVisualStyleBackColor = true;
            this.button_requestFollowing.Click += new System.EventHandler(this.button_requestFollowing_Click);
            // 
            // textBox_follow
            // 
            this.textBox_follow.Enabled = false;
            this.textBox_follow.Location = new System.Drawing.Point(72, 240);
            this.textBox_follow.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_follow.Name = "textBox_follow";
            this.textBox_follow.Size = new System.Drawing.Size(98, 20);
            this.textBox_follow.TabIndex = 16;
            // 
            // button_follow
            // 
            this.button_follow.Enabled = false;
            this.button_follow.Location = new System.Drawing.Point(187, 236);
            this.button_follow.Margin = new System.Windows.Forms.Padding(2);
            this.button_follow.Name = "button_follow";
            this.button_follow.Size = new System.Drawing.Size(65, 26);
            this.button_follow.TabIndex = 17;
            this.button_follow.Text = "Follow";
            this.button_follow.UseVisualStyleBackColor = true;
            this.button_follow.Click += new System.EventHandler(this.button_follow_Click);
            // 
            // button_getAllSweets
            // 
            this.button_getAllSweets.Enabled = false;
            this.button_getAllSweets.Location = new System.Drawing.Point(13, 357);
            this.button_getAllSweets.Name = "button_getAllSweets";
            this.button_getAllSweets.Size = new System.Drawing.Size(109, 26);
            this.button_getAllSweets.TabIndex = 18;
            this.button_getAllSweets.Text = "All Sweets";
            this.button_getAllSweets.UseVisualStyleBackColor = true;
            this.button_getAllSweets.Click += new System.EventHandler(this.button_getAllSweets_Click);
            // 
            // button_getUsers
            // 
            this.button_getUsers.Enabled = false;
            this.button_getUsers.Location = new System.Drawing.Point(128, 325);
            this.button_getUsers.Name = "button_getUsers";
            this.button_getUsers.Size = new System.Drawing.Size(91, 26);
            this.button_getUsers.TabIndex = 19;
            this.button_getUsers.Text = "All Users";
            this.button_getUsers.UseVisualStyleBackColor = true;
            this.button_getUsers.Click += new System.EventHandler(this.button_getUsers_Click);
            // 
            // button_getFollows
            // 
            this.button_getFollows.Enabled = false;
            this.button_getFollows.Location = new System.Drawing.Point(225, 357);
            this.button_getFollows.Name = "button_getFollows";
            this.button_getFollows.Size = new System.Drawing.Size(91, 26);
            this.button_getFollows.TabIndex = 20;
            this.button_getFollows.Text = "Users Followed";
            this.button_getFollows.UseVisualStyleBackColor = true;
            this.button_getFollows.Click += new System.EventHandler(this.button_getFollows_Click);
            // 
            // button_getFollowers
            // 
            this.button_getFollowers.Enabled = false;
            this.button_getFollowers.Location = new System.Drawing.Point(225, 325);
            this.button_getFollowers.Name = "button_getFollowers";
            this.button_getFollowers.Size = new System.Drawing.Size(91, 26);
            this.button_getFollowers.TabIndex = 21;
            this.button_getFollowers.Text = "Followers";
            this.button_getFollowers.UseVisualStyleBackColor = true;
            this.button_getFollowers.Click += new System.EventHandler(this.button_getFollowers_Click);
            // 
            // button_block
            // 
            this.button_block.Enabled = false;
            this.button_block.Location = new System.Drawing.Point(187, 270);
            this.button_block.Margin = new System.Windows.Forms.Padding(2);
            this.button_block.Name = "button_block";
            this.button_block.Size = new System.Drawing.Size(65, 26);
            this.button_block.TabIndex = 23;
            this.button_block.Text = "Block";
            this.button_block.UseVisualStyleBackColor = true;
            this.button_block.Click += new System.EventHandler(this.button_block_Click);
            // 
            // textBox_block
            // 
            this.textBox_block.Enabled = false;
            this.textBox_block.Location = new System.Drawing.Point(72, 274);
            this.textBox_block.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_block.Name = "textBox_block";
            this.textBox_block.Size = new System.Drawing.Size(98, 20);
            this.textBox_block.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Username:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Username:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(384, 420);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(362, 121);
            this.listBox1.TabIndex = 26;
            // 
            // button_getMySweets
            // 
            this.button_getMySweets.Enabled = false;
            this.button_getMySweets.Location = new System.Drawing.Point(12, 456);
            this.button_getMySweets.Name = "button_getMySweets";
            this.button_getMySweets.Size = new System.Drawing.Size(91, 26);
            this.button_getMySweets.TabIndex = 27;
            this.button_getMySweets.Text = "Get My Sweets";
            this.button_getMySweets.UseVisualStyleBackColor = true;
            this.button_getMySweets.Click += new System.EventHandler(this.button_getMySweets_Click);
            // 
            // button_deleteSweet
            // 
            this.button_deleteSweet.Enabled = false;
            this.button_deleteSweet.Location = new System.Drawing.Point(109, 456);
            this.button_deleteSweet.Name = "button_deleteSweet";
            this.button_deleteSweet.Size = new System.Drawing.Size(131, 26);
            this.button_deleteSweet.TabIndex = 28;
            this.button_deleteSweet.Text = "Delete Selected Sweet";
            this.button_deleteSweet.UseVisualStyleBackColor = true;
            this.button_deleteSweet.Click += new System.EventHandler(this.button_deleteSweet_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(381, 404);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "My Sweets";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(381, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Feed";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 559);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_deleteSweet);
            this.Controls.Add(this.button_getMySweets);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_block);
            this.Controls.Add(this.textBox_block);
            this.Controls.Add(this.button_getFollowers);
            this.Controls.Add(this.button_getFollows);
            this.Controls.Add(this.button_getUsers);
            this.Controls.Add(this.button_getAllSweets);
            this.Controls.Add(this.button_follow);
            this.Controls.Add(this.textBox_follow);
            this.Controls.Add(this.button_requestFollowing);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Name = "Form1";
            this.Text = "Switter-Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_requestFollowing;
        private System.Windows.Forms.TextBox textBox_follow;
        private System.Windows.Forms.Button button_follow;
        private System.Windows.Forms.Button button_getAllSweets;
        private System.Windows.Forms.Button button_getUsers;
        private System.Windows.Forms.Button button_getFollows;
        private System.Windows.Forms.Button button_getFollowers;
        private System.Windows.Forms.Button button_block;
        private System.Windows.Forms.TextBox textBox_block;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_getMySweets;
        private System.Windows.Forms.Button button_deleteSweet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

