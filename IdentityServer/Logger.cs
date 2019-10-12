using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Logger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var format = $"[{0:HH:mm:ss} {1}] {2}";

            while (exception != null)
            {
                var log = string.Format(format, DateTime.Now, logLevel, exception.Message);
                File.AppendAllText(@"identityserver4_log.txt", log);
                exception = exception.InnerException;
            }
        }
    }
}
