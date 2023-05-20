using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MemoryGame2
{
    public partial class Form1 : Form
    {
        // List to hold the numbers that will be used to assign tags to pictures
        private List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };

        // Variables to keep track of user choices
        private string firstChoice;
        private string secondChoice;
        private PictureBox picA;
        private PictureBox picB;

        // Variables to keep track of game state and timing
        private bool gameOver = false;
        private int totalTime = 60;
        private int countDownTime;

        // List to hold the picture boxes that will be used in the game
        private List<PictureBox> pictures = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            LoadPictures(); // Load the picture boxes into the game
        }

        // Event handler for the timer tick
        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            label1.Text = "Time Left: " + countDownTime;
            if (countDownTime < 1)
            {
                // If the time runs out, end the game and reveal all pictures
                GameOver("You ran out of time :/");
                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile("Images/" + (string)x.Tag + ".jpg");
                    }
                }
            }
        }

        // Event handler for the hide timer tick
        private void HideTimer_Tick(object sender, EventArgs e)
        {
            // Hide the pictures
            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }

            // Reset the choices
            firstChoice = null;
            secondChoice = null;
        }

        // Event handler for the new game button click
        private void NewGameEvent(object sender, EventArgs e)
        {
            NewGame(); // Start a new game
        }

        // Load the picture boxes into the game
        private void LoadPictures()
        {
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 0; i < 16; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 100;
                newPic.Width = 100;
                newPic.BackColor = Color.DarkSeaGreen;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 4)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 120;
                }
                if (rows == 4)
                {
                    leftPos = 20;
                    topPos += 120;
                    rows = 0;
                }
            }
            NewGame(); // Start a new game
        }

        // Event handler for picture box clicks
        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver)
            {
                // Don't register a click if the game is over
                return;
            }
            if (firstChoice == null)
            {
                // First picture clicked
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Image == null)
                {
                    // Show the image of the first choice
                    picA.Image = Image.FromFile("Images/" + (string)picA.Tag + ".jpg");
                    firstChoice = (string)picA.Tag;
                }
            }
            else if (secondChoice == null)
            {
                // Second picture clicked
                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Image == null)
                {
                    // Show the image of the second choice
                    picB.Image = Image.FromFile("Images/" + (string)picB.Tag + ".jpg");
                    secondChoice = (string)picB.Tag;
                }
            }
            else
            {
                // Two pictures have been selected
                CheckPictures(picA, picB);
            }
        }

        // Start a new game
        private void NewGame()
        {
            // Randomize the original list
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            // Assign the random list to the original
            numbers = randomList;
            // Set the tag of each picture box to a number in the randomized list
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString();
            }
            // Reset the timer and start the countdown
            label1.Text = "Time Left: " + totalTime.ToString() + " seconds";
            gameOver = false;
            Timer.Start();
            countDownTime = totalTime;
        }

        // Check if the two selected pictures are a match
        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                // The pictures match, remove their tags
                A.Tag = null;
                B.Tag = null;
            }
            // Reset the choices
            firstChoice = null;
            secondChoice = null;
            // Hide all pictures
            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }
            // Check if all pictures have been matched
            if (pictures.All(o => o.Tag == pictures[0].Tag))
            {
                // Game is over, player wins
                GameOver("You Win!");
            }
        }

        // End the game
        private void GameOver(string msg)
        {
            // Stop the timer and disable further clicks
            Timer.Stop();
            gameOver = true;
            // Show a message box with the game over message
            MessageBox.Show(msg);
        }
    }
}