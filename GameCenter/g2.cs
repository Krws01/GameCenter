using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameCenter;

namespace SnakeGame 
{
    public partial class g2 : Form
    {
        private Timer gameTimer;
        private List<Rectangle> snake; // جسم الثعبان
        private Rectangle food;
        private int direction; // 3=يمين=0 أسفل=1 يسار=2 أعلى 
        private int score;
        private Random random;
        private int cellSize = 20; // حجم الخلية

        public g2()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // للتنقيط البصري في الرسم

            this.Width = 600;
            this.Height = 400;

            this.KeyPreview = true;
            this.KeyDown += SnakeForm_KeyDown;
            this.Paint += SnakeForm_Paint;

            gameTimer = new Timer();
            gameTimer.Interval = 100; // السرعة
            gameTimer.Tick += GameTimer_Tick;

            InitializeGame();
        }

        private void InitializeGame()
        {
            score = 0;
            direction = 0; 
            random = new Random();

            // البدء بجسم طوله 3 وحدات في المنتصف 
            snake = new List<Rectangle>();

            int startX = this.ClientSize.Width / 2;
            int startY = this.ClientSize.Height / 2;

            for (int i = 0; i < 3; i++)
            {
                snake.Add(new Rectangle(startX - i * cellSize, startY, cellSize, cellSize));
            }

            GenerateFood();
            gameTimer.Start();
        }

        private void GenerateFood()
        {
            int maxX = this.ClientSize.Width / cellSize;
            int maxY = this.ClientSize.Height / cellSize;

            Rectangle newFood;
            while (true) // ضمان عدم تكرار موقع الطعام مع جسم الثعبان
            {
                int x = random.Next(0, maxX) * cellSize;
                int y = random.Next(0, maxY) * cellSize;
                newFood = new Rectangle(x, y, cellSize, cellSize);
                bool conflict = false;
                foreach (var part in snake)
                {
                    if (part.IntersectsWith(newFood))
                    {
                        conflict = true;
                        break;
                    }
                }
                if (!conflict)
                    break;
            }
            food = newFood;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
        }

        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            // منع تغيير الاتجاه للاتجاه المعاكس
            if (e.KeyCode == Keys.Right && direction != 2)
                direction = 0;
            else if (e.KeyCode == Keys.Down && direction != 3)
                direction = 1;
            else if (e.KeyCode == Keys.Left && direction != 0)
                direction = 2;
            else if (e.KeyCode == Keys.Up && direction != 1)
                direction = 3;
        }

        private void MoveSnake()
        {
            Rectangle head = snake[0];
            Rectangle newHead = head;

            switch (direction)
            {
                case 0: // يمين
                    newHead.X += cellSize;
                    break;
                case 1: // تحت
                    newHead.Y += cellSize;
                    break;
                case 2: // يسار
                    newHead.X -= cellSize;
                    break;
                case 3: // فوق
                    newHead.Y -= cellSize;
                    break;
            }

            //  اذا صدم بالجدار (Game Over)
            if (newHead.X < 0 || newHead.X >= this.ClientSize.Width
                || newHead.Y < 0 || newHead.Y >= this.ClientSize.Height)
            {
                GameOver();
                return;
            }

            //  اذا صدم بجسمه (Game Over)
            for (int i = 0; i < snake.Count; i++)
            {
                if (newHead.IntersectsWith(snake[i]))
                {
                    GameOver();
                    return;
                }
            }

           // اذا اكل الطعام
            if (newHead.IntersectsWith(food))
            {
              
                snake.Insert(0, newHead);
                score += 10;
                GenerateFood();
            }
            else
            {
                //حركه عاديه
                snake.Insert(0, newHead);
                snake.RemoveAt(snake.Count - 1);
            }

            this.Invalidate(); // طلب رسم جديد
        }

        private void SnakeForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // رسم جسم الثعبان
            foreach (var part in snake)
            {
                g.FillRectangle(Brushes.Green, part);// يرسم داخل المستطيل 
                g.DrawRectangle(Pens.Black, part); // يرسم بحدود المستطيل
            }

            g.FillRectangle(Brushes.YellowGreen, snake[0]); 
            g.DrawRectangle(Pens.Black, snake[0]);

            // رسم الطعام
            g.FillEllipse(Brushes.Red, food);
            g.DrawEllipse(Pens.Black, food);

            // رسم لوحة النقاط
            g.DrawString($"Score: {score}", this.Font, Brushes.White, 10, 10);
        }
        
        private void GameOver()
        {
            gameTimer.Stop();

            var result = MessageBox.Show(
                "انتهت اللعبة!\nهل تريد أعادة المحاولة؟",
                "Game Over",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                InitializeGame();
            }
            else if (result == DialogResult.No)
            {
                
                try
                {
                  
                    this.Hide();

                    Home homeForm = new Home();
                    homeForm.Show();

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ أثناء فتح القائمة الرئيسية: " + ex.Message);
                    Application.Exit();
                }
            }
        }
    }
}