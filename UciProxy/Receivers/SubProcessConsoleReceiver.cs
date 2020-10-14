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
        private readonly Process _process;
        private readonly ThreadWrapper _readOutputThread;
        private readonly ThreadWrapper _readErrorThread;

        public SubProcessConsoleReceiver(string executablePath, Action<UciRequest, string> action)
        {
            _process = ProcessManager.GetProcess(executablePath);
            _readOutputThread = new ThreadWrapper(() => ConsoleReceiver.ReadStream(_process.StandardOutput, DataType.Stdout, action, "SUB_PROCESS_STDOUT"), "SubProcessStdout");
            _readErrorThread = new ThreadWrapper(() => ConsoleReceiver.ReadStream(_process.StandardError, DataType.Stderr, action, "SUB_PROCESS_STDERR"), "SubProcessStderr");
        }

        public void Stop()
        {
            _readErrorThread.Abort();

        }

        public void WaitForExit()
        {
            _readErrorThread.Join();
            _readOutputThread.Join();
        }
    }
}
