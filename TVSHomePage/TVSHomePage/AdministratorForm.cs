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

                string loadEmpComboBox = "select EMP_ID from EmployeeData";
                command.CommandText = loadEmpComboBox;
                OleDbDataReader idReader = command.ExecuteReader();
                while (idReader.Read())
                {
                    cbEmpID.Items.Add(idReader["EMP_ID"].ToString());
                }


                
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
            //open the add employee screen
            AddEmployeeForm addEmp = new AddEmployeeForm();
            addEmp.Show();
        }     

        private void btnReload_Click(object sender, EventArgs e)
        {
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
                //employee Id was selected, open form passing the ID number
                EditEmployeeForm editEmp = new EditEmployeeForm(cbEmpID.Text);
                editEmp.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //check to make sure the user has chosen an employee ID
            if (cbEmpID.Text == "")
            {
                //no employee ID selected
                MessageBox.Show("You must select an employee ID to delete!", "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //valid ID was selected
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string deleteEmployeeQuery = "delete from EmployeeData where [EMP_ID]=" + cbEmpID.Text + "";
                    command.CommandText = deleteEmployeeQuery;
                    MessageBox.Show(deleteEmployeeQuery);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Employee Record Deleted! ", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();

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
}
