using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MladostAir.Data;
using MladostAir.Models;
using ExternalFiles.Readers;

namespace MladostAir.UI
{
    public partial class Form1 : Form
    {
        private int maxRows;
        private int inc;
        private int reportClicks;

        private readonly MladostAirDbContext dbContext = new MladostAirDbContext();
        private readonly List<Customer> customersList;

        public Form1()
        {
            InitializeComponent();
            customersList = dbContext.Customers.ToList();
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

            this.BackColor = Color.FromArgb(204, 224, 180);
         
            try
            {
                maxRows = dbContext.Customers.Count();
                BindComboBox();
                NavigateRecords();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void NavigateRecords()
        {
            comboBox1.SelectedIndex = inc;
            firstNameTextBox.Text = customersList[inc].FirstName;
            lastNameTextBox.Text = customersList[inc].LastName;
            customerNumberTextBox.Text = customersList[inc].CustomerNumber.ToString();
            ageTextBox.Text = customersList[inc].Age.ToString();
            UpdateLabel();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int num;
            if (!IsValidInput(firstNameTextBox.Text, firstNameTextBox)
                || !IsValidInput(lastNameTextBox.Text, lastNameTextBox)
                || !int.TryParse(ageTextBox.Text, out num) || !int.TryParse(customerNumberTextBox.Text, out num))
            {
                return;
            }

            var customer = new Customer
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Age = int.Parse(ageTextBox.Text),
                CustomerNumber = int.Parse(customerNumberTextBox.Text)
            };

            customersList.Add(customer);
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            maxRows++;
            BindComboBox();

            MessageBox.Show("Record saved!");
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

            var id = comboBox1.Text;
            var customer = dbContext.Customers.First(c => c.Id.ToString() == id);
            customersList.Remove(customer);

            customer.FirstName = firstNameTextBox.Text;
            customer.LastName = lastNameTextBox.Text;
            customer.Age = int.Parse(ageTextBox.Text);
            customer.CustomerNumber = int.Parse(customerNumberTextBox.Text);

            dbContext.Customers.AddOrUpdate(customer);
            customersList.Add(customer);
            dbContext.SaveChanges();
            MessageBox.Show("Record updated!");
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
            dt.Columns.Add("id", typeof(string));
            dt.Load(reader);

            comboBox1.DisplayMember = "id";
            comboBox1.DataSource = dt;

            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var customer = dbContext.Customers.FirstOrDefault(c => c.FirstName == firstNameTextBox.Text &&
                                                                   c.LastName == lastNameTextBox.Text &&
                                                                   c.Age.ToString() == ageTextBox.Text &&
                                                                   c.CustomerNumber.ToString() ==
                                                                   customerNumberTextBox.Text);
            dbContext.Entry(customer).State = EntityState.Deleted;
            MessageBox.Show("Record deleted!");
            dbContext.SaveChanges();

            customersList.Remove(customer);

            BindComboBox();
            maxRows = customersList.Count;
            NavigateRecords();

            if (inc != 0)
            {
                inc--;
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
            if (inc != maxRows - 1)
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
            PdfPTable pdfTable = new PdfPTable(5);

            FileStream fs = new FileStream("../../../CustomersReport" + reportClicks + ".pdf",
                FileMode.Create, FileAccess.Write, FileShare.None);

            var idHeader = new PdfPCell(new Phrase("Id"));
            idHeader.BackgroundColor = BaseColor.GRAY;

            var firstNameHeader = new PdfPCell(new Phrase("First Name"));
            firstNameHeader.BackgroundColor = BaseColor.GRAY;

            var lastNameHeader = new PdfPCell(new Phrase("Last Name"));
            lastNameHeader.BackgroundColor = BaseColor.GRAY;

            var custNumberHeader = new PdfPCell(new Phrase("Customer Number"));
            custNumberHeader.BackgroundColor = BaseColor.GRAY;

            var ageHeader = new PdfPCell(new Phrase("Age"));
            ageHeader.BackgroundColor = BaseColor.GRAY;

            pdfTable.AddCell(idHeader);
            pdfTable.AddCell(firstNameHeader);
            pdfTable.AddCell(lastNameHeader);
            pdfTable.AddCell(custNumberHeader);
            pdfTable.AddCell(ageHeader);

            foreach (var customer in dbContext.Customers)
            {
                var idCell = new PdfPCell(new Phrase(customer.Id.ToString()));
                var fNameCell = new PdfPCell(new Phrase(customer.FirstName));
                var lNameCell = new PdfPCell(new Phrase(customer.LastName));
                var custNumberCell = new PdfPCell(new Phrase(customer.CustomerNumber.ToString()));
                var ageCell = new PdfPCell(new Phrase(customer.Age.ToString()));

                pdfTable.AddCell(idCell);
                pdfTable.AddCell(fNameCell);
                pdfTable.AddCell(lNameCell);
                pdfTable.AddCell(custNumberCell);
                pdfTable.AddCell(ageCell);
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
            recordLabel.Text = "RECORD " + (inc + 1) + " OUT OF " + maxRows;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            ageTextBox.Clear();
            customerNumberTextBox.Clear();
        }

        private void btnPopulate_Click(object sender, EventArgs e)
        {
            JsonReportFileReader.ReadJsonFile();
            MessageBox.Show("Database populated successfully!");
            btnPopulate.Enabled = false;
        }
    }
}
