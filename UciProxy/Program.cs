using ConfigHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace UciProxy
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger("UciProxy");

        static void Main(string[] args)
        {
            try
            {
                Logger.Info("Starting...");
                var config = BaseConfig.LoadAll<Config>("DefaultConfig.json", args);
                var uciProxyHandler = new UciProxyHandler(config);
                uciProxyHandler.Run();
                Logger.Info("End.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
