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
        private Uci.UciClient _client;

        public NetworkSender(string address, int port)
        {
            var channel = new Channel($"{address}:{port}", ChannelCredentials.Insecure);
            _client = new Uci.UciClient(channel);
        }

        public void Send(UciRequest uciRequest)
        {
            var reply = _client.SendUciMessage(uciRequest);
        }
    }
}
