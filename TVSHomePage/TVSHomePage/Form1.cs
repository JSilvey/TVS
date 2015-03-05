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
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create And Show EmployeeForm When Clicked
            EmployeeForm empForm = new EmployeeForm();
            empForm.Show();
        }

        private void btnAdministrator_Click(object sender, EventArgs e)
        {
            //Prompt Admin Log in messagebox
            
            //Create and Show AdminForm when Clicked
            AdministratorForm adminForm = new AdministratorForm();
            adminForm.Show();
        }

        private void btnTimeClock_Click(object sender, EventArgs e)
        {
            //Create and show time clock form when clicked
            TimeClockForm timeClockForm = new TimeClockForm();
            timeClockForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close the form
            this.Close();
        }

        
    }
}
