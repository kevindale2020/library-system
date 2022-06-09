using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace LibrarySystem
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Application.StartupPath+"\\BookDatabase2.mdb");
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        public String bookid;
        public Form1()
        {
            InitializeComponent();
            button.SoundLocation = "Button.wav";
            hover.SoundLocation = "Hover.wav";
        }
        public void loadData()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "Select * from books order by AccessionNumber";
            command.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        public void clear()
        {
            txtTitle.Text = "";
            txtAuthor.Text = "";
            comboBox1.ResetText();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String availability = "Available";
            button.Play();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Insert into books ([Title],[Category],[Author],[Availability]) Values('" + txtTitle.Text + "','"+comboBox1.Text+"','" + txtAuthor.Text + "','"+availability+"')";
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                loadData();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bookid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button.Play();
            loadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            button.Play();
            try
            {
                 var result = MessageBox.Show("Are you sure you want to edit this entry?","Message",MessageBoxButtons.YesNo);
                 if (result == DialogResult.Yes)
                 {
                     connection.Open();
                     OleDbCommand command = new OleDbCommand();
                     command.Connection = connection;
                     command.CommandText = "Update books set [Title] = '" + txtTitle.Text + "',[Category] = '"+comboBox1.Text+"', [Author] = '" + txtAuthor.Text + "' where [AccessionNumber] = " + bookid + "";
                     command.ExecuteNonQuery();
                     MessageBox.Show("Successfully updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     connection.Close();
                     loadData();
                     clear();
                 }
                 else
                 {
                     return;
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            button.Play();
            try
            {
                 var result = MessageBox.Show("Are you sure you want to delete this entry?","Message",MessageBoxButtons.YesNo);
                 if (result == DialogResult.Yes)
                 {
                     connection.Open();
                     OleDbCommand command = new OleDbCommand();
                     command.Connection = connection;
                     command.CommandText = "Delete * from books where [AccessionNumber] = " + bookid + "";
                     command.ExecuteNonQuery();
                     MessageBox.Show("Successfully deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     connection.Close();
                     loadData();
                     clear();
                 }
                 else
                 {
                     return;
                 }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from books where [AccessionNumber] like '" + txtSearch.Text + "%' or Title like '" + txtSearch.Text + "%' or Author like '"+txtSearch.Text+"%'";
                command.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          comboBox1.Items.Add("Mathematics");
          comboBox1.Items.Add("Science");
          comboBox1.Items.Add("English");
          comboBox1.Items.Add("History");
          comboBox1.Items.Add("Fiction");
          comboBox1.Items.Add("Novel");
          comboBox1.Items.Add("Fantasy");
          comboBox1.Items.Add("Sci-fi");
          comboBox1.Items.Add("Romance");
          comboBox1.Items.Add("Mystery");
          comboBox1.Items.Add("Horror");
          comboBox1.Items.Add("Comedy");
          
        }


        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void btnEdit_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void btnPrint_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }
    }
}
