using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using MladostAir.Data;
using MladostAir.Models;
using MladostAir.Run;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MladostAir.UI
{
    public partial class Form1 : Form
    {
        private DatabaseConnection objConnection;
        private string conString;
        private DataSet ds;
        private DataRow dRow;
        private int MaxRows;
        private int inc;
        private int reportClicks;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_Mladost_AirDataSet.Tickets' table. You can move, or remove it, as needed.
            //this.ticketsTableAdapter.Fill(this._Mladost_AirDataSet.Tickets);
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
            //comboBox1.SelectedIndex = inc;
            UpdateLabel();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int num;
            if (!IsValidInput(firstNameTextBox.Text, firstNameTextBox)
                || !IsValidInput(lastNameTextBox.Text, lastNameTextBox)
                || !Int32.TryParse(ageTextBox.Text, out num) || !Int32.TryParse(customerNumberTextBox.Text, out num))
            {
                return;
            }

            DataRow newRow = ds.Tables[0].NewRow();
            newRow[1] = firstNameTextBox.Text;
            newRow[2] = lastNameTextBox.Text;
            newRow[3] = int.Parse(customerNumberTextBox.Text);
            newRow[4] = int.Parse(ageTextBox.Text);
            ds.Tables[0].Rows.Add(newRow);

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
            int num;
            if (!IsValidInput(firstNameTextBox.Text, firstNameTextBox)
                || !IsValidInput(lastNameTextBox.Text, lastNameTextBox)
                || !Int32.TryParse(ageTextBox.Text, out num) || !Int32.TryParse(customerNumberTextBox.Text, out num)
                || int.Parse(ageTextBox.Text) < 0 || int.Parse(ageTextBox.Text) > 170 || int.Parse(customerNumberTextBox.Text) < 0)
            {
                return;
            }

            DataRow row = ds.Tables[0].Rows[inc];

            row[1] = firstNameTextBox.Text;
            row[2] = lastNameTextBox.Text;
            row[3] = int.Parse(customerNumberTextBox.Text);
            row[4] = int.Parse(ageTextBox.Text);

            try
            {
                objConnection.UpdateDatabase(ds);
                BindComboBox();
                MessageBox.Show("Record updated");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private bool IsValidInput(string text, TextBox tb)
        {
            int number;
            Regex regex = new Regex(@"[\d]");
            if (Int32.TryParse(firstNameTextBox.Text, out number) || regex.IsMatch(tb.Text) || tb.Text == "")
            {
                MessageBox.Show("Invalid input! Please re-enter.");
                tb.Text = "";
                return false;
            }

            return true;
        }

        private void BindComboBox()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=Mladost-Air;Integrated Security=True");
            conn.Open();
           
            SqlCommand sc = new SqlCommand("select * from customers", conn);
            SqlDataReader reader = sc.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Load(reader);

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
            else
            {
                MessageBox.Show("Last record reached!");
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (inc != 0)
            {
                inc--;
                NavigateRecords();
            }
            else
            {
                MessageBox.Show("First record reached!");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            PdfPTable pdfTable = new PdfPTable(ds.Tables[0].Columns.Count);

            FileStream fs = new FileStream("../../../CustomersReport" + reportClicks + ".pdf",
                FileMode.Create, FileAccess.Write, FileShare.None);

            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                var pdfCell = new PdfPCell(new Phrase(column.Caption));
                pdfTable.AddCell(pdfCell);
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (var col in row.ItemArray)
                {
                    var pdfCell = new PdfPCell(new Phrase(col.ToString()));
                    pdfTable.AddCell(pdfCell);
                }
            }

            try
            {
                Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(pdfDocument, fs);

                pdfDocument.Open();
                pdfDocument.Add(pdfTable);
                pdfDocument.Close();

                MessageBox.Show("PDF created successfully!");
                reportClicks++;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void UpdateLabel()
        {
            recordLabel.Text = "RECORD " + (inc + 1) + " OUT OF " + MaxRows;
        }
    }
}
