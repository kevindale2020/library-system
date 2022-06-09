using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;

namespace LibrarySystem
{
    public partial class Form4 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        public Form4()
        {
            Thread t = new Thread(new ThreadStart(SplashStart));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            button.SoundLocation = "Button.wav";
            hover.SoundLocation = "Hover.wav";
            t.Abort();
        }
        public void SplashStart()
        {
            Application.Run(new Form2());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button.Play();
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            button.Play();
            try
            {
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please fill up everything", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from Users where user_username = '" + txtUsername.Text + "' and user_password = '" + txtPassword.Text + "'";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while(reader.Read())
                {
                    count++;
                }
                if(count==1)
                {
                    MessageBox.Show("Welcome " + txtUsername.Text,"Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Hide();
                    Form6 f6 = new Form6();
                    f6.Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            button.Play();
            Application.Exit();
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void linkLabel1_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }
    }
}
