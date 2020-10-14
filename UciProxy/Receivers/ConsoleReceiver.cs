using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UciProxy
{
    public class ConsoleReceiver: IReceiver
    {
        private static readonly ILog Logger = LogManager.GetLogger("ConsoleReceiver");

        private ThreadWrapper _readInputThread;
       
        public ConsoleReceiver(Action<UciRequest, string> action)
        {
            var stream = new StreamReader(Console.OpenStandardInput());
            _readInputThread = new ThreadWrapper(() => ReadStream(stream, DataType.Stdout, action, "CONSOLE_RECEIVER"), "Console Receiver");
        }

        public static void ReadStream(TextReader reader, DataType dataType, Action<UciRequest, string> action, string name)
        {
            try
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        var uciRequest = new UciRequest()
                        {
                            DataType = dataType,
                            Data = line
                        };
                        action(uciRequest, name);
                    }
                }
                while (line != null);
                Logger.Warn($"Stream '{name}' closed.");
            }
            catch (ThreadAbortException)
            {
                Logger.Info($"Stream '{name}' was aborted.");
            }
            catch (Exception e)
            {
                Logger.Error($"{e}");
            }
        }

        public void Stop()
        {
            _readInputThread.Abort();
        }

        public void WaitForExit()
        {
            _readInputThread.Join();
        }
    }
}
