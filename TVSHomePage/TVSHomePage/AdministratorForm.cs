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

                //Clear and Fill combobox
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

                cbEmpID.SelectedItem = null;

                
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

        private void txtEditEmployee_Click(object sender, EventArgs e)
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

                cbEmpID.SelectedItem = null;
                                
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
                               
                
                //valid ID was selected
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string deleteEmployeeQuery = "delete from EmployeeData where [EMP_ID]=" + emp_ID + "";
                    command.CommandText = deleteEmployeeQuery;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Employee Record Deleted! ", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cbEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Split combobox string in order to select just the EMP_ID
            //create a delimiter definition (a space is my delimiter)
            char delimiter = ' ';
            string text = cbEmpID.Text;
            string[] words = text.Split(delimiter);
            string emp_ID = words[0];
            string userPassword="";

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
                string loadTimeCard = "select ClockedIn,ClockedOut,HoursWorked from TimeClock where UserPassword=" +userPassword + "";
                command.CommandText = loadTimeCard;
                OleDbDataAdapter daTimeCard = new OleDbDataAdapter(command);
                DataTable dtTimeCard = new DataTable();
                daTimeCard.Fill(dtTimeCard);
                dgvTimeTable.DataSource = dtTimeCard;

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
    }
}
