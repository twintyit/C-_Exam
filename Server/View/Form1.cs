using Server.Controllers;
using System.Net;

namespace Server
{
    public delegate void ShowMessageDelegate(string message);

    public partial class Form1 : Form
    {
        private HomeController homeController;
        public Form1()
        {
            InitializeComponent();
            homeController = new HomeController(ShowMessage, AddClientToComboBox, RemoveClientToComboBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int port)
                && IPAddress.TryParse(textBox2.Text, out IPAddress ip))
            {
                homeController.StartServer(ip, port);
            }
        }

        private void ShowMessage(string message)
        {
            label2.Text = message;
        }

        private void AddClientToComboBox(string clientId)
        {
            
                comboBox1.Items.Add(clientId);
                comboBox1.SelectedIndex = 0;
        }

        private void RemoveClientToComboBox(string clientId)
        {
            if(comboBox1.SelectedItem!= null && comboBox1.SelectedItem.ToString() != "")
            {
                comboBox1.Items.Remove(clientId);
                comboBox1.SelectedItem = "";
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            homeController.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() != "")
            {
                homeController.RemoveClient(comboBox1.SelectedItem.ToString());
            }
        }
    }
}
