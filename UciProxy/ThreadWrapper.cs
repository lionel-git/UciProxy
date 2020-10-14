using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UciProxy
{
    public class ThreadWrapper
    {
        private static readonly ILog Logger = LogManager.GetLogger("ThreadWrapper");

        private readonly Action _action;
        private Thread _thread;
        private readonly string _name;

        public string ThreadName => $"{_name}({_thread.ManagedThreadId})";

        public ThreadWrapper(Action action, string name, bool start = true)
        {
            _action = action;
            _name = name;
            if (start)
                Start();
        }
        
        private void Run()
        {
            try
            {
                _action();
                Logger.Info($"Exit thread: {ThreadName}");
            }
            catch (ThreadAbortException)
            {
                Logger.Info($"thread '{ThreadName}' was aborted.");
            }
            catch (Exception e)
            {
                Logger.Error($"Exception in thread {ThreadName}:\n{e}");
            }
        }

        public void Start()
        {
            _thread = new Thread(Run);
            Logger.Info($"Starting {ThreadName}");
            _thread.Start();
        }

        public void Join()
        {
            _thread.Join();
        }

        public void Abort()
        {
            _thread.Abort();
        }
    }
}
