namespace switter_server
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
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_port = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.richtextbox_log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Port:";
            // 
            // textbox_port
            // 
            this.textbox_port.Location = new System.Drawing.Point(82, 32);
            this.textbox_port.Name = "textbox_port";
            this.textbox_port.Size = new System.Drawing.Size(100, 20);
            this.textbox_port.TabIndex = 1;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(203, 31);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // richtextbox_log
            // 
            this.richtextbox_log.Location = new System.Drawing.Point(16, 72);
            this.richtextbox_log.Name = "richtextbox_log";
            this.richtextbox_log.Size = new System.Drawing.Size(601, 300);
            this.richtextbox_log.TabIndex = 3;
            this.richtextbox_log.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 433);
            this.Controls.Add(this.richtextbox_log);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.textbox_port);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Switter-Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_port;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.RichTextBox richtextbox_log;
    }
}

