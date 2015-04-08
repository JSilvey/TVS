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
    public partial class AddEmployeeForm : Form
    {
        //create a gloabal connection to the database
        private OleDbConnection connection = new OleDbConnection();

        public AddEmployeeForm()
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //close the connection to database and close the form
            connection.Close();
            this.Close();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string addEmployeeNonQuery = "insert into EmployeeData (FirstName,LastName,StreetAddress,City,State,Zip,Phone,SSN,DOB,[Password],Role,Title,RatePerHour) values('" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtStreetAddress.Text + "','" + txtCity.Text + "','" + txtState.Text + "','" + txtZip.Text + "','" + txtPhone.Text + "','" + txtSSN.Text + "','" + txtDOB.Text + "','" + txtPassword.Text + "','" + txtRole.Text + "','" + txtTitle.Text + "', '" + txtRate.Text + "')";
                command.CommandText = addEmployeeNonQuery;
                MessageBox.Show(addEmployeeNonQuery);
                command.ExecuteNonQuery();
                MessageBox.Show(txtFirstName.Text + " " + txtLastName.Text + " was added to the employee database.","Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();

                ClearTextBoxes();


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

        private void ClearTextBoxes()
        {
            //clear all text boxes
            txtFirstName.Clear();
            txtLastName.Clear();
            txtStreetAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtZip.Clear();
            txtPhone.Clear();
            txtSSN.Clear();
            txtDOB.Clear();
            txtPassword.Clear();
            txtTitle.Clear();
            txtRate.Clear();
            txtRole.Clear();

            //return focus to first textbox
            txtFirstName.Focus();

        }
    }
}
