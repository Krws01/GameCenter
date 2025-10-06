using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCenter
{
    public partial class MemoryGame : Form
    {
        Random random = new Random();
        List<String> icons = new List<String>()
        {
            "!","!","N","N",",",",","k","k","b","b","v","v","w","w","z","z"
        };

        Label firstClicked,secondClicked;
        public MemoryGame()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }
        private void AssignIconsToSquares()
        {
            Label label;
            int randomNumber;

            for (int i = 0; i<tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is  Label)
                
                    label= (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;

                randomNumber = random.Next(0,icons.Count);
                label.Text = icons[randomNumber];

                icons.RemoveAt(randomNumber);



            }
        }

        private void MemoryGame_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home Home = new Home();
            Home.Show();
        }
        private void Cheekwinner()
        {
            Label label;
            for(int i = 0;i<tableLayoutPanel1.Controls.Count;i++)
            {
                label = tableLayoutPanel1.Controls[i]as Label;
                if(label != null && label.ForeColor==label.BackColor)
                return;
            }

            
            var result = MessageBox.Show(
                "فزت باللعبه مبروك 🎉\n",
                "You Win",
                MessageBoxButtons.OK,
                MessageBoxIcon.None);

            if (result == DialogResult.OK)
            {
                this.Hide();
                Home homeForm = new Home();
                homeForm.Show();
            }

        }
        private void label_click(object sender, EventArgs e)
        {
            // معناه انك اخترت مربعين
            if(firstClicked!=null&& secondClicked!=null)
                return;

            Label clikLabel =sender as Label;
            if (clikLabel == null)
                return;

            if(clikLabel.ForeColor==Color.White)
                return;

            if(firstClicked==null)
            {
                firstClicked = clikLabel;
                firstClicked.ForeColor = Color.White;
                return;

            }

                secondClicked = clikLabel;
                secondClicked.ForeColor = Color.White;

                Cheekwinner();


            if (firstClicked.Text==secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }else
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home homeForm = new Home();
            homeForm.Show();
        }

       

    }


}
