using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UciProxy
{
    public class UciProxyHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger("UciProxyHandler");

        private readonly ConcurrentQueue<UciRequest> _lines;

        private readonly IReceiver _receiver;
        private readonly ISender _sender;

        private readonly ThreadWrapper _queueSenderThread;

        private bool _stop;

        private readonly bool _logExchange;

        private int _delayMs;

        public UciProxyHandler(Config config, bool revertDirection)
        {
            _logExchange = config.LogExchange;
            _lines = new ConcurrentQueue<UciRequest>();
            _stop = false;
            _queueSenderThread = new ThreadWrapper(() => SendLines(), "LineSender");
            var input = revertDirection ? config.Output : config.Input;
            var output = revertDirection ? config.Input : config.Output;
            _receiver = HandlerFactory.GetReceiver(input, Receive);
            _sender = HandlerFactory.GetSender(output);
            _delayMs = config.DelayMs > 0 ? config.DelayMs : 10;
        }

        void Receive(UciRequest r, string name)
        {
            _lines.Enqueue(r);
            if (_logExchange)
                Logger.Info($"{name} {r}");
        }

        public void Stop()
        {
            _stop = true;
            _queueSenderThread.Join();
            _receiver.Stop();
        }

        public void WaitReceiverExit()
        {
             _receiver.WaitForExit();
        }

        private void SendLines()
        {
            try
            {
                do
                {
                    if (_sender != null && _lines.TryDequeue(out UciRequest uciRequest))
                        _sender.Send(uciRequest);
                    else
                        Thread.Sleep(_delayMs);
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
