using Grpc.Core;
using log4net;
using log4net.Repository.Hierarchy;
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
        private static readonly ILog Logger = LogManager.GetLogger("NetworkReceiver");

        private readonly Server _server;

        public NetworkReceiver(int port, Action<UciRequest, string> action)
        {
            _server = new Server
            {
                Services = { Uci.BindService(new UciImpl(action)) },
                Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) }
            };
            _server.Start();
        }

        public void Stop()
        {
            Logger.Info("Stopping grpc server...");
            _server.ShutdownAsync().Wait();
            Logger.Info("Server stopped");
        }

        // A voir 
        public void WaitForExit()
        {
            Logger.Info("In NetworkReceiver.WaitForExit()");
            Thread.Sleep(10_000_000);
        }
    }
}
