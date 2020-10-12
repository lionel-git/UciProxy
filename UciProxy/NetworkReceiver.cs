﻿using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class NetworkReceiver : IReceiver
    {
        private Server _server;

        public NetworkReceiver(int port, Action<UciRequest> action)
        {
            _server = new Server
            {
                Services = { Uci.BindService(new UciImpl(action)) },
                Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) }
            };
            _server.Start();
        }
    }
}
