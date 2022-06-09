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
    public partial class Form2 : Form
    {
        System.Media.SoundPlayer menu = new System.Media.SoundPlayer();
        public Form2()
        {
            InitializeComponent();
            menu.SoundLocation = "Menu.wav";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            menu.Play();

        }
    }
}
