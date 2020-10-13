using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public class ConsoleSender : ISender
    {
        public ConsoleSender()
        {
        }

        public void Send(UciRequest uciRequest)
        {
            switch (uciRequest.DataType)
            {
                case DataType.Undefined:
                case DataType.Stdout:
                    Console.WriteLine(uciRequest.Data);
                    break;
                case DataType.Stderr:
                    Console.Error.WriteLine(uciRequest.Data);
                    break;
            }
        }
    }
}
