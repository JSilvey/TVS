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
            String clockID= null;
            String clockIn = "true";
            String payedOut = "no";
            
            
            
            
            if (txtEmployeeID.Text.Equals(""))
            {
                MessageBox.Show("Employee ID field can not be left blank!", "ERROR!");
            }
            else
            {
                string userPassword = txtEmployeeID.Text;
                
                
                try
                {
                    //open database connection
                    connection.Open();

                    //list of commands for database
                    string clockInCommand = "insert into TimeClock (UserPassword,ClockedIn,payedOut) values('" + userPassword + "','" + dt + "','"+payedOut+"') ";
                    string setClockedInCommand = "update EmployeeData set isClockedIn='"+clockIn+"' where Password='"+userPassword+"'";
                    string isClockedInQuery = "select isClockedIn from EmployeeData where Password='"+userPassword+"' ";
                    string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + userPassword + "'";
                    string clockIdQuery = "select Clock_ID from TimeClock where PayedOut='"+"no"+"'";
                    

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
                        OleDbDataReader tempClockIDReader = command.ExecuteReader();

                        //grab a clockID number to set as a test number
                        //in theory this should select the highest number but I have encountered 
                        //an intermitten bug that selects a lower number once in a while
                        int tempClockID = 0;
                        while (tempClockIDReader.Read())
                        {                           
                            //this set a temp clock ID in order to test it against a second 
                            //read to make sure the highest clock_ID number is recorded
                            tempClockID = Convert.ToInt32(tempClockIDReader["Clock_ID"].ToString());                            
                        }                                             
                        tempClockIDReader.Close();

                        //read clock Id numbers again and test against temp to verify clockid number is highest number
                        
                        OleDbDataReader clockIDReader = command.ExecuteReader();
                        while (clockIDReader.Read())
                        {
                            //read clock_id in again. If another number is higher than tempclockID set ClockID to the higher number
                            if (Convert.ToInt32(clockIDReader["Clock_ID"].ToString()) >= tempClockID)
                            {
                                clockID = clockIDReader["Clock_ID"].ToString();
                            }
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
                        txtEmployeeID.Clear();
                    }                   
                    connection.Close();
                }
                catch (Exception ex)
                {
                    txtEmployeeID.Clear();
                    connection.Close();
                    MessageBox.Show("There was an error connecting to the database.\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

            }
            //set focus back to text box
            txtEmployeeID.Focus();
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            //Create a new time object
            DateTime dt = DateTime.Now;
            String clockOut = "false";
            String clockID = "";
            String nullClockID = "";
            String payedOut = "no";

            if (txtEmployeeID.Text.Equals(""))
            {
                MessageBox.Show("Employee ID field can not be left blank!", "ERROR!");
            }
            else
            {
                string userPassword = txtEmployeeID.Text;
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
                        string clockOutCommand = "update TimeClock set ClockedOut='"+dt+"',PayedOut='"+payedOut+"' where Clock_ID="+longClockID+"";                        
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
                        txtEmployeeID.Clear();
                    }                    
                }
                catch (Exception ex)
                {
                    txtEmployeeID.Clear();
                    connection.Close();
                    MessageBox.Show("There was an error connecting to the database.\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            //set focus back to text box
            txtEmployeeID.Focus();
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
                DateTime clockIn = DateTime.Parse(clockInTime,System.Globalization.CultureInfo.CurrentCulture);
                DateTime clockOut = DateTime.Parse(clockOutTime, System.Globalization.CultureInfo.CurrentCulture);
                
                //calculate hours worked
                TimeSpan workHours = (clockOut.Subtract(clockIn));
                int hours = workHours.Hours;
                int minutes = ((workHours.Minutes) % 60);
                int seconds = ((workHours.Seconds) % 60);
                string timeWorked = hours.ToString() + ":" + minutes.ToString() + ":"+seconds.ToString();

                string setHoursWorked = "update TimeClock set HoursWorked='" + timeWorked + "' where Clock_ID=" + clockID + "";
                command.CommandText = setHoursWorked;
                command.ExecuteNonQuery();

                //calculate total hours worked
                string no = "no";
                string getHoursWorkedQuery = "select HoursWorked from TimeClock where UserPassword='"+userId+"' and PayedOut='"+no+"'";
                string results=null;                
                //retrieve hours from HoursWorked column that have not been paid out via a paycheck
                command.CommandText = getHoursWorkedQuery;
                OleDbDataReader getHoursReader = command.ExecuteReader();
                while (getHoursReader.Read())
                {                   
                    results += (getHoursReader["HoursWorked"].ToString()) + ':';
                }
                getHoursReader.Close();                
                string totalHours=(TotalHours(results));                

                //write totalhours to TimeClock table in database
                string writeTotalHours = "update TimeClock set TotalHoursWorked='"+totalHours+"' where Clock_ID="+clockID+"";
                command.CommandText = writeTotalHours;
                command.ExecuteNonQuery();                             
                
                //write totalhours to PayPeriod table in database
                string readPayPeriodTable = "select UserPassword from PayPeriod";
                string writePayPeriodTable = null;
                command.CommandText = readPayPeriodTable;
                OleDbDataReader payPeriodReader = command.ExecuteReader();
                while (payPeriodReader.Read())
                {
                    if (payPeriodReader["UserPassword"].ToString() != userId) 
                    {
                        //if the userID is not present in the table yet add the userID and total hours
                        writePayPeriodTable = "insert into PayPeriod (UserPassword,TotalHours) values('" + userId + "','" + totalHours + "')";
                    }
                    else
                    {
                        //if the userId is already present in the table update the total hours
                        writePayPeriodTable = "update PayPeriod set TotalHours='" + totalHours + "'where UserPassword='" + userId + "'";
                        break;
                    }
                }
                payPeriodReader.Close();                
                command.CommandText = writePayPeriodTable;
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("An error has occured!" + ex, "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                connection.Close();
            }


        }
        public string TotalHours(String results)
        {
            //split the results array into an array that holds each hour, minute, and second
            char[] delimiters = new char[] { ' ', ':' };
            string[] times = results.Split(delimiters);            
            

            //split the times array into individual arrays
            int[] hr = new int[(times.Length - 1) / 3];
            int[] min = new int[(times.Length - 1) / 3];
            int[] sec = new int[(times.Length) - 1 / 3];


            //populate hr array
            for (int i = 0, j = 0; i < times.Length - 1; i += 3, j++)
            {
                hr[j] = Convert.ToInt32(times[i]);

            }

            //populate min array
            for (int i = 1, j = 0; i < times.Length - 1; i += 3, j++)
            {
                min[j] = Convert.ToInt32(times[i]);

            }
            //populate sec array
            for (int i = 2, j = 0; i < times.Length; i += 3, j++)
            {
                sec[j] = Convert.ToInt32(times[i]);

            }


            //Sum the arrays individually
            int hrSum = 0;
            int minSum = 0;
            int secSum = 0;
            string totalHours = "";

            for (int i = 0; i < hr.Length; i++)
            {
                hrSum += hr[i];
            }
            for (int i = 0; i < hr.Length; i++)
            {
                minSum += min[i];
            }
            for (int i = 0; i < hr.Length; i++)
            {
                secSum += sec[i];
            }
            
            // if seconds is > 60 add minutes to minSum and set secSum to remainder
            if (secSum > 60)
            {
                minSum += (secSum / 60);
                secSum = (secSum % 60);
            }
            //if minutes is > 60 add hours to hrSum and set minSum to remainder
            if (minSum>60)
            {
                hrSum += (minSum / 60);
                minSum = (minSum % 60);
            }

            totalHours = (hrSum + ":" + minSum + ":" + secSum).ToString();
            return totalHours;
        }
    }
}
