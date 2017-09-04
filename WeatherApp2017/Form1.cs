using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp2017
{
    public partial class Form1 : Form
    {
        System.Random random;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            random = new System.Random();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image image = Properties.Resources.sunny;
            switch (random.Next(0, 3))
            {
                case 0:
                    image = Properties.Resources.sunny;
                    break;
                case 1:
                    image = Properties.Resources.cloudy;
                    break;
                case 2:
                    image = Properties.Resources.raining;
                    break;
                case 3:
                    image = Properties.Resources.snowy;
                    break;
            }
            pictureBox1.Image = image;
        }
    }
}
