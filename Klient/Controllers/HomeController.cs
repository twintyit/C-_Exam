using Klient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Klient.Controllers
{
    internal class HomeController
    {
        private KlientLogics logics;
        private ShowListDelegate showWindowList;
        private ShowScreenDelegate showScreen;
        private ShowStatusDelegate showStatus;

        public HomeController(ShowListDelegate showWindowList, ShowScreenDelegate showScreen, ShowStatusDelegate showStatus)
        {
            this.showWindowList = showWindowList;
            this.showScreen = showScreen;
            this.showStatus = showStatus;
            logics = new KlientLogics();
            logics.DisplayWindowList += ShowWindowList;
            logics.DisplayScreen += ShowScreen;
            logics.DisplayStatus += ShowStatus;
        }

        private void ShowWindowList(object sender, string[] data)
        {
            showWindowList(data);
        }

        private void ShowScreen(object sender, Bitmap image)
        {
            showScreen(image);
        }

        private void ShowStatus(object sender, string data)
        {
            showStatus(data);
        }

        public async void SendCommandWindowList()
        {
           
            await logics.SendCommandAsync("WIND");
        }

        public async void SendCommandDisplayScreen()
        {
            await logics.SendCommandAsync("IMAG");
        }

        public async void ConnectToServer(string ip, int port)
        {
            logics.ServerIp = ip;
            logics.ServerPort = port;
            await logics.ConnectToServerAsync();
        }

        public async Task DisconnectAsync()
        {
            await logics.DisconnectFromServerAsync();
        }
    }
}
