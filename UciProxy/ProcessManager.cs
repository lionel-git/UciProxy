using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public static class ProcessManager
    {
        private static ConcurrentDictionary<string, Process> _processes = new ConcurrentDictionary<string, Process>();

        public static Process GetProcess(string path)
        {
            if (_processes.TryGetValue(path, out Process process))
                return process;
            else
            {
                var newProcess = Process.Start(new ProcessStartInfo()
                {
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = "",
                    Arguments = "",
                    FileName = path,
                    UseShellExecute = false
                });
                if (!_processes.TryAdd(path, newProcess))
                    throw new Exception($"Unable to add process with path {path}");
                return newProcess;
            }
        }
    }
}
