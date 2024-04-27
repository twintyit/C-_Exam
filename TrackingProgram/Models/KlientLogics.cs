using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Klient.Models
{
    internal class KlientLogics
    {
        private string serverIp;
        private int serverPort;
        private PictureBox pictureBox;

        public KlientLogics(string ip, int port, PictureBox pictureBox)
        {
            serverIp = ip;
            serverPort = port;
            this.pictureBox = pictureBox;
        }

        public void StartReceiving()
        {
            try
            {
                using (TcpClient client = new TcpClient(serverIp, serverPort))
                using (NetworkStream stream = client.GetStream())
                {
                    while (true)
                    {
                        // Отримання зображення екрану від сервера
                        Bitmap receivedImage = ReceiveImage(stream);

                        // Відображення отриманого зображення на PictureBox
                        DisplayImage(receivedImage);

                        // Отримання і відображення інформації про вікна від сервера
                        string windowList = ReceiveWindowList(stream);
                        DisplayWindowList(windowList);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення до сервера: {ex.Message}");
            }
        }

        private Bitmap ReceiveImage(NetworkStream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                do
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, bytesRead);
                } while (stream.DataAvailable);

                return new Bitmap(memoryStream);
            }
        }

        private string ReceiveWindowList(NetworkStream stream)
        {
            byte[] buffer = new byte[4096];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        private void DisplayImage(Bitmap image)
        {
            pictureBox.Image = image;
        }

        private void DisplayWindowList(string windowList)
        {
            // Відображення інформації про вікна від сервера (наприклад, в консоль)
            Console.WriteLine("Список активних вікон:");
            Console.WriteLine(windowList);
        }
    }
}
