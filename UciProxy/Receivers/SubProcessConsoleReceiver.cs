using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class SubProcessConsoleReceiver : IReceiver
    {
        private static readonly ILog Logger = LogManager.GetLogger("SubProcessConsoleReader2");

        private Process _process;
        private Task _readOutputTask;
        private Task _readErrorTask;

        public SubProcessConsoleReceiver(string executablePath, Action<UciRequest> action)
        {
            _process = ProcessManager.GetProcess(executablePath);
            _readOutputTask = Task.Run(() => ConsoleReceiver.ReadStream(_process.StandardOutput, DataType.Stdout, action, "SUB_PROCESS_STDOUT"));
            _readErrorTask = Task.Run(() => ConsoleReceiver.ReadStream(_process.StandardError, DataType.Stderr, action, "SUB_PROCESS_STDERR"));
        }
    }
}
