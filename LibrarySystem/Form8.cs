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
    public partial class Form8 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        public String studentid;
        public String fname;
        public String lname;
        public String mname;
        public String address; 
        public String contact;
        public String bookid;
        public String category; 
        public String author;
        public String title;
        public String availability;
        public Boolean flag;
        public Boolean flag2;
        public Boolean check;
        public Form8()
        {
            InitializeComponent();
            button.SoundLocation = "Button.wav";
            hover.SoundLocation = "Hover.wav";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                using (OleDbCommand command1 = new OleDbCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = "Select * from students where student_id = " + txtID.Text + "";
                    command1.ExecuteNonQuery();
                    OleDbDataAdapter da1 = new OleDbDataAdapter(command1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    OleDbDataReader reader1 = command1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        int length = dt1.Rows.Count;
                        for (int i = 0; i < length; i++)
                        {
                            if (dt1.Rows[i].ItemArray[0].ToString().Equals(txtID.Text))
                            {
                                studentid = dt1.Rows[i].ItemArray[0].ToString();
                                fname = dt1.Rows[i].ItemArray[1].ToString();
                                lname = dt1.Rows[i].ItemArray[2].ToString();
                                mname = dt1.Rows[i].ItemArray[3].ToString();
                                address = dt1.Rows[i].ItemArray[4].ToString();
                                contact = dt1.Rows[i].ItemArray[5].ToString();
                                flag = true;
        
                            }

                        }


                    }
                    else
                    {
                        flag = false;
                    }
                }
                using(OleDbCommand command2 = new OleDbCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = "Select * from books where AccessionNumber = " + txtBookID.Text + "";
                    command2.ExecuteNonQuery();
                    OleDbDataAdapter da2 = new OleDbDataAdapter(command2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        int length = dt2.Rows.Count;
                        for (int i = 0; i < length; i++)
                        {
                            if (dt2.Rows[i].ItemArray[0].ToString().Equals(txtBookID.Text))
                            {
                               
                                    bookid = dt2.Rows[i].ItemArray[0].ToString();
                                    title = dt2.Rows[i].ItemArray[1].ToString();
                                    category = dt2.Rows[i].ItemArray[2].ToString();
                                    author = dt2.Rows[i].ItemArray[3].ToString();
                                    availability = dt2.Rows[i].ItemArray[4].ToString();
                                    flag2 = true;
                            }

                        }


                    }
                    else
                    {
                        flag2 = false;
                    }

                }
                if(flag==true&&flag2==true)
                {
                    if(availability=="Unavailable")
                    {
                        MessageBox.Show("Sorry this book is currently unavailable", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txtID.Text = "";
                        this.txtBookID.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("OK", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form9 f9 = new Form9(studentid, fname, lname, mname, address, contact, bookid, title, category, author);
                        f9.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid ID", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtID.Text = "";
                    this.txtBookID.Text = "";
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            txtID.Focus();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBookID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
