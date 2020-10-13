using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UciProxy
{
    public class NetworkReceiver : IReceiver
    {
        private Server _server;

        public NetworkReceiver(int port, Action<UciRequest, string> action)
        {
            _server = new Server
            {
                Services = { Uci.BindService(new UciImpl(action)) },
                Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) }
            };
            _server.Start();
        }

        // A voir 
        public void WaitForExit()
        {
            Thread.Sleep(10_000_000);
        }
    }
}
