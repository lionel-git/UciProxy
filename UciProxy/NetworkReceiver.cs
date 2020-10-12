using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class NetworkReceiver : IReceiver
    {
        public NetworkReceiver(int port, Action<UciRequest> action)
        {
            // listen on port

        }
    }
}
