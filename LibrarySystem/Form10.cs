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
    public partial class Form10 : Form
    {
        List<Panel> listPanel = new List<Panel>();
        int index;
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        public int penalty = 0;
        public String borrowid;
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        System.Media.SoundPlayer ok = new System.Media.SoundPlayer();
        public Form10()
        {
            InitializeComponent();
            button.SoundLocation = "Button.wav";
            hover.SoundLocation = "Hover.wav";
            ok.SoundLocation = "Ok.wav";
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public void loadData()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "Select t.id as ID, s.student_fname as Firstname, s.student_lname as LastName, b.Title as Title, b.category as Category, b.Author as Author, t.date_borrowed as DateBorrowed, t.return_date as ReturnDate, t.penalty as Penalty, t.status as Status from ((transactions t inner join students s on t.student_id = s.student_id) inner join books b on b.AccessionNumber = t.book_id) order by t.id desc";
            command.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }
        public void clear()
        {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
            txt9.Text = "";
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txt1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txt8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txt9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

            borrowid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            DateTime currentDate = DateTime.Now;
            DateTime returnDate = DateTime.Parse(this.txt8.Text);

            int diffDays = (returnDate - currentDate).Days;

            if(diffDays < 0)
            {
                penalty = Math.Abs(diffDays) * 25;
                MessageBox.Show("Book was returned " + Math.Abs(diffDays) + " days late, a penalty of " + penalty + " will be charge ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt9.Text = penalty.ToString();
            } else
            {
                MessageBox.Show("Book returned on time no penalty", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            button.Play();
            penalty = Convert.ToInt32(txt9.Text);
       
            try
            {
                connection.Open();
                using(OleDbCommand command1 = new OleDbCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = "Update transactions set penalty = '"+penalty+"', status = '"+txt10.Text+"' where id = "+txt1.Text+"";
                    command1.ExecuteNonQuery();
                }
                using (OleDbCommand command2 = new OleDbCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = "Update books set Availability = 'Available' where Title = '"+txt4.Text+"'";
                    command2.ExecuteNonQuery();
                }
                MessageBox.Show("OK", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                loadData();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ok.Play();
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
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
                    command.CommandText = "Delete * from transactions where id = "+borrowid+"";
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
    }
}
