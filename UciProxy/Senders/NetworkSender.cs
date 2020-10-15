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

        public NetworkSender(string host, int port, int connectionTimeOut, int heartbeatTimeOut)
        {
            if (connectionTimeOut <= 0)
                connectionTimeOut = 60;
            Helpers.WaitForServer(host, port, connectionTimeOut);
            _channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);
            _client = new Uci.UciClient(_channel);
            _heartbeatThread = new ThreadWrapper(() => CheckHeartbeat(heartbeatTimeOut), "Checkheartbeat");
        }

        public void Send(UciRequest uciRequest)
        {
            _client.SendUciMessage(uciRequest);
        }

        public void CheckHeartbeat(int heartbeatTimeOut)
        {
            try
            {
                if (heartbeatTimeOut <= 0)
                    heartbeatTimeOut = 5;
                do
                {
                    var deadline = DateTime.UtcNow.AddSeconds(heartbeatTimeOut);
                    Logger.Debug("Check hearbeat...");
                    _client.SendHeartbeat(new HeartbeatRequest(), null, deadline);
                    Logger.Debug("Hearbeat ok.");
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
