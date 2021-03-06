﻿using ConfigHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Threading;
using System.IO;

namespace UciProxy
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger("UciProxy");

        static void Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
                Logger.Info("Starting...");
                Helpers.GetRuntimeInfo(out Version clrRuntimeVersion, out string fwDescription);
                Logger.Info($"Clr: {clrRuntimeVersion} | {fwDescription}");
                var config = BaseConfig.LoadAll<Config>("DefaultConfig.json", args);
                Logger.Info($"config: {config}");
                if (!config.Help)
                {
                    var uciProxyHandlerForward = new UciProxyHandler(config, false);
                    var uciProxyHandlerReverse = new UciProxyHandler(config, true);
                    uciProxyHandlerForward.WaitReceiverExit();
                    Logger.Info("Done Forward");
                    uciProxyHandlerForward.Stop();
                    uciProxyHandlerReverse.Stop();
                }
                Logger.Info("End.");
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}
