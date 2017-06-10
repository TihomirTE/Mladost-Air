using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MladostAir.Data;
using MladostAir.Models;
using MladostAir.Run;

namespace MladostAir.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this._Mladost_AirDataSet);
        }

        DatabaseConnection objConnection;
        string conString;

        DataSet ds;
        DataRow dRow;

        int MaxRows;
        int inc = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_Mladost_AirDataSet.Customers' table. You can move, or remove it, as needed.
            // this.customersTableAdapter.Fill(this._Mladost_AirDataSet.Customers);
            try
            {
                objConnection = new DatabaseConnection();
                conString = Properties.Settings.Default.Mladost_AirConnectionString;
                objConnection.connection_string = conString;
                objConnection.Sql = Properties.Settings.Default.SQL;
                ds = objConnection.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                NavigateRecords();
                BindComboBox();
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void NavigateRecords()
        {
            dRow = ds.Tables[0].Rows[inc];
            firstNameTextBox.Text = dRow.ItemArray.GetValue(1).ToString();
            lastNameTextBox.Text = dRow.ItemArray.GetValue(2).ToString();
            customerNumberTextBox.Text = dRow.ItemArray.GetValue(3).ToString();
            ageTextBox.Text = dRow.ItemArray.GetValue(4).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO ADD VALIDATION
            DataRow newRow = ds.Tables[0].NewRow();
            newRow[1] = firstNameTextBox.Text;
            newRow[2] = lastNameTextBox.Text;
            newRow[3] = customerNumberTextBox.Text;
            newRow[4] = int.Parse(ageTextBox.Text);
            ds.Tables[0].Rows.Add(newRow);
            //var db = new MladostAirDbContext();
            //var customer = new Customer
            //{
            //    FirstName = firstNameTextBox.Text,
            //    LastName = lastNameTextBox.Text,
            //    CustomerNumber = int.Parse(customerNumberTextBox.Text),
            //    Age = int.Parse(ageTextBox.Text),
            //};

            //db.Customers.Add(customer);
            //db.SaveChanges();

            // MessageBox.Show("Database Updated");
            try
            {
                objConnection.UpdateDatabase(ds);
                BindComboBox();
                MaxRows += 1;
                inc = MaxRows - 1;
                MessageBox.Show("Database Updated");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //TODO ADD VALIDATION

        }

        private void BindComboBox()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Mladost-Air;Integrated Security=True");
            SqlCommand sc = new SqlCommand("select * from customers", conn);
            SqlDataReader reader;
            conn.Open();
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Load(reader);

            //comboBox1.ValueMember = "name";
            comboBox1.DisplayMember = "id";
            comboBox1.DataSource = dt;

            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds.Tables[0].Rows[inc].Delete();
                objConnection.UpdateDatabase(ds);
                BindComboBox();

                if (inc != 0)
                {
                    inc--;
                }

                MessageBox.Show("Record deleted!");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            inc = selectedIndex;
            NavigateRecords();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (inc != MaxRows - 1)
            {
                inc++;
                NavigateRecords();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (inc != 0)
            {
                inc--;
                NavigateRecords();
            }
        }
    }
}
