using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UciProxy
{
    class NetworkSender : ISender
    {
        private readonly Uci.UciClient _client;

        public NetworkSender(string address, int port)
        {
            Helpers.WaitForServer(address, port, 60);
            var channel = new Channel($"{address}:{port}", ChannelCredentials.Insecure);
            _client = new Uci.UciClient(channel);
        }

        public void Send(UciRequest uciRequest)
        {
            _client.SendUciMessage(uciRequest);
        }
    }
}
