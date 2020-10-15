using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class UciImpl : Uci.UciBase
    {
        private readonly Action<UciRequest, string> _action;

        public UciImpl(Action<UciRequest, string> action)
        {
            _action = action;
        }

        public override Task<UciReply> SendUciMessage(UciRequest request, ServerCallContext context)
        {
            _action(request, "network");
            return Task.FromResult(new UciReply());
        }

        public override Task<HeartbeatReply> SendHeartbeat(HeartbeatRequest request, ServerCallContext context)
        {         
            if (request.StopRequested)
                Environment.Exit(0);
            return Task.FromResult(new HeartbeatReply() { StopAck = request.StopRequested });
        }

    }
}
