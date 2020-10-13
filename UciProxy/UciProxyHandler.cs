using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace UciProxy
{
    public class UciProxyHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger("UciProxyHandler");

        private ConcurrentQueue<UciRequest> _lines;

        private IReceiver _receiver;
        private ISender _sender;

        private Task _queueSenderTask;

        private bool _stop;

        private bool _logExchange;

        public UciProxyHandler(Config config, bool revertDirection)
        {
            _logExchange = config.LogExchange;
            _lines = new ConcurrentQueue<UciRequest>();
            _stop = false;
            _queueSenderTask = Task.Run(() => SendLines());
            var input = revertDirection ? config.Output : config.Input;
            var output = revertDirection ? config.Input : config.Output;
            _receiver = HandlerFactory.GetReceiver(input, Receive);
            _sender = HandlerFactory.GetSender(output);
        }

        void Receive(UciRequest r)
        {
            _lines.Enqueue(r);
            if (_logExchange)
                Logger.Info($"=> {r}");
        }

        public void Stop()
        {
            _stop = true;
            _queueSenderTask.Wait();
        }

        private void SendLines()
        {
            try
            {
                do
                {
                    if (_lines.TryDequeue(out UciRequest uciRequest))
                        _sender.Send(uciRequest);
                    else
                        Thread.Sleep(10);
                }
                while (!_stop);
            }
            catch (Exception e)
            {
                Logger.Error($"{e}");
            }
        }
    }
}
