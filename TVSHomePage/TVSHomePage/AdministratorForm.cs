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
    public partial class AdministratorForm : Form
    {
        //create a gloabal connection to the database
        private OleDbConnection connection = new OleDbConnection();

        // create a global variable for admin ID
        private string adminID;

        public AdministratorForm(string userID)
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
                MessageBox.Show("There is an error in the database connection string (line35.)\n" + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            
            //pass userID to global variable
            adminID = userID;

            //load form data
            LoadData();
            
        }
        
        
        public void LoadData()
        {
            try
            {
                //establish a connection to db to perform query
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string getUserNameQuery = "select FirstName,LastName from EmployeeData where Password='" + adminID + "' ";
                string loadEmpInfoTableQuery = "select * from EmployeeData";
                
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
                dgvEmployees.DataSource = dtEmpInfo;
                dgvEmployees.AutoResizeColumns();
                dgvEmployees.ClearSelection();
                dgvEmployees.CurrentCell = null;

                //Clear and Fill listbox
                cbEmpID.Items.Clear();
                string loadEmpComboBox = "select EMP_ID,FirstName,LastName from EmployeeData";
                command.CommandText = loadEmpComboBox;
                OleDbDataReader idReader = command.ExecuteReader();
                while (idReader.Read())
                {
                    //Add employees to combobox
                    cbEmpID.Items.Add(idReader[0].ToString() + " " + idReader[1].ToString() + " " + idReader[2].ToString());
                }
                idReader.Close();

                //Load list for employees already clocked in
                lstClockedIn.Items.Clear();
                string loadClockedInList = "select FirstName,LastName from EmployeeData where isClockedIn='" + true + "'";
                command.CommandText = loadClockedInList;
                OleDbDataReader clockedInReader = command.ExecuteReader();
                while(clockedInReader.Read())
                {
                    lstClockedIn.Items.Add(clockedInReader["FirstName"].ToString() + " " + clockedInReader["LastName"].ToString());
                }
                clockedInReader.Close();

                //clear combobox and datagridview timetable
                cbEmpID.SelectedItem = null;
                cbEmpID.Text = "";
                dgvTimeTable.DataSource = null;
                dgvTimeTable.Rows.Clear();
                dgvPayHistory.DataSource = null;
                dgvPayHistory.Rows.Clear();

                
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("There was an error" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            connection.Close();
            //open the add employee screen
            AddEmployeeForm addEmp = new AddEmployeeForm();
            addEmp.Show();
        }     

        private void btnReload_Click(object sender, EventArgs e)
        {
            connection.Close();
            //update/reload the forms
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Close();
        }        

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            //check to make sure the user has chosen an employee ID 
            if (cbEmpID.Text == "")
            {
                //no employee ID selected
                MessageBox.Show("You must select an employee ID to edit", "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Split combobox string in order to select just the EMP_ID
                //create a delimiter definition (a space is my delimiter)
                char delimiter = ' ';
                string text = cbEmpID.Text;
                string[] words = text.Split(delimiter);
                string emp_ID = words[0];              
                
                
                //employee Id was selected, open form passing the ID number
                EditEmployeeForm editEmp = new EditEmployeeForm(emp_ID);
                editEmp.Show();                                
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            connection.Close();
            //check to make sure the user has chosen an employee ID
            if (cbEmpID.Text == "")
            {
                //no employee ID selected
                MessageBox.Show("You must select an employee ID to delete!", "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Split combobox string in order to select just the EMP_ID
                //create a delimiter definition (a space is my delimiter)
                char delimiter = ' ';
                string text = cbEmpID.Text;
                string[] words = text.Split(delimiter);
                string emp_ID = words[0];
                string empName = (words[1] + " " + words[2]);
                string userPassword = "";

                DialogResult dialog = MessageBox.Show("Do you really want to delete " + empName + " from database?", "WARNING!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {


                    //valid ID was selected
                    try
                    {
                        //get user password from emp_ID number
                        connection.Open();
                        string getPassword = "select [Password] from EmployeeData where EMP_ID=" + emp_ID + "";
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandText = getPassword;
                        OleDbDataReader drGetPassword = command.ExecuteReader();
                        drGetPassword.Read();
                        userPassword = drGetPassword["Password"].ToString();
                        connection.Close();

                        //delete from TimeClock table
                        connection.Open();
                        string deleteTimeClockEntry = "delete from TimeClock where [UserPassword]='" + userPassword + "'";
                        command.CommandText = deleteTimeClockEntry;
                        command.ExecuteNonQuery();
                        connection.Close();

                        //delete from PayPeriod Table                    
                        connection.Open();
                        command.Connection = connection;
                        string deletePayPeriodEntry = "delete from PayPeriod where [UserPassword]='" + userPassword + "'";
                        command.CommandText = deletePayPeriodEntry;
                        command.ExecuteNonQuery();
                        connection.Close();

                        //delete from PayChecks Table                    
                        connection.Open();
                        command.Connection = connection;
                        string deletePayChecksEntry = "delete from PayChecks where [userPassword]='" + userPassword + "'";
                        command.CommandText = deletePayChecksEntry;
                        command.ExecuteNonQuery();
                        connection.Close();

                        //delete from EmployeeData Table
                        connection.Open();
                        command.Connection = connection;
                        string deleteEmployeeQuery = "delete from EmployeeData where [EMP_ID]=" + emp_ID + "";
                        command.CommandText = deleteEmployeeQuery;
                        command.ExecuteNonQuery();
                        MessageBox.Show(empName + "'s Record Deleted! ", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        connection.Close();

                        //clear Combo box entry
                        cbEmpID.Text = null;
                        cbEmpID.SelectedItem = null;

                        //reload table data
                        LoadData();
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
            }
            
        }

        private void cbEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Split combobox string in order to select just the EMP_ID
            //create a delimiter definition (a space is my delimiter)
            char delimiter = ' ';
            string text = cbEmpID.Text;
            string[] words = text.Split(delimiter);
            string emp_ID = words[0];
            string userPassword = "";
           

            try
            {
                connection.Open();               
                               
                //use the combobox to get the user ID and then retrieve user password
                //the user password is the primary key for the TimeClock table
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string getPasswordQuery = "select [Password] from EmployeeData where EMP_ID="+emp_ID+" ";
                command.CommandText = getPasswordQuery;
                OleDbDataReader pwReader = command.ExecuteReader();
                while (pwReader.Read())
                {
                    userPassword = pwReader["Password"].ToString();
                }
                pwReader.Close();

                //use the user password to load data into the timecard table
                string loadTimeCard = "select ClockedIn,ClockedOut,HoursWorked,TotalHoursWorked,payedOut from TimeClock where UserPassword='" +userPassword + "'";
                command.CommandText = loadTimeCard;
                OleDbDataAdapter daTimeCard = new OleDbDataAdapter(command);
                DataTable dtTimeCard = new DataTable();
                daTimeCard.Fill(dtTimeCard);
                dgvTimeTable.DataSource = dtTimeCard;
                dgvTimeTable.AutoResizeColumns();
                dgvTimeTable.ClearSelection();
                dgvTimeTable.CurrentCell = null;

                //user the user password to load paycheck history data into the timecard table
                string loadPayHistory = "select Check_ID,HoursPaid,GrossPay,PayDate from PayChecks where userPassword='"+userPassword+"'";
                command.CommandText = loadPayHistory;
                OleDbDataAdapter daPayHistory = new OleDbDataAdapter(command);
                DataTable dtPayHistory = new DataTable();
                daPayHistory.Fill(dtPayHistory);
                dgvPayHistory.DataSource = dtPayHistory;
                dgvPayHistory.AutoResizeColumns();
                dgvPayHistory.ClearSelection();
                dgvPayHistory.CurrentCell = null;

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

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string payedOut = "no";
            string userPassword;
            string totalPay="";            

            //create a dictionary to store userPassword and totalpay as strings from database
            Dictionary<string, string> dictionaryPayStrings = new Dictionary<string, string>();

            //create a second dictionary to store userPassword as string and totalpay as double converted to seconds
            Dictionary<string, double> dictionaryPayInSeconds = new Dictionary<string, double>();

            //create a third dictionary to store userPassword and pay amount
            Dictionary<string, double> dictionaryPayAmount = new Dictionary<string, double>();

            try
            {
                connection.Open();

                //Populate dictionary from PayPeriod Table
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string readPayPeriodTable = "select UserPassword,TotalHours from PayPeriod";
                command.CommandText = readPayPeriodTable;
                OleDbDataReader pwReader = command.ExecuteReader();
                while (pwReader.Read())
                {
                    userPassword = pwReader["UserPassword"].ToString();
                    totalPay = pwReader["TotalHours"].ToString();
                    dictionaryPayStrings.Add(userPassword, totalPay);
                }
                pwReader.Close();
                connection.Close();

                //convert dictionary time from string to double and populate dictionaryPayInSeconds 
                foreach(KeyValuePair<string,string> userPw in dictionaryPayStrings)
                {
                    //convert time to seconds and add to a separate dictionary
                    TimeSpan time = TimeSpan.Parse(userPw.Value);                    
                    double sec = time.TotalSeconds;
                    dictionaryPayInSeconds.Add(userPw.Key, sec);                    
                }
                
                //convert payInSeconds to gross pay and populate payAmount Dictionary
                foreach(KeyValuePair<string,double> userIDNum in dictionaryPayInSeconds)
                {
                    
                    connection.Open();
                    string readPayRate = "select RatePerHour from EmployeeData where [Password]='"+userIDNum.Key+"'";
                    command.CommandText = readPayRate;
                    OleDbDataReader payRateReader = command.ExecuteReader();
                    payRateReader.Read();
                    double payPerSecond = (Convert.ToDouble(payRateReader["RatePerHour"].ToString()) / 3600);
                    double grossPay = payPerSecond * (userIDNum.Value);
                    dictionaryPayAmount.Add(userIDNum.Key, grossPay);
                    payRateReader.Close();
                    connection.Close();

                    //show paid out date on timeclock and reset totalhours in paycheck table
                    connection.Open();
                    string editPayedOut = "update TimeClock set PayedOut='" +  dt + "' where UserPassword='" + userIDNum.Key + "' and PayedOut='"+payedOut+"'";
                    command.CommandText = editPayedOut;
                    command.ExecuteNonQuery();
                    string resetTotalHours = "update PayPeriod set TotalHours='"+"0:0:0"+"'where UserPassword='"+userIDNum.Key+"'";
                    command.CommandText = resetTotalHours;
                    command.ExecuteNonQuery();
                    connection.Close();

                    //record paycheck amount in PayChecks table
                    connection.Open();
                    string recordPaycheck = "insert into PayChecks (userPassword,GrossPay,PayDate) values('" + userIDNum.Key + "','" + grossPay.ToString("C") + "','"+dt+"')";
                    command.CommandText = recordPaycheck;
                    command.ExecuteNonQuery();
                    connection.Close();

                    //read paycheck number from PayChecks table
                    connection.Open();
                    string readCheckNumber = "select Check_ID from PayChecks where userPassword='"+userIDNum.Key+"'";
                    command.CommandText = readCheckNumber;
                    OleDbDataReader drCheckNum = command.ExecuteReader();
                    drCheckNum.Read();
                    string tempCheckNum = drCheckNum["Check_ID"].ToString();
                    drCheckNum.Close();
                    
                    //recheck check number to make sure it is the latest(largest) number
                    string checkNum = "";
                    OleDbDataReader drCheckNumFinalCheck = command.ExecuteReader();
                    while (drCheckNumFinalCheck.Read())
                    {
                        if (Convert.ToInt32(drCheckNumFinalCheck["Check_ID"].ToString())>=Convert.ToInt32(tempCheckNum))
                        {
                            //If the reader finds a larger check number than tempCheckNum store the larger number in checkNum
                            checkNum = drCheckNumFinalCheck["Check_ID"].ToString();

                        }
                    }
                    drCheckNumFinalCheck.Close();
                    connection.Close();

                    //get user hours from paystrings dictionary
                    string userhours = dictionaryPayStrings[userIDNum.Key];
                    //write hoursPaid to paychecks table
                    connection.Open();
                    string writeHoursPaid = "update PayChecks set HoursPaid='"+userhours+"' where Check_ID="+checkNum+"";
                    command.CommandText = writeHoursPaid;
                    command.ExecuteNonQuery();                    
                    connection.Close();
                    
                }
                connection.Close();
                MessageBox.Show("Payroll Payout Complete!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("There was an error!\n" + ex, "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }



        }         
      
    }
}
