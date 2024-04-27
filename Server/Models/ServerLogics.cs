using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LibraryTrackingProgram;

namespace Server.Models
{
    internal class ServerLogics
    {
        private TcpListener? listener;
        private bool isRunning;
        public event EventHandler<string>? message;
        public event EventHandler<string>? connectedClient;
        public event EventHandler<string>? disconnectedClient;

        private Dictionary<string, TcpClient> connectedClients = new Dictionary<string, TcpClient>();

        public void SetTcpListener(IPAddress ip, int port) 
        {
            listener = new TcpListener(ip, port);
        }

        public async Task StartAsync()
        {
            if (listener != null)
            {
                isRunning = true;
                listener.Start();
                message?.Invoke(this, "Waiting for client to connect...");

                try
                {
                    while (isRunning)
                    {
                        TcpClient client = await listener.AcceptTcpClientAsync();
                        string clientId = ((IPEndPoint)client.Client.RemoteEndPoint).ToString();
                        connectedClients.Add(clientId, client);
                        message?.Invoke(this, $"Client {clientId} connected!");
                        connectedClient?.Invoke(this,clientId);

                        await Task.Run(() => HandleClient(client));
                    }
                }
                catch (Exception ex)
                {
                    message?.Invoke(this, $"Error in server: {ex.Message}");
                }
            }
        }

        public void DisconnectClient(string clientId)
        {
            if (connectedClients.ContainsKey(clientId))
            {
                TcpClient client = connectedClients[clientId];
                connectedClients.Remove(clientId);
                client.Close();
                message?.Invoke(this,$"Client {clientId} disabled");
                disconnectedClient?.Invoke(this,  clientId);
            }
            else
            {
                Console.WriteLine($"Клиент с идентификатором {clientId} не найден.");
            }
        }

        public void Stop()
        {
            if (listener != null)
            {
                isRunning = false;
                listener.Stop();
                message?.Invoke(this, $"Server stopped");
            }
        }

        private async void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    while (isRunning)
                    {
                        byte[] commandBuffer = new byte[4];
                        await stream.ReadAsync(commandBuffer, 0, commandBuffer.Length);
                        string command = Encoding.UTF8.GetString(commandBuffer);

                        if (command == "IMAG")
                        {
                            await Task.Run(async () =>
                            {
                                byte[] messageTypeBytes = BitConverter.GetBytes((int)MessageType.ScreenTransmission);
                                await stream.WriteAsync(messageTypeBytes, 0, messageTypeBytes.Length);

                                Bitmap screenImage = CaptureScreen();
                                await SendScreenAsync(screenImage, stream);
                            });
                        }
                        else if (command == "WIND")
                        {
                            await Task.Run(async () =>
                            {
                                byte[] messageTypeBytes = BitConverter.GetBytes((int)MessageType.WindowList);
                                await stream.WriteAsync(messageTypeBytes, 0, messageTypeBytes.Length);

                                string windowList = GetOpenWindows();
                                byte[] windowData = Encoding.UTF8.GetBytes(windowList);

                                await SendDataAsync(stream, MessageType.WindowList, windowData);
                            });
                        }
                        else if(command == "DISC")
                        {
                            string clientId = ((IPEndPoint)client.Client.RemoteEndPoint).ToString();
                            DisconnectClient(clientId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка обробки підключення: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        private Bitmap CaptureScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen!.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }

        private string GetOpenWindows()
        {
            StringBuilder windowList = new StringBuilder();

            EnumWindows((hwnd, lParam) =>
            {
                if (IsWindowVisible(hwnd))
                {
                    const int maxLength = 1000;
                    StringBuilder title = new StringBuilder(maxLength);
                    GetWindowText(hwnd, title, maxLength);

                    if (title.Length > 0)
                    {
                        windowList.AppendLine(title.ToString());
                    }
                }
                return true;
            }, nint.Zero);

            return windowList.ToString();
        }

        private async Task SendScreenAsync(Bitmap image, NetworkStream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                byte[] buffer = ms.ToArray();

                await SendDataAsync(stream, MessageType.ScreenTransmission, buffer);
            }
        }

        private async Task SendDataAsync(NetworkStream stream, MessageType type, byte[] data)
        { 
            byte[] sizeBytes = BitConverter.GetBytes(data.Length);
            await stream.WriteAsync(sizeBytes, 0, sizeBytes.Length);

            await stream.WriteAsync(data, 0, data.Length);
        }

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, nint lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(nint hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);

        private delegate bool EnumWindowsProc(nint hWnd, nint lParam);
    }
}
