using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCenter
{
    public partial class X_O : Form
    {
        private char currentPlayer = 'X';
        private int movesCount = 0;
        public X_O()
        {
            InitializeComponent();
            lblTurn.Text = "X دور اللاعب";
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Text == "")
            {
                btn.Text = currentPlayer.ToString();
                btn.ForeColor = (currentPlayer == 'X') ? Color.Red : Color.Blue; // لون حسب اللاعب
                movesCount++;

                if (CheckWin())
                {
                    HighlightWin(currentPlayer);
                    DialogResult dr = MessageBox.Show($"اللاعب {currentPlayer} فاز! 🎉\nهل تريد إعادة اللعب؟",
                                                      "انتهاء اللعبة",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                        ResetGame();
                    else
                    {
                        this.Hide();
                        Home Home = new Home();
                        Home.Show();
                    }
                }
                else if (movesCount == 9)
                {
                    DialogResult dr = MessageBox.Show("تعادل! 🤝\nهل تريد إعادة اللعب؟",
                                                      "انتهاء اللعبة",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                        ResetGame();
                    else
                    {
                        this.Hide();
                        Home Home = new Home();
                        Home.Show();
                    }
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    lblTurn.Text = $" {currentPlayer}دور اللاعب";
                }
            }
        }

        private bool CheckWin()
        {
            return (CheckLine(btn1, btn2, btn3) ||
                    CheckLine(btn4, btn5, btn6) ||
                    CheckLine(btn7, btn8, btn9) ||
                    CheckLine(btn1, btn4, btn7) ||
                    CheckLine(btn2, btn5, btn8) ||
                    CheckLine(btn3, btn6, btn9) ||
                    CheckLine(btn1, btn5, btn9) ||
                    CheckLine(btn3, btn5, btn7));
        }

        private bool CheckLine(Button b1, Button b2, Button b3)
        {
            return (b1.Text == currentPlayer.ToString() &&
                    b2.Text == currentPlayer.ToString() &&
                    b3.Text == currentPlayer.ToString());
        }

        private void HighlightWin(char winner)
        {
            // يغير لون الخط الفائز
            if (CheckLine(btn1, btn2, btn3)) { btn1.BackColor = btn2.BackColor = btn3.BackColor = Color.LightGreen; }
            if (CheckLine(btn4, btn5, btn6)) { btn4.BackColor = btn5.BackColor = btn6.BackColor = Color.LightGreen; }
            if (CheckLine(btn7, btn8, btn9)) { btn7.BackColor = btn8.BackColor = btn9.BackColor = Color.LightGreen; }
            if (CheckLine(btn1, btn4, btn7)) { btn1.BackColor = btn4.BackColor = btn7.BackColor = Color.LightGreen; }
            if (CheckLine(btn2, btn5, btn8)) { btn2.BackColor = btn5.BackColor = btn8.BackColor = Color.LightGreen; }
            if (CheckLine(btn3, btn6, btn9)) { btn3.BackColor = btn6.BackColor = btn9.BackColor = Color.LightGreen; }
            if (CheckLine(btn1, btn5, btn9)) { btn1.BackColor = btn5.BackColor = btn9.BackColor = Color.LightGreen; }
            if (CheckLine(btn3, btn5, btn7)) { btn3.BackColor = btn5.BackColor = btn7.BackColor = Color.LightGreen; }
        }

        private void ResetGame()
        {
            foreach (Control c in Controls)
            {
                if (c is Button btn && btn.Name.StartsWith("btn") && btn.Name != "btnBackHome" && btn.Name != "btnRestart")
                {
                    btn.Text = "";
                    btn.BackColor = Color.White;
                }
            }
            currentPlayer = 'X';
            movesCount = 0;
            lblTurn.Text = "X دور اللاعب";
        }

      
       

        private void X_O_Load(object sender, EventArgs e)
        {

        }

      

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void btnBackHome_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Home Home = new Home();
            Home.Show();
        }
    }
}
