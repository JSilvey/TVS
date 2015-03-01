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
            this.btnEmployee = new System.Windows.Forms.Button();
            this.btnAdministrator = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTimeClock = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEmployee
            // 
            this.btnEmployee.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployee.Location = new System.Drawing.Point(36, 149);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(213, 29);
            this.btnEmployee.TabIndex = 0;
            this.btnEmployee.Text = "Employee Advanced";
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAdministrator
            // 
            this.btnAdministrator.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdministrator.Location = new System.Drawing.Point(36, 200);
            this.btnAdministrator.Name = "btnAdministrator";
            this.btnAdministrator.Size = new System.Drawing.Size(213, 32);
            this.btnAdministrator.TabIndex = 1;
            this.btnAdministrator.Text = "Administrator Advanced";
            this.btnAdministrator.UseVisualStyleBackColor = true;
            this.btnAdministrator.Click += new System.EventHandler(this.btnAdministrator_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome!\r\nPlease Select an Option";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnTimeClock
            // 
            this.btnTimeClock.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimeClock.Location = new System.Drawing.Point(36, 98);
            this.btnTimeClock.Name = "btnTimeClock";
            this.btnTimeClock.Size = new System.Drawing.Size(213, 29);
            this.btnTimeClock.TabIndex = 3;
            this.btnTimeClock.Text = "Time Clock";
            this.btnTimeClock.UseVisualStyleBackColor = true;
            this.btnTimeClock.Click += new System.EventHandler(this.btnTimeClock_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(36, 254);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(213, 32);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumBlue;
            this.ClientSize = new System.Drawing.Size(284, 319);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnTimeClock);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdministrator);
            this.Controls.Add(this.btnEmployee);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "HomePage";
            this.Text = "Time Verification System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.Button btnAdministrator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTimeClock;
        private System.Windows.Forms.Button btnExit;
    }
}

