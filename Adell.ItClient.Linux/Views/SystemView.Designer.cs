namespace Adell.ItClient.Linux.Views
{
    partial class SystemView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonShutDown = new System.Windows.Forms.Button();
            this.buttonReboot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonShutDown
            // 
            this.buttonShutDown.Location = new System.Drawing.Point(12, 12);
            this.buttonShutDown.Name = "buttonShutDown";
            this.buttonShutDown.Size = new System.Drawing.Size(75, 23);
            this.buttonShutDown.TabIndex = 0;
            this.buttonShutDown.Text = "Shut Down";
            this.buttonShutDown.UseVisualStyleBackColor = true;
            // 
            // buttonReboot
            // 
            this.buttonReboot.Location = new System.Drawing.Point(114, 13);
            this.buttonReboot.Name = "buttonReboot";
            this.buttonReboot.Size = new System.Drawing.Size(75, 23);
            this.buttonReboot.TabIndex = 1;
            this.buttonReboot.Text = "Reboot";
            this.buttonReboot.UseVisualStyleBackColor = true;
            // 
            // SystemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonReboot);
            this.Controls.Add(this.buttonShutDown);
            this.Name = "SystemView";
            this.Size = new System.Drawing.Size(210, 48);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShutDown;
        private System.Windows.Forms.Button buttonReboot;
    }
}
