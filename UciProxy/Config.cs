using ConfigHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class Config : BaseConfig
    {
        public Source Input { get; set; }
        public Source Output { get; set; }
        public bool LogExchange { get; set; }

        // Delay in Ms used for spinlock, default to 10 ms
        public int DelayMs { get; set; }

    }
}
