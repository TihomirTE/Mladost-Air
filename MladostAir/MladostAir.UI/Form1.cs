using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
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
            var db = new MladostAirDbContext();
            var customer = new Customer
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Age = int.Parse(ageTextBox.Text)
            };

            db.Customers.AddOrUpdate(customer);
            db.SaveChanges();
          
            MessageBox.Show("Database Updated");
            MessageBox.Show(db.Customers.Count().ToString());
        }
    }
}
