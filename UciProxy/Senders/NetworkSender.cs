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
    class NetworkSender : ISender
    {
        private static readonly ILog Logger = LogManager.GetLogger("NetworkSender");

        private readonly Uci.UciClient _client;
        private readonly Channel _channel;
        private readonly ThreadWrapper _heartbeatThread;

        public NetworkSender(string host, int port)
        {
            Helpers.WaitForServer(host, port, 60);
            _channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _client = new Uci.UciClient(_channel);
            _heartbeatThread = new ThreadWrapper(() => CheckHeartbeat(), "Checkheartbeat");
        }

        public void Send(UciRequest uciRequest)
        {
            _client.SendUciMessage(uciRequest);
        }

        public void CheckHeartbeat()
        {
            try
            {
                do
                {                   
                    _client.SendHeartbeat(new HeartbeatRequest());
                    Thread.Sleep(1000);
                }
                while (true);
            }
            catch(Exception e)
            {
                Logger.Warn($"Heartbeat failed: {e.Message}");
                Environment.Exit(0);
            }
        }
    }
}
