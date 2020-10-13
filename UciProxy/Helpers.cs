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
        public static bool WaitForServer(string hostname, int port, int timeOutSeconds, int delaySeconds = 2)
        {
            var deadLine = DateTime.Now.AddSeconds(timeOutSeconds);
            while (DateTime.Now < deadLine)
            {
                try
                {
                    var client = new TcpClient();
                    client.Connect(hostname, port);
                    client.Close();
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
