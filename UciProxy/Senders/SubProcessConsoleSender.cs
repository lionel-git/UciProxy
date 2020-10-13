using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class SubProcessConsoleSender : ISender
    {
        private Process _process;

        public SubProcessConsoleSender(string executablePath)
        {
            _process = ProcessManager.GetProcess(executablePath);
        }

        public void Send(UciRequest uciRequest)
        {
            _process.StandardInput.WriteLine(uciRequest.Data);
        }
    }
}
