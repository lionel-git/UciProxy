using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public interface IReceiver
    {
        void WaitForExit();
    }
}
