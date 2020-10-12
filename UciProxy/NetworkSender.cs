using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    class NetworkSender : ISender
    {
        public NetworkSender(string address, int port)
        {
            // connect to host:port
        }

        public void Send(UciRequest uciRequest)
        {
            throw new NotImplementedException();
        }
    }
}
