namespace MemoryGame
{
    public partial class Form1 : Form
    {
        private List<Image> cardImages;
        private PictureBox firstClicked;
        private PictureBox secondClicked;
        private int matchesFound;
        int totalTime = 30;
        int countdownTime;
        bool gameOver = false;
        Random rand;

        public Form1()
        {
            InitializeComponent();
            LoadPictures();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;

            if (clickedPictureBox.Tag.ToString() == "flipped")
            {
                // The picture box is already flipped, do nothing
                return;
            }

            // Set the Tag property of the picture box to "flipped" so we know it has been flipped
            clickedPictureBox.Tag = "flipped";

            // Set the image of the picture box to the hidden image stored in the Tag property
            Image hiddenImage = (Image)clickedPictureBox.Tag;
            clickedPictureBox.Image = hiddenImage;

            // Do any other actions that you want to happen when a picture box is flipped
        }
        private void TimerEvent(object sender, EventArgs e)
        {

        }

        private void NewGame(object sender, EventArgs e)
        {

        }

        private void LoadPictures()
        {
            Image[] images = new Image[] { Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6, Properties.Resources._7, Properties.Resources._8 };
            List<Image> pairs = new List<Image>();
            foreach (Image image in images)
            {
                pairs.Add(image);
                pairs.Add(image);
            }
            // Shuffle the image pairs
            Random rand = new Random();
            int n = pairs.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Image temp = pairs[k];
                pairs[k] = pairs[n];
                pairs[n] = temp;
            }
            for (int i = 1; i < 16; i++)
            {
                PictureBox picBox = this.Controls.Find("pictureBox" + i.ToString(), true).FirstOrDefault() as PictureBox;
                picBox.Tag = hiddenImage;
            }
        
        // Define the size of the hidden image
        int imageSize = 100;

            // Create a new bitmap to hold the hidden image
            Bitmap hiddenImage = new Bitmap(imageSize, imageSize);

            // Create a graphics object to draw on the bitmap
            Graphics g = Graphics.FromImage(hiddenImage);

            // Fill the bitmap with a gray background
            g.FillRectangle(Brushes.Gray, 0, 0, imageSize, imageSize);

            // Draw a black square in the center of the bitmap
            int squareSize = 50;
            int squareX = (imageSize - squareSize) / 2;
            int squareY = (imageSize - squareSize) / 2;

            // Release the graphics object
            g.Dispose();

            // Populate the picture boxes with the hidden image
            pictureBox1.Image = hiddenImage;
            pictureBox2.Image = hiddenImage;
            pictureBox3.Image = hiddenImage;
            pictureBox4.Image = hiddenImage;
            pictureBox5.Image = hiddenImage;
            pictureBox6.Image = hiddenImage;
            pictureBox7.Image = hiddenImage;
            pictureBox8.Image = hiddenImage;
            pictureBox9.Image = hiddenImage;
            pictureBox10.Image = hiddenImage;
            pictureBox11.Image = hiddenImage;
            pictureBox12.Image = hiddenImage;
            pictureBox13.Image = hiddenImage;
            pictureBox14.Image = hiddenImage;
            pictureBox15.Image = hiddenImage;
            pictureBox16.Image = hiddenImage;
            for (int i = 1; i < 16; i++)
            {
                // Create a new PictureBox control
                PictureBox pictureBox = new PictureBox();

                // Set the Tag property to the current index
                pictureBox.Tag = i;

            }
        }
        private void Restart()
        {
            firstClicked = null;
            secondClicked = null;
            matchesFound = 0;
            gameOver = false;

            // Reset countdown timer
            countdownTime = totalTime;
            label1.Text = countdownTime.ToString();

            // Load pictures and shuffle cards
            LoadPictures();
        }
        private void CheckPictures(PictureBox A, PictureBox B)
        {

        }
        private void GameOver()
        {

        }
    }
}