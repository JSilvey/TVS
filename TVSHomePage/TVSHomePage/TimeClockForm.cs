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
        //create a global connection to the database
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
            String clockID= null;
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
            String clockID = "";
            String nullClockID = "";

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
                    
                    string setClockedOutCommand = "update EmployeeData set isClockedIn='" + clockOut + "' where Password='" + userPassword + "'";
                    string isClockedOutQuery = "select isClockedIn from EmployeeData where Password='" + userPassword + "' ";
                    string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + userPassword + "'";
                    string getclockIdQuery = "select CurrentClockID from EmployeeData where Password='" + userPassword + "'";
                    string resetClockID = "update EmployeeData set CurrentClockID='" + nullClockID + "' where Password='" + userPassword + "'";

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
                        //get current clock ID (current line in database)
                        command.CommandText = getclockIdQuery;
                        OleDbDataReader clockIDReader = command.ExecuteReader();
                        clockIDReader.Read();
                        clockID = clockIDReader["CurrentClockID"].ToString();
                        clockIDReader.Close();
                        
                        //set clock ID to long
                        long longClockID = Convert.ToInt64(clockID);
                        
                        //clockout command 
                        string clockOutCommand = "update TimeClock set ClockedOut='"+time+"' where Clock_ID="+longClockID+"";
                        
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
                        CalcHoursWorked(userPassword,longClockID);

                        //Show clock in Confirmation Message Box
                        MessageBox.Show(userName + " clocked out at:\n" + dt, "Clock In Confirmation");

                        //Clear masked text box for next employee
                        mtbEmployeeID.Clear();
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

        public void CalcHoursWorked(string userId, long clockID )
        {
            try
            {
                connection.Open();
                //create a query to get clocked in and clocked out times
                string getHoursQuery = "select ClockedIn, ClockedOut from TimeClock where Clock_ID=" + clockID + " ";

                //create a new command
                OleDbCommand command = new OleDbCommand();

                //set command's connection to the global connection
                command.Connection = connection;

                //set command text to the query
                command.CommandText = getHoursQuery;

                //create a new reader object to read values from database
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();

                //Set the values to local variables
                string clockInTime = reader["ClockedIn"].ToString();
                string clockOutTime = reader["ClockedOut"].ToString();

                //close the reader
                reader.Close();

                //parse string into date/time objects
                DateTime clockIn = DateTime.ParseExact(clockInTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                DateTime clockOut = DateTime.ParseExact(clockOutTime, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);

                //calculate hours worked
                TimeSpan workHours = clockOut.Subtract(clockIn);

                string setHoursWorked = "update TimeClock set HoursWorked='" + workHours + "' where Clock_ID=" + clockID + "";
                command.CommandText = setHoursWorked;

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("An error has occured!" + ex.Message, "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                connection.Close();
            }


        }
    }
}
