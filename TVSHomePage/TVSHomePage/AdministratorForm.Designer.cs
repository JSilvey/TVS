namespace TVSHomePage
{
    partial class AdministratorForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClockedIn = new System.Windows.Forms.Button();
            this.btnPayRoll = new System.Windows.Forms.Button();
            this.btnEditTimeCards = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(562, 575);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 29);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Exit";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClockedIn
            // 
            this.btnClockedIn.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClockedIn.Location = new System.Drawing.Point(79, 456);
            this.btnClockedIn.Name = "btnClockedIn";
            this.btnClockedIn.Size = new System.Drawing.Size(204, 29);
            this.btnClockedIn.TabIndex = 12;
            this.btnClockedIn.Text = "Currently Clocked In";
            this.btnClockedIn.UseVisualStyleBackColor = true;
            this.btnClockedIn.Click += new System.EventHandler(this.btnClockedIn_Click);
            // 
            // btnPayRoll
            // 
            this.btnPayRoll.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayRoll.Location = new System.Drawing.Point(79, 409);
            this.btnPayRoll.Name = "btnPayRoll";
            this.btnPayRoll.Size = new System.Drawing.Size(204, 29);
            this.btnPayRoll.TabIndex = 13;
            this.btnPayRoll.Text = "Create Weekly Pay Roll";
            this.btnPayRoll.UseVisualStyleBackColor = true;
            this.btnPayRoll.Click += new System.EventHandler(this.btnPayRoll_Click);
            // 
            // btnEditTimeCards
            // 
            this.btnEditTimeCards.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditTimeCards.Location = new System.Drawing.Point(79, 502);
            this.btnEditTimeCards.Name = "btnEditTimeCards";
            this.btnEditTimeCards.Size = new System.Drawing.Size(204, 29);
            this.btnEditTimeCards.TabIndex = 14;
            this.btnEditTimeCards.Text = "Edit Time Cards";
            this.btnEditTimeCards.UseVisualStyleBackColor = true;
            // 
            // AdministratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1243, 631);
            this.Controls.Add(this.btnEditTimeCards);
            this.Controls.Add(this.btnPayRoll);
            this.Controls.Add(this.btnClockedIn);
            this.Controls.Add(this.btnClose);
            this.Name = "AdministratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdministratorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClockedIn;
        private System.Windows.Forms.Button btnPayRoll;
        private System.Windows.Forms.Button btnEditTimeCards;
    }
}