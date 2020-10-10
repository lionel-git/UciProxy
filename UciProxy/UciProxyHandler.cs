using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace UciProxy
{
    public class UciProxyHandler
    {
        private ConcurrentQueue<string> _lines;

        public UciProxyHandler(Config config)
        {
            _lines = new ConcurrentQueue<string>();

            // 2 threads
            // Input => lines (IReceiver depend de Source input)
            // lines => output (ISender depend de Source output)


        }

        public void Run()
        {

        }
    }
}
