using SnakeGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace GameCenter
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            X_O XO = new X_O();
            XO.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            g2 game2 = new g2();
            game2.Show();

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MemoryGame MG = new MemoryGame(); 
            MG.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
