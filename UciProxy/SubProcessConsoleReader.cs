using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class SubProcessConsoleReader : IReceiver
    {
        private static readonly ILog Logger = LogManager.GetLogger("SubProcessConsoleReader");

        private Process _process;
        private Task _readOutputTask;
        private Task _readErrorTask;
        private Action<UciRequest> _action;

        public SubProcessConsoleReader(string executablePath, Action<UciRequest> action)
        {
            _action = action;
            // Start exe 
            _process = Process.Start(new ProcessStartInfo()
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                WorkingDirectory = "",
                Arguments = "",
                FileName = executablePath,
                UseShellExecute = false
            });
            _readOutputTask = Task.Run(() => ReadOutput());
            _readErrorTask = Task.Run(() => ReadError());
        }

        private void ReadOutput()
        {
            try
            {
                do
                {
                    var line = _process.StandardOutput.ReadLine();
                    var uciRequest = new UciRequest()
                    {
                        DataType = DataType.Stdout,
                        Data = line
                    };
                    _action(uciRequest);
                }
                while (!_process.HasExited);
            }
            catch (Exception e)
            {
                Logger.Error($"{e}");
            }
        }

        private void ReadError()
        {
            try
            {
                do
                {
                    var line = _process.StandardError.ReadLine();
                    var uciRequest = new UciRequest()
                    {
                        DataType = DataType.Stderr,
                        Data = line
                    };
                    _action(uciRequest);
                }
                while (!_process.HasExited);
            }
            catch (Exception e)
            {
                Logger.Error($"{e}");
            }
        }
    }
}
