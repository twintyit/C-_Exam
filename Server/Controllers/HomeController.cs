using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    internal class HomeController
    {
        private ServerLogics logics;
        private ShowMessageDelegate showMessage;
        private ShowMessageDelegate addClient;
        private ShowMessageDelegate removeClient;

        public HomeController(ShowMessageDelegate showMessageDelegate, ShowMessageDelegate addClient, ShowMessageDelegate removeClient) 
        {
            showMessage = showMessageDelegate;
            this.addClient = addClient;
            this.removeClient = removeClient;
            logics = new ServerLogics();
            logics.message += DisplayMessage;
            logics.connectedClient += AddClient;
            logics.disconnectedClient += RemoveClient;
        }
        private void DisplayMessage(object sender, string message)
        {
            showMessage(message);
        }

        private void AddClient(object sender, string clientId)
        {
            addClient(clientId);
        }

        private void RemoveClient(object sender, string clientId)
        {
            removeClient(clientId);
        }

        public void RemoveClient(string clientId)
        {
            logics.DisconnectClient(clientId);
        }

        public async void StartServer(IPAddress ip ,int port)
        {
            logics.SetTcpListener(ip, port);
            await logics.StartAsync();
        }

        public void Stop()
        {
            logics.Stop();
        }
    }
}
