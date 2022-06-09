using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace LibrarySystem
{
    public partial class Form9 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        public String studentid;
        public String bookid;
        public Form9(String studentid, String fname, String lname, String mname, String address, String contact, String bookid, String title, String category, String author)
        {
            InitializeComponent();
            this.studentid = studentid;
            this.bookid = bookid;
            txtFirstName.Text = fname;
            txtLastName.Text = lname;
            txtMiddleInitial.Text = mname;
            txtAddress.Text = address;
            txtMobileNumber.Text = contact;
            txtTitle.Text = title;
            txtCategory.Text = category;
            txtAuthor.Text = author;
            txtDate.Text = DateTime.Now.ToShortDateString();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtMiddleInitial.Enabled = false;
            txtAddress.Enabled = false;
            txtMobileNumber.Enabled = false;
            txtTitle.Enabled = false;
            txtCategory.Enabled = false;
            txtAuthor.Enabled = false;

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8();
            f8.Show();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            connection.Open();
            using (OleDbCommand command1 = new OleDbCommand())
            {
                command1.Connection = connection;
                command1.CommandText = "Update books set Availability = 'Unavailable' where AccessionNumber = " + bookid + "";
                command1.ExecuteNonQuery();

            }
            using (OleDbCommand command2 = new OleDbCommand())
            {
                command2.Connection = connection;
                command2.CommandText = "Insert into transactions (student_id,book_id,title,category,author,date_borrowed,return_date,penalty) Values ('" + studentid + "','" + bookid + "','" + txtTitle.Text + "','" + txtCategory.Text + "','" + txtAuthor.Text + "','" + txtDate.Text + "','" + dateTimePicker1.Text + "','0')";
                command2.ExecuteNonQuery();
            }

            MessageBox.Show("Book successfully issued", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }
    }
}
