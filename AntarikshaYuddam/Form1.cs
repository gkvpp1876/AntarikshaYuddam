using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntarikshaYuddam
{
    public partial class Form1 : Form
    {
        //To display starts in the background
        PictureBox[] stars;
        //For background speed control
        int backgroundSpeed;
        //To dynamically position our starts in the screen
        Random rnd;
        //To control the player's movement
        int playerSpeed;
        //To shoot bullets
        PictureBox[] munitions;
        //To control the speed of the bullets
        int munitionSpeed;

        public Form1()
        {
            InitializeComponent();
        }

        // To initialize game window and the stars in the background
        private void Form1_Load(object sender, EventArgs e)
        {
            //these numbers assiging here are based on pixel values
            backgroundSpeed = 4;
            playerSpeed = 4;

            munitionSpeed = 20;
            munitions = new PictureBox[3];
            //Image munition = Image.FromFile(@"asserts\munition.png"); //unable to load image using this method

            for (int i = 0; i < munitions.Length; i++)
            {
                munitions[i] = new PictureBox
                {
                    Size = new Size(8, 8),
                    //BackgroundImage = munition,
                    BorderStyle = BorderStyle.None,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    ImageLocation = @"asserts\munition.png"
                };
                this.Controls.Add(munitions[i]);
            }

            stars = new PictureBox[10];
            rnd = new Random();

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox
                {
                    BorderStyle = BorderStyle.None,
                    Location = new System.Drawing.Point(rnd.Next(20, this.Size.Width), rnd.Next(-10, this.Size.Height))
                };

                if (i % 2 == 0)
                {
                    stars[i].Size = new System.Drawing.Size(2, 2);
                    stars[i].BackColor = System.Drawing.Color.White;
                }
                else
                {
                    stars[i].Size = new System.Drawing.Size(3, 3);
                    stars[i].BackColor = System.Drawing.Color.DarkGray;
                }
                this.Controls.Add(stars[i]);
            }
        }

        //To make some of the stars move faster than the others and continuously loop them in the background
        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length / 2; i++)
            {
                stars[i].Top += backgroundSpeed;

                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }

            for (int i = stars.Length / 2; i < stars.Length; i++)
            {
                stars[i].Top += backgroundSpeed - 2;
                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 10)
            {
                Player.Left -= playerSpeed;
            }
        }

        //To control the player's movement to the Right we use Player Left to increase as PictureBox Right property is private
        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Right < this.Size.Width - 20)
            {
                Player.Left += playerSpeed;
            }
        }

        //To control the player's movement to the Down we use Player Top to increase as PictureBox Down property is private
        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top < this.Size.Height - 40)
            {
                Player.Top += playerSpeed;
            }
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
            {
                Player.Top -= playerSpeed;
            }
        }

        //To control the player's movement using the arrow keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                LeftMoveTimer.Start();
            }
            if (e.KeyCode == Keys.Right)
            {
                RightMoveTimer.Start();
            }
            if (e.KeyCode == Keys.Down)
            {
                DownMoveTimer.Start();
            }
            if (e.KeyCode == Keys.Up)
            {
                UpMoveTimer.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMoveTimer.Stop();
            LeftMoveTimer.Stop();
            DownMoveTimer.Stop();
            UpMoveTimer.Stop();
        }

        //To shoot bullets continuously using timer until game ends
        private void MoveMunationTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < munitions.Length; i++)
            {
                if (munitions[i].Top > 0)
                {
                    munitions[i].Visible = true;
                    munitions[i].Top -= munitionSpeed;
                }
                else
                {
                    munitions[i].Visible = false;
                    munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30);
                    //munitions[i].Top = Player.Top;
                    //munitions[i].Left = Player.Left + Player.Width / 2;
                }
            }
        }
    }
}
