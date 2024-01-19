using LoggersLibrary.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggersLibrary
{
    public class LoggerProvider : ILoggerProvider, IDisposable
    {
        private ContainerLog ContainerLog { get; }

        public LoggerProvider()
        {
            ContainerLog = new ContainerLog(new List<ILogger>());
        }
        public ILogger GetLogger()
        {
            return ContainerLog;
        }

        public ILoggerProvider Registration(Func<ILogger> createLogger)
        {
            ContainerLog.GetLoggers().Add(createLogger());
            return this;
        }

        public void Dispose()
        {
            ContainerLog.Dispose();
        }
    }
}
