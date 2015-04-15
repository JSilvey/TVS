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
    public partial class frmEmployee : Form
    {
        //create a global connection to the database
        private OleDbConnection connection = new OleDbConnection();

        //create a global variable for userID
        private string id;
                
        public frmEmployee(String userID)
        {
            InitializeComponent();
            //start timer on form load
            tmrEmployee.Start();

            //Pass userID to global variable
            this.id = userID;
                        
            //connection string for database
            try
            {
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Admin\Desktop\CAPSTONE\TVSDB.accdb;
                Persist Security Info=False;";
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error in the database connection string (line25.)" + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            LoadData();
            
        }

        

        private void LoadData()
        {
            try
            {
                //establish a connection to db to perform query
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string getUserNameQuery = "select FirstName,LastName from EmployeeData where Password='"+id+"' ";
                string loadEmpInfoTableQuery = "select FirstName,LastName,StreetAddress,City,State,Zip,Phone from EmployeeData where Password='" + id + "'";
                string loadTimeClockTableQuery = "select ClockedIn,ClockedOut,HoursWorked,TotalHoursWorked,payedOut from TimeClock where UserPassword='"+id+"'";
                string loadPayHistory = "select Check_ID,HoursPaid,GrossPay,PayDate from PayChecks where userPassword='"+id+"'";
                
                //Get user first and last name and greet in title bar
                command.CommandText = getUserNameQuery;
                OleDbDataReader nameReader = command.ExecuteReader();
                nameReader.Read();
                String firstName = nameReader["FirstName"].ToString();
                String lastName = nameReader["LastName"].ToString();
                this.Text = "Welcome " + firstName + " " + lastName;                
                nameReader.Close();

                //Fill Employee Data table
                command.CommandText = loadEmpInfoTableQuery;
                OleDbDataAdapter daEmpInfo = new OleDbDataAdapter(command);
                DataTable dtEmpInfo = new DataTable();
                daEmpInfo.Fill(dtEmpInfo);
                dgvUserInfo.DataSource = dtEmpInfo;
                dgvUserInfo.AutoResizeColumns();
                dgvUserInfo.ClearSelection();
                dgvUserInfo.CurrentCell = null;

                //Fill Textboxes
                command.CommandText = loadEmpInfoTableQuery;
                OleDbDataReader drEmpInfo = command.ExecuteReader();
                drEmpInfo.Read();
                txtStreet.Text=drEmpInfo["StreetAddress"].ToString();;
                txtCity.Text=drEmpInfo["City"].ToString();;
                txtState.Text=drEmpInfo["State"].ToString();;
                txtZipcode.Text = drEmpInfo["Zip"].ToString(); ;
                txtPhone.Text=drEmpInfo["Phone"].ToString();
                drEmpInfo.Close();


                //Fill TimeCard Table
                command.CommandText = loadTimeClockTableQuery;
                OleDbDataAdapter daTimeClock = new OleDbDataAdapter(command);
                DataTable dtTimeClock = new DataTable();
                daTimeClock.Fill(dtTimeClock);
                dgvTimeClock.DataSource = dtTimeClock;
                dgvTimeClock.AutoResizeColumns();
                dgvTimeClock.ClearSelection();
                dgvTimeClock.CurrentCell = null;

                //Fill PayHistory Table
                command.CommandText = loadPayHistory;
                OleDbDataAdapter daPayHistory = new OleDbDataAdapter(command);
                DataTable dtPayHistory = new DataTable();
                daPayHistory.Fill(dtPayHistory);
                dgvPayHistory.DataSource = dtPayHistory;
                dgvPayHistory.AutoResizeColumns();
                dgvPayHistory.ClearSelection();
                dgvPayHistory.CurrentCell = null;

                
                
                //close db connection
                connection.Close();

                
                
            }                
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("There was an error in loaddata method\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //close form
            this.Close();
        }

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            //Create a new time object
            DateTime dt = DateTime.Now;
            String clockID = null;
            String clockIn = "true";
            string payedOut = "no";

            try
            {
                //open database connection
                connection.Open();

                //list of commands for database
                string clockInCommand = "insert into TimeClock (UserPassword,ClockedIn,payedOut) values('" + id + "','" + dt + "','"+payedOut+"') ";
                string setClockedInCommand = "update EmployeeData set isClockedIn='" + clockIn + "' where Password='" + id + "'";
                string isClockedInQuery = "select isClockedIn from EmployeeData where Password='" + id + "' ";
                string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + id + "'";
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
                    MessageBox.Show(userName + " is already clocked in!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string setClockID = "update EmployeeData set CurrentClockID='" + clockID + "' where Password='" + id + "'";

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

                //reload tabledata
                LoadData();
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

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Test to see if all fields contain a value
            if ((txtStreet.Text == "") || (txtCity.Text == "") || (txtState.Text == "") || (txtZipcode.Text == "") || (txtPhone.Text == ""))
            {
                //One or more fields left blank
                MessageBox.Show("All fields must be filled in to update info!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //open a new connection and update user info
                try
                {
                    connection.Open();
                    OleDbCommand updateCommand = new OleDbCommand();
                    updateCommand.Connection = connection;
                    string updateInfo = @"update EmployeeData
set StreetAddress='" + txtStreet.Text + "', City='" + txtCity.Text + "', State='" + txtState.Text + "', Zip='" + txtZipcode.Text + "', Phone='" + txtPhone.Text + "' where Password='" + id + "'";
                    updateCommand.CommandText = updateInfo;
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show("An error has occured!\n\n"+ex.Message,"ERROR!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
                //reload the data in the tables
                LoadData();
            }
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            
            //Create a new time object
            DateTime dt = DateTime.Now;
            String clockOut = "false";
            String clockID = "";
            String nullClockID = "";
            string payedOut = "no";

            try
            {
                    //open database connection
                    connection.Open();

                    //list of commands for database

                    string setClockedOutCommand = "update EmployeeData set isClockedIn='" + clockOut + "' where Password='" + id + "'";
                    string isClockedOutQuery = "select isClockedIn from EmployeeData where Password='" + id + "' ";
                    string nameQuery = "select FirstName, LastName from EmployeeData where Password='" + id + "'";
                    string getclockIdQuery = "select CurrentClockID from EmployeeData where Password='" + id + "'";
                    string resetClockID = "update EmployeeData set CurrentClockID='" + nullClockID + "' where Password='" + id + "'";

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
                        string clockOutCommand = "update TimeClock set ClockedOut='" + dt + "',payedOut='"+payedOut+"' where Clock_ID=" + longClockID + "";

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
                        tcf.CalcHoursWorked(id, longClockID);

                        //Show clock in Confirmation Message Box
                        MessageBox.Show(userName + " clocked out at:\n" + dt, "Clock Out Confirmation");

                        connection.Close();

                    }
                    connection.Close();                                   

                    //reload table data
                    LoadData();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show("There was an error in the clockout method.\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tmrEmployee_Tick(object sender, EventArgs e)
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
        
    }
}
