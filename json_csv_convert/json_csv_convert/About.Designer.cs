namespace json_csv_convert
{
    partial class About
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
            this.AppAbout = new System.Windows.Forms.Label();
            this.AppName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AppAbout
            // 
            this.AppAbout.AutoSize = true;
            this.AppAbout.ForeColor = System.Drawing.Color.White;
            this.AppAbout.Location = new System.Drawing.Point(13, 82);
            this.AppAbout.Name = "AppAbout";
            this.AppAbout.Size = new System.Drawing.Size(60, 13);
            this.AppAbout.TabIndex = 0;
            this.AppAbout.Text = "App_About";
            this.AppAbout.Click += new System.EventHandler(this.About_Click);
            // 
            // AppName
            // 
            this.AppName.AutoSize = true;
            this.AppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppName.ForeColor = System.Drawing.Color.White;
            this.AppName.Location = new System.Drawing.Point(12, 9);
            this.AppName.Name = "AppName";
            this.AppName.Size = new System.Drawing.Size(106, 24);
            this.AppName.TabIndex = 1;
            this.AppName.Text = "App_Name";
            this.AppName.Click += new System.EventHandler(this.About_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(387, 156);
            this.Controls.Add(this.AppName);
            this.Controls.Add(this.AppAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Click += new System.EventHandler(this.About_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AppAbout;
        private System.Windows.Forms.Label AppName;
    }
}