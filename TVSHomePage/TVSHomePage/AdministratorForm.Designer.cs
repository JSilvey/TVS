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
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
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
            // dgvEmployees
            // 
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Location = new System.Drawing.Point(14, 16);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.Size = new System.Drawing.Size(1215, 197);
            this.dgvEmployees.TabIndex = 15;
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEmployee.Location = new System.Drawing.Point(527, 292);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(188, 47);
            this.btnAddEmployee.TabIndex = 16;
            this.btnAddEmployee.Text = "Add Employee";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // btnReload
            // 
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Location = new System.Drawing.Point(748, 292);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(188, 47);
            this.btnReload.TabIndex = 17;
            this.btnReload.Text = "Reload Data";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // AdministratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1243, 631);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnAddEmployee);
            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.btnEditTimeCards);
            this.Controls.Add(this.btnPayRoll);
            this.Controls.Add(this.btnClockedIn);
            this.Controls.Add(this.btnClose);
            this.Name = "AdministratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrator ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClockedIn;
        private System.Windows.Forms.Button btnPayRoll;
        private System.Windows.Forms.Button btnEditTimeCards;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.Button btnReload;
    }
}