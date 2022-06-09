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
    public partial class Form5 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        public Form5()
        {
            InitializeComponent();
            button.SoundLocation = "Button.wav";
            hover.SoundLocation = "Hover.wav";
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            button.Play();
            try
            {
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please fill up", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtPassword.Text != txtReenter.Text)
                {
                    MessageBox.Show("Mismatched password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtReenter.Text = "";
                    return;
                }
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Insert into users ([user_fname],[user_lname],[user_address],[user_email],[user_contact],[user_username],[user_password]) Values('" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtAddress.Text + "','" + txtEmail.Text + "','" + txtContact.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "')";
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully registered", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                this.Hide();
                Form4 f4 = new Form4();
                f4.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void btnRegister_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }
    }
}
