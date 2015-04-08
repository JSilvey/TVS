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


                //Fill TimeCard Table
                /*command.CommandText = loadTimeClockTableQuery;
                OleDbDataAdapter daTimeClock = new OleDbDataAdapter(command);
                DataTable dtTimeClock = new DataTable();
                daTimeClock.Fill(dtTimeClock);
                dgvTimeClock.DataSource = dtTimeClock;
                */
                //close db connection
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
            AddEmployeeForm addEmp = new AddEmployeeForm();
            addEmp.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        
    }
}
