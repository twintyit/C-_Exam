using Klient.Controllers;
using System.Net;
using System.Windows.Forms;

namespace Klient
{
    public delegate void ShowListDelegate(string[] list);
    public delegate void ShowScreenDelegate(Bitmap image);
    public delegate void ShowStatusDelegate(string message);

    public partial class Form1 : Form
    {
        private HomeController homeController;
        public Form1()
        {
            InitializeComponent();
            homeController = new HomeController(ShowWindowList, ShowScreen, ShowStatus);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            homeController.SendCommandWindowList();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int port)
               && !string.IsNullOrEmpty(textBox2.Text))
            {
                homeController.ConnectToServer(textBox2.Text, port);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            homeController.SendCommandDisplayScreen();
        }

        private void ShowWindowList(string[] data)
        {
            listBox1.DataSource = data;
        }

        private void ShowScreen(Bitmap image)
        {
            pictureBox1.Image = ResizeImage(image, pictureBox1.Width, pictureBox1.Height);
        }

        private void ShowStatus(string message)
        {
            label2.Text = message;
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await homeController.DisconnectAsync();
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        { 
            await homeController.DisconnectAsync();
        }
    }
}
