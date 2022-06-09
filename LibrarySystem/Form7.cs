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
    public partial class Form7 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        public String id;
        public Form7()
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
            command.CommandText = "Select student_id as StudentID, student_fname as Firstname, student_lname as Lastname, student_mname as Middleinitial, student_address as Address, student_contact as Contact from students order by student_id";
            command.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        public void clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleInitial.Text = "";
            txtAddress.Text = "";
            txtMobileNumber.Text = "";
          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            button.Play();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Insert into students (student_fname,student_lname,student_mname,student_address,student_contact) Values ('"+txtFirstName.Text+"','"+txtLastName.Text+"','"+txtMiddleInitial.Text+"','"+txtAddress.Text+"','"+txtMobileNumber.Text+"')";
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                clear();
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button.Play();
            loadData();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMiddleInitial.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtMobileNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            button.Play();
            try
            {
                var result = MessageBox.Show("Are you sure you want to edit this entry?", "Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "Update students set student_fname = '"+txtFirstName.Text+"', student_lname = '"+txtLastName.Text+"', student_mname = '"+txtMiddleInitial.Text+"', student_address = '"+txtAddress.Text+"', student_contact = '"+txtMobileNumber.Text+"' where student_id =  "+id+"";
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
                var result = MessageBox.Show("Are you sure you want to delete this entry?", "Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "Delete * from students where student_id = " + id + "";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }
    }
}
