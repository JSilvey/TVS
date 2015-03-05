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
    public partial class TimeClockForm : Form
    {
        public TimeClockForm()
        {
            InitializeComponent();
            //Start Timer When Form Loads
            tmrTimeClock.Start();
        }

        private void tmrTimeClock_Tick(object sender, EventArgs e)
        {
            //Get current Time
            DateTime dateTime = DateTime.Now;
            //Set Time Label To Show Current Time
            this.lblDay.Text = dateTime.ToString("dddd");
            this.lblDate.Text = dateTime.ToString("MMM dd, yyyy");
            this.lblTime.Text = dateTime.ToString("hh:mm:ss tt");
            lblDay.Visible = true;
            lblDate.Visible = true;
            lblTime.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Close();
        }

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            //Create a new time object
            DateTime clockedIn = DateTime.Now;
            if (mtbEmployeeID.Text.Equals(""))
            {
                MessageBox.Show("Employee ID field can not be left blank!", "ERROR!");
            }
            else
            {
                //Show clock in Confirmation Message Box
                MessageBox.Show(mtbEmployeeID.Text.ToString() + " clocked in at:\n" + clockedIn.ToString("h:mm tt   MMM dd, yyyy"), "Clock In Confirmation");
                //Clear masked text box for next employee
                mtbEmployeeID.Clear();

            }
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            //Create time object
            DateTime clockedOut = DateTime.Now;
            if (mtbEmployeeID.Text.Equals(""))
            {
                MessageBox.Show("Employee ID field cannot be left blank!","ERROR!");
            }
            else
            {
                //Show clock out confirmation message box
                MessageBox.Show(mtbEmployeeID.Text.ToString() + " clocked out at:\n" + clockedOut.ToString("h:mm tt   MMM dd, yyyy"), "Clock Out Confirmation");
                //clear masked text box for next employee
                mtbEmployeeID.Clear();  
            }
                     
         }
    }
}
