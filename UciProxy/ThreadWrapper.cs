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

        private Action _action;
        private Thread _thread;
        private string _name;

        public ThreadWrapper(Action action, string name)
        {
            _action = action;
            _name = name;
        }

        private void Run()
        {
            try
            {
                _action();
            }
            catch (Exception e)
            {
                Logger.Error($"Exception in thread {_name}:\n{e}");
            }
        }

        public void Start()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void Join()
        {
            _thread.Join();
        }
    }
}
