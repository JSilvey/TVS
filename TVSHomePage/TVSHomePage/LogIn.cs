using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVSHomePage
{
    public partial class LogIn : Form
    {
        //create a gloabal connection to the database
        private OleDbConnection connection = new OleDbConnection();

        public LogIn()
        {
            InitializeComponent();
            //connection string for database connection
            try
            {
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Admin\Desktop\CAPSTONE\TVSDB.accdb;
                Persist Security Info=False;";
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is an error in the database connection string (line25.)\n" + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close this window
            this.Close();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                //open database connection
                connection.Open();
                //create new command for database connection
                OleDbCommand Command = new OleDbCommand();
                Command.Connection = connection;
                String loginQuery = "select * from EmployeeData where LastName='" + txtUserName.Text + "' and Password='" + txtPassword.Text + "'";
                String RoleQuery = "select * from EmployeeData where LastName='" + txtUserName.Text + "'and Password='" + txtPassword.Text + "'";

                Command.CommandText = loginQuery;
                
                //create a reader object to read through database
                OleDbDataReader reader = Command.ExecuteReader();
                //count variable counts the number of matching records
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count==1)//username and password are correct
                {
                    //close reader
                    reader.Close();
                    
                    //create a new reader to query user's ROLE (admin or user)
                    Command.CommandText = RoleQuery;
                    OleDbDataReader roleReader = Command.ExecuteReader();
                    while (roleReader.Read())
                    {
                        //get user role to determine which screen they can see
                        string role = roleReader["Role"].ToString().ToLower();

                        if (role != "admin") //not an admin, show advanced employee form
                        {
                            
                            frmEmployee empForm = new frmEmployee(txtPassword.Text);
                            empForm.Show();

                            //close login window
                            this.Close();
                        }
                        else //must be admin, show admin form
                        {
                            AdministratorForm adminForm = new AdministratorForm(txtPassword.Text);
                            adminForm.Show();

                            //close login window
                            this.Close();
                        }                        
                    }
                    //close roleReader
                    roleReader.Close();
                }
                else if (count>1)//there are duplicate entries in the database
                {                    
                    MessageBox.Show("Warning! There are duplicate usernames and passwords in database!");
                }
                else//username and/or password is incorrect
                {
                    MessageBox.Show("Username and/or password is incorrect","",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //close database connection
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("There was an error connecting to the database.\n"+ex.Message, "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //clear textboxes
            txtUserName.Clear();
            txtPassword.Clear();
            //set focus back to username textbox
            txtUserName.Focus();
        }
    }
}
