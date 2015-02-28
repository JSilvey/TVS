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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Start Timer When Form Loads
            timer1.Start();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //Create a timer to display current time on main page
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Get current Time
            DateTime dateTime = DateTime.Now;
            //Set Time Label To Show Current Time
            this.lblDateTime.Text = dateTime.ToString();
            lblDateTime.Visible = true;

        }
    }
}
