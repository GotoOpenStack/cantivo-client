using Adell.ItClient.Linux.Controls;
using Adell.ItClient.Linux.Views;

namespace Adell.ItClient.Linux
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkFullscreen = new System.Windows.Forms.CheckBox();
            this.systemView1 = new SystemView();
            this.info = new BindableToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(156, 78);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(180, 23);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(164, 20);
            this.username.TabIndex = 1;
            this.username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectNext);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(180, 59);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(164, 20);
            this.password.TabIndex = 2;
            this.password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectNext);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "password";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "login";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.info});
            this.statusStrip1.Location = new System.Drawing.Point(0, 208);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(383, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(180, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectNext);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::Adell.ItClient.Linux.Properties.Settings.Default.UseSsh;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Adell.ItClient.Linux.Properties.Settings.Default, "UseSsh", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(252, 121);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(42, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "ssh";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkFullscreen
            // 
            this.checkFullscreen.AutoSize = true;
            this.checkFullscreen.Checked = global::Adell.ItClient.Linux.Properties.Settings.Default.FullScreen;
            this.checkFullscreen.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Adell.ItClient.Linux.Properties.Settings.Default, "FullScreen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkFullscreen.Location = new System.Drawing.Point(12, 121);
            this.checkFullscreen.Name = "checkFullscreen";
            this.checkFullscreen.Size = new System.Drawing.Size(74, 17);
            this.checkFullscreen.TabIndex = 6;
            this.checkFullscreen.Text = "Fullscreen";
            this.checkFullscreen.UseVisualStyleBackColor = true;
            // 
            // systemView1
            // 
            this.systemView1.Location = new System.Drawing.Point(134, 157);
            this.systemView1.Name = "systemView1";
            this.systemView1.Size = new System.Drawing.Size(210, 48);
            this.systemView1.TabIndex = 8;
            // 
            // info
            // 
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(39, 17);
            this.info.Text = "Ready";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(383, 230);
            this.Controls.Add(this.systemView1);
            this.Controls.Add(this.checkFullscreen);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ITCAN Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkFullscreen;
        private Views.SystemView systemView1;
        private Controls.BindableToolStripStatusLabel info;
    }
}

