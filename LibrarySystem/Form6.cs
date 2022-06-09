using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace LibrarySystem
{
    public partial class Form6 : Form
    {
        System.Media.SoundPlayer button = new System.Media.SoundPlayer();
        System.Media.SoundPlayer hover = new System.Media.SoundPlayer();
        System.Media.SoundPlayer clock = new System.Media.SoundPlayer();
        public Form6()
        {
            InitializeComponent();
            button.SoundLocation = "Button.wav";
            hover.SoundLocation = "Hover.wav";
            clock.SoundLocation = "Tick.wav";
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
            clock.Play();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button.Play();
            var result = MessageBox.Show("Are you sure you want to log out?", "Message", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form4 f4 = new Form4();
                f4.Show();
                MessageBox.Show("Successfully logged out", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form f8 = new Form8();
            f8.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button.Play();
            this.Hide();
            Form10 f10 = new Form10();
            f10.Show();
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void linkLabel1_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button.Play();
        }
        

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            hover.Play();
        }
    }
}
