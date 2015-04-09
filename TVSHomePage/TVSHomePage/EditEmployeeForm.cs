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
            this.Close();
        }

        private void btnEditEmp_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string editEmployeeNonQuery = "update EmployeeData set [FirstName]='" + txtFirstName.Text + "', [LastName]='" + txtLastName.Text + "', [StreetAddress]='" + txtStreetAddress.Text + "' ,[City]='" + txtCity.Text + "' ,[State]='" + txtState.Text + "' ,[Zip]='" + txtZip.Text + "' ,[Phone]='" + txtPhone.Text + "' ,[SSN]='" + txtSSN.Text + "' ,[DOB]='" + txtDOB.Text + "' ,[Role]='" + txtRole.Text + "' ,[Title]='" + txtTitle.Text + "' ,[RatePerHour]='" + txtRate.Text + "'where [EMP_ID]=" + empID + "";
                command.CommandText = editEmployeeNonQuery;
                MessageBox.Show(editEmployeeNonQuery);
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
    }
}
