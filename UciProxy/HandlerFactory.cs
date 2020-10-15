using System;
using System.Collections.Concurrent;

namespace UciProxy
{
    public class HandlerFactory
    {
        public static IReceiver GetReceiver(Source source, Action<UciRequest, string> action)
        {
            switch (source.SourceType)
            {
                case SourceType.EXECUTABLE:
                   return new SubProcessConsoleReceiver(source.ExecutablePath, action);
                case SourceType.NETWORK:
                    return new NetworkReceiver(source.LocalPort, action);
                case SourceType.CONSOLE:
                    return new ConsoleReceiver(action);
                default:
                    throw new Exception($"Unhandled case: {source.SourceType}");
            }
        }

        public static ISender GetSender(Source source)
        {
            switch (source.SourceType)
            {
                case SourceType.CONSOLE:
                    return new ConsoleSender();
                case SourceType.NETWORK:
                    return new NetworkSender(source.RemoteHost, source.RemotePort, source.ConnectionTimeOut, source.HeartbeatTimeOut);
                case SourceType.EXECUTABLE:
                    return new SubProcessConsoleSender(source.ExecutablePath);
                default:
                    throw new Exception($"Unhandled case: {source.SourceType}");
            }
        }
    }
}
