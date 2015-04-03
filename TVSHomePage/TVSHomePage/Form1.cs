using System;
using System.Windows.Forms;

namespace TVSHomePage
{
    public partial class HomePage : Form
    {
        
                
    
        public HomePage()
        {
            InitializeComponent();
            
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

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            //show login screen to continue
            LogIn login = new LogIn();
            login.Show();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
        
    }
}
