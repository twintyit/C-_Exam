using LibraryTrackingProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Klient.Models
{
    internal class KlientLogics
    {
        public string ServerIp { get; set; }
        public int ServerPort{ get; set; }
        private TcpClient? client;
        private NetworkStream? stream;

        public event EventHandler<string[]>? DisplayWindowList;
        public event EventHandler<string>? DisplayStatus;
        public event EventHandler<Bitmap>? DisplayScreen;

        public KlientLogics(string ip = "127.0.0.1", int port = 700)
        {
            ServerIp = ip;
            ServerPort = port;
        }
        public async Task ConnectToServerAsync()
        {
            try
            {
                client = new TcpClient(ServerIp, ServerPort);
                stream = client.GetStream();
                DisplayStatus?.Invoke(this, "Сonnected to server");
                await ReceiveDataAsync();
            }
            catch (Exception ex)
            {
                DisplayStatus?.Invoke(this, $"Error connecting to the server");
            }
            finally
            {
                await DisconnectFromServerAsync();
            }
        }

        public async Task DisconnectFromServerAsync()
        {
            try
            {
                if (client != null)
                {
                    await SendCommandAsync("DISC");
                    client.Close();
                    client = null;
                    DisplayStatus?.Invoke(this, "Disconnected from the server");
                    DisplayWindowList?.Invoke(this,new string[1]);
                    DisplayScreen?.Invoke(this, new Bitmap(@"NoSig.jpg"));
                }
                DisplayStatus?.Invoke(this, "Disconnected from the server");
            }
            catch (Exception ex)
            {
                DisplayStatus?.Invoke(this, $"Error connecting to the server");
            }
            
        }

        public async Task ReceiveDataAsync()
        {
            while (client != null && client.Connected)
            {
                byte[] messageTypeBytes = new byte[4];
                await stream!.ReadAsync(messageTypeBytes, 0, messageTypeBytes.Length);
                MessageType type = (MessageType)BitConverter.ToInt32(messageTypeBytes, 0);

                switch (type)
                {
                    case MessageType.ScreenTransmission:
                      
                        await ReceiveScreenTransmissionAsync();
                        break;

                    case MessageType.WindowList:
                        await ReceiveWindowListAsync();
                        break;
                }
            }
        }

        public async Task ReceiveScreenTransmissionAsync()
        {
            try
            {
                await SendCommandAsync("IMAG");
                Bitmap receivedImage = await ReceiveImageAsync(stream);
                DisplayScreen?.Invoke(this, receivedImage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка отримання трансляції екрану: {ex.Message}");
            }
        }

        public async Task ReceiveWindowListAsync()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    string[] windowList = await ReceiveWindowListDataAsync(stream!);
                    DisplayWindowList?.Invoke(this, windowList);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка отримання списку активних вікон: {ex.Message}");
            }
        }
       

        private async Task<Bitmap> ReceiveImageAsync(NetworkStream stream)
        {
            byte[] imageData = await ReadDataAsync(stream);
            Bitmap receivedImage = ConvertBytesToBitmap(imageData);
            return receivedImage;
        }

        private async Task<string[]> ReceiveWindowListDataAsync(NetworkStream stream)
        {
            byte[] windowData = await ReadDataAsync(stream);
            string windowList = Encoding.UTF8.GetString(windowData);
            string[] windowArray = windowList.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return windowArray;
        }

        private async Task<byte[]> ReadDataAsync(NetworkStream stream, int timeoutMilliseconds = 10000)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {

                byte[] sizeBytes = new byte[4];
                await stream.ReadAsync(sizeBytes, 0, sizeBytes.Length);
                int bufferSize = BitConverter.ToInt32(sizeBytes, 0);

                byte[] buffer = new byte[bufferSize];
                int totalBytesRead = 0;
                bool dataReceived = false;
                DateTime startTime = DateTime.Now;

                while (!dataReceived && totalBytesRead < buffer.Length)
                {
                    if (stream.DataAvailable)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length - totalBytesRead);
                        if (bytesRead > 0)
                        {
                            totalBytesRead += bytesRead;
                            memoryStream.Write(buffer, 0, bytesRead);

                            if (totalBytesRead >= buffer.Length)
                            {
                                dataReceived = true;
                            }
                        }
                    }
                    else
                    {
                        TimeSpan elapsedTime = DateTime.Now - startTime;
                        if (elapsedTime.TotalMilliseconds >= timeoutMilliseconds)
                        {
                            break;
                        }

                        await Task.Delay(100);
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public async Task SendCommandAsync(string command)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    byte[] commandData = Encoding.UTF8.GetBytes(command);
                    await stream!.WriteAsync(commandData, 0, commandData.Length);

                }
                else
                {
                    DisplayStatus?.Invoke(this, "Connect to the server first");
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Помилка відправлення команди: {ex.Message}");
            }
        }
        private Bitmap ConvertBytesToBitmap(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                return new Bitmap(ms);
            }
        }
    }
}
