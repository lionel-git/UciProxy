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
        private Action<UciRequest> _action;

        public UciImpl(Action<UciRequest> action)
        {
            _action = action;
        }

        public override Task<UciReply> SendUciMessage(UciRequest request, ServerCallContext context)
        {
            _action(request);
            return Task.FromResult(new UciReply());
        }
    }
}
