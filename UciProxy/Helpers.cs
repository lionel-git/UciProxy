using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UciProxy
{
    public static class Helpers
    {
        private static readonly ILog Logger = LogManager.GetLogger("Helpers");

        public static bool WaitForServer(string hostname, int port, int timeOutSeconds, int delaySeconds = 2)
        {
            var deadLine = DateTime.Now.AddSeconds(timeOutSeconds);
            while (DateTime.Now < deadLine)
            {
                try
                {
                    Logger.Info($"Checking connection to: {hostname}:{port}");
                    var client = new TcpClient();
                    client.Connect(hostname, port);
                    client.Close();
                    Logger.Info($"Connection ok");
                    return true;
                }
                catch (Exception)
                {
                    Thread.Sleep(delaySeconds * 1000);
                }
            }
            return false;
        }
    }
}
