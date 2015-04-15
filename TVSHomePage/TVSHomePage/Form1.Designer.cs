namespace TVSHomePage
{
    partial class HomePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnTimeClock = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(320, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 48);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome!\r\nPlease Select an Option";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TVSHomePage.Properties.Resources.Apps_preferences_system_login_icon;
            this.pictureBox1.Location = new System.Drawing.Point(29, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(273, 249);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvanced.Image = global::TVSHomePage.Properties.Resources.Admin_icon;
            this.btnAdvanced.Location = new System.Drawing.Point(350, 137);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(181, 47);
            this.btnAdvanced.TabIndex = 1;
            this.btnAdvanced.Text = "Advanced";
            this.btnAdvanced.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdvanced.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::TVSHomePage.Properties.Resources.Close_2_icon;
            this.btnExit.Location = new System.Drawing.Point(350, 196);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(181, 47);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnTimeClock
            // 
            this.btnTimeClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimeClock.Image = global::TVSHomePage.Properties.Resources.Clock_icon_small;
            this.btnTimeClock.Location = new System.Drawing.Point(350, 78);
            this.btnTimeClock.Name = "btnTimeClock";
            this.btnTimeClock.Size = new System.Drawing.Size(181, 47);
            this.btnTimeClock.TabIndex = 0;
            this.btnTimeClock.Text = "Time Clock";
            this.btnTimeClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimeClock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTimeClock.UseVisualStyleBackColor = true;
            this.btnTimeClock.Click += new System.EventHandler(this.btnTimeClock_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(568, 265);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnTimeClock);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Time Verification System";
            this.Load += new System.EventHandler(this.HomePage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTimeClock;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

