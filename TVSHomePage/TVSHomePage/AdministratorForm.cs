using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVSHomePage
{
    public partial class AdministratorForm : Form
    {
        public AdministratorForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close This window
            this.Close();
        }

        private void btnClockedIn_Click(object sender, EventArgs e)
        {
            //View who is clocked in
        }

        private void btnPayRoll_Click(object sender, EventArgs e)
        {
            //Create Payroll report for current week
        }
    }
}
