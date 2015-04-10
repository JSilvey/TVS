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
    public partial class EditEmployeeForm : Form
    {
        //create a gloabal connection to the database
        private OleDbConnection connection = new OleDbConnection();
        private int empID;

        public EditEmployeeForm(string employeeID)
        {            
            InitializeComponent();            

            //connection string for database connection
            try
            {
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Admin\Desktop\CAPSTONE\TVSDB.accdb;
                Persist Security Info=False;";
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error in the database connection string (line25.)\n" + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            empID = Convert.ToInt32(employeeID);
            loadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Close();
        }

        private void btnEditEmp_Click(object sender, EventArgs e)
        {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string editEmployeeNonQuery = "update EmployeeData set [FirstName]='" + txtFirstName.Text + "', [LastName]='" + txtLastName.Text + "', [StreetAddress]='" + txtStreetAddress.Text + "' ,[City]='" + txtCity.Text + "' ,[State]='" + txtState.Text + "' ,[Zip]='" + txtZip.Text + "' ,[Phone]='" + txtPhone.Text + "' ,[SSN]='" + txtSSN.Text + "' ,[DOB]='" + txtDOB.Text + "',[Password]='"+txtPassword.Text+"' ,[Role]='" + txtRole.Text + "' ,[Title]='" + txtTitle.Text + "' ,[RatePerHour]='" + txtRate.Text + "'where [EMP_ID]=" + empID + "";
                command.CommandText = editEmployeeNonQuery;
                command.ExecuteNonQuery();
                MessageBox.Show(txtFirstName.Text + " " + txtLastName.Text + "'s record was updated! ", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("There was an error!\n" + ex.Message, "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        public void loadData()
        {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string loadDataQuery = "select * from EmployeeData where EMP_ID="+empID+"";
                command.CommandText = loadDataQuery;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtStreetAddress.Text = reader["StreetAddress"].ToString();
                    txtCity.Text = reader["City"].ToString();
                    txtState.Text = reader["State"].ToString();
                    txtZip.Text = reader["Zip"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtSSN.Text = reader["SSN"].ToString();
                    txtDOB.Text = reader["DOB"].ToString();
                    txtPassword.Text = reader["Password"].ToString();
                    txtRole.Text = reader["Role"].ToString();
                    txtTitle.Text = reader["Title"].ToString();
                    txtRate.Text = reader["RatePerHour"].ToString();
                }
                connection.Close();
                this.Text = "Edit "+txtFirstName.Text+" "+txtLastName.Text+"'s data.";
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("There was an error!\n" + ex.Message, "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            //Create a new time object
            DateTime dt = DateTime.Now;
            String clockID= null;
            String clockIn = "true";           
            
            string userPassword = txtPassword.Text;
                
                
                try
                {
                    //open database connection
                    connection.Open();

                    //list of commands for database
                    string clockInCommand = "insert into TimeClock (UserPassword,ClockedIn) values('" + userPassword + "','" + dt + "') ";
                    string setClockedInCommand = "update EmployeeData set isClockedIn='"+clockIn+"' where Password='"+userPassword+"'";
                    string isClockedInQuery = "select isClockedIn from EmployeeData where Password='"+userPassword+"' ";
                    string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + userPassword + "'";
                    string clockIdQuery = "select Clock_ID from TimeClock";
                    

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
                        
                        //read in new clock ID created by access database
                        command.CommandText = clockIdQuery;                        
                        OleDbDataReader clockIDReader = command.ExecuteReader();
                        
                        while (clockIDReader.Read())
                        {
                            //this will get the last clock ID generated by the database
                             clockID = clockIDReader["Clock_ID"].ToString();                            
                        }                                             
                        clockIDReader.Close();

                        //set the clock id in employeeData Table
                        string setClockID = "update EmployeeData set CurrentClockID='" + clockID + "' where Password='" + userPassword + "'";
                        
                        //set the clock_id in EmployeeData Table for future reference
                        command.CommandText = setClockID;
                        command.ExecuteNonQuery();
                       
                        //set user's clocked in status to true
                        command.CommandText = setClockedInCommand;                         
                        command.ExecuteNonQuery();                        

                        //Show clock in Confirmation Message Box
                        MessageBox.Show(userName + " clocked in at:\n" + dt, "Clock In Confirmation");

                        
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

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            //Create a new time object
            DateTime dt = DateTime.Now;
            String clockOut = "false";
            String clockID = "";
            String nullClockID = "";

            try
            {
                //open database connection
                connection.Open();

                //list of commands for database

                string setClockedOutCommand = "update EmployeeData set isClockedIn='" + clockOut + "' where Password='" + txtPassword.Text + "'";
                string isClockedOutQuery = "select isClockedIn from EmployeeData where Password='" + txtPassword.Text + "' ";
                string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + txtPassword.Text + "'";
                string getclockIdQuery = "select CurrentClockID from EmployeeData where Password='" + txtPassword.Text + "'";
                string resetClockID = "update EmployeeData set CurrentClockID='" + nullClockID + "' where Password='" + txtPassword.Text + "'";

                //create new command for database connection
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                //create a reader to see if user is already clocked out
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

                //test if user is already clocked out
                if (userClockedOut == "false")
                {
                    MessageBox.Show(userName + " is already clocked out!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //get current clock ID (current line in database)
                    command.CommandText = getclockIdQuery;
                    OleDbDataReader clockIDReader = command.ExecuteReader();
                    clockIDReader.Read();
                    clockID = clockIDReader["CurrentClockID"].ToString();
                    clockIDReader.Close();

                    //set clock ID to long
                    long longClockID = Convert.ToInt64(clockID);

                    //clockout command 
                    string clockOutCommand = "update TimeClock set ClockedOut='" + dt + "' where Clock_ID=" + longClockID + "";

                    //timestamp user's clockout time and date in database
                    command.CommandText = clockOutCommand;
                    command.ExecuteNonQuery();

                    //reset clockId in employee table for next clockin
                    command.CommandText = resetClockID;
                    command.ExecuteNonQuery();

                    //set user's clocked in status to false
                    command.CommandText = setClockedOutCommand;
                    command.ExecuteNonQuery();

                    connection.Close();

                    //Calculate hours worked
                    TimeClockForm tcf = new TimeClockForm();
                    tcf.CalcHoursWorked(txtPassword.Text, longClockID);

                    //Show clock in Confirmation Message Box
                    MessageBox.Show(userName + " clocked out at:\n" + dt, "Clock In Confirmation");

                }

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
