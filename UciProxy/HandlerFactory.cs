using System;
using System.Collections.Concurrent;

namespace UciProxy
{
    public class HandlerFactory
    {
        public static IReceiver GetReceiver(Source source, Action<UciRequest> action)
        {
            switch (source.SourceType)
            {
                case SourceType.EXECUTABLE:
                   return new SubProcessConsoleReader(source.Address, action);
                case SourceType.NETWORK:
                    return new NetworkReceiver(source.Port, action);
                case SourceType.CONSOLE:
                    throw new NotImplementedException("Reading from console is not implemented");
                default:
                    throw new Exception($"Unhandled case: {source.SourceType}");
            }
        }

        public static ISender GetSender(Source source)
        {
            switch (source.SourceType)
            {
                case SourceType.CONSOLE:
                    return new ConsoleWriter();
                case SourceType.NETWORK:
                    return new NetworkSender(source.Address, source.Port);
                case SourceType.EXECUTABLE:
                    throw new NotImplementedException("writing to executable  is not implemented");
                default:
                    throw new Exception($"Unhandled case: {source.SourceType}");
            }
        }
    }
}
