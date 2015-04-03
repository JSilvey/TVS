using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TVSHomePage
{
    public partial class TimeClockForm : Form
    {
        //create a gloabal connection to the database
        private OleDbConnection connection = new OleDbConnection();

        public TimeClockForm()
        {
            InitializeComponent();
            //Start Timer When Form Loads
            tmrTimeClock.Start();
            //connection string for database connection
            try
            {
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Admin\Desktop\CAPSTONE\TVSDB.accdb;
                Persist Security Info=False;";
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is an error in the database connection string (line25.)" + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        
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
            DateTime dt = DateTime.Now;
            String date = dt.ToString("d");
            String time = dt.ToString("HH:mm");
            
            String clockIn = "true";
            
            if (mtbEmployeeID.Text.Equals(""))
            {
                MessageBox.Show("Employee ID field can not be left blank!", "ERROR!");
            }
            else
            {
                string userPassword = mtbEmployeeID.Text;
                
                
                try
                {
                    //open database connection
                    connection.Open();

                    //list of commands for database
                    string clockInCommand = "insert into TimeClock (UserPassword,WorkDate,ClockedIn) values('" + userPassword + "','" + date + "','" + time + "') ";
                    string setClockedInCommand = "update EmployeeData set isClockedIn='"+clockIn+"' where Password='"+userPassword+"'";
                    string isClockedInQuery = "select isClockedIn from EmployeeData where Password='"+userPassword+"' ";
                    string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + userPassword + "'";

                    //create new command for database connection
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    //create a reader to see if user is already clocked in
                    command.CommandText = isClockedInQuery;
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();                    
                    string userClockedIn = reader["isClockedIn"].ToString().ToLower();
                    reader.Close();
                    
                    //create a reader to get user's full name
                    command.CommandText = nameQuery;
                    OleDbDataReader nameReader = command.ExecuteReader();
                    nameReader.Read();
                    string userName = (nameReader["FirstName"].ToString() + " " + nameReader["LastName"].ToString());
                    nameReader.Close();
                    
                    //test if user is already clocked in
                    if (userClockedIn == "true")
                    {
                        MessageBox.Show(userName +" is already clocked in!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //timestamp user's clockin time and date in database
                        command.CommandText = clockInCommand;
                        command.ExecuteNonQuery();
                       
                        //set user's clocked in status to true
                        command.CommandText = setClockedInCommand;
                        command.ExecuteNonQuery();                        

                        //Show clock in Confirmation Message Box
                        MessageBox.Show(userName + " clocked in at:\n" + dt, "Clock In Confirmation");

                        //Clear masked text box for next employee
                        mtbEmployeeID.Clear();
                    }                   
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show("There was an error connecting to the database.\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            //Create a new time object
            DateTime dt = DateTime.Now;
            String date = dt.ToString("d");
            String time = dt.ToString("HH:mm");

            String clockOut = "false";

            if (mtbEmployeeID.Text.Equals(""))
            {
                MessageBox.Show("Employee ID field can not be left blank!", "ERROR!");
            }
            else
            {
                string userPassword = mtbEmployeeID.Text;


                try
                {
                    //open database connection
                    connection.Open();

                    //list of commands for database
                    string clockOutCommand = "insert into TimeClock (UserPassword,WorkDate,ClockedOut) values('" + userPassword + "','" + date + "','" + time + "') ";
                    string setClockedOutCommand = "update EmployeeData set isClockedIn='" + clockOut + "' where Password='" + userPassword + "'";
                    string isClockedOutQuery = "select isClockedIn from EmployeeData where Password='" + userPassword + "' ";
                    string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + userPassword + "'";

                    //create new command for database connection
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    //create a reader to see if user is already clocked in
                    command.CommandText = isClockedOutQuery;
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    string userClockedOut = reader["isClockedIn"].ToString().ToLower();
                    reader.Close();

                    //create a reader to get user's full name
                    command.CommandText = nameQuery;
                    OleDbDataReader nameReader = command.ExecuteReader();
                    nameReader.Read();
                    string userName = (nameReader["FirstName"].ToString() + " " + nameReader["LastName"].ToString());
                    nameReader.Close();

                    //test if user is already clocked in
                    if (userClockedOut == "false")
                    {
                        MessageBox.Show(userName + " is already clocked out!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //timestamp user's clockout time and date in database
                        command.CommandText = clockOutCommand;
                        command.ExecuteNonQuery();

                        //set user's clocked in status to false
                        command.CommandText = setClockedOutCommand;
                        command.ExecuteNonQuery();

                        //Show clock in Confirmation Message Box
                        MessageBox.Show(userName + " clocked out at:\n" + dt, "Clock In Confirmation");

                        //Clear masked text box for next employee
                        mtbEmployeeID.Clear();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show("There was an error connecting to the database.\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

            }       
         }
    }
}
