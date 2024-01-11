using LoggersLibrary.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggersLibrary
{
    internal class ContainerLog : ILogger
    {
        List<ILogger> Loggers;
        internal ContainerLog(List<ILogger> loggers)
        {
            Loggers = loggers;
        }
        public List<ILogger> GetLoggers()
        {
            return Loggers;
        }
        public void Dispose()
        {
            Loggers.ForEach(l => l.Dispose());
        }

        public void LogError(Exception ex)
        {
            Loggers.ForEach(l => l.LogError(ex));
        }

        public void LogInfo(string message)
        {
            Loggers.ForEach(l => l.LogInfo(message));
        }

        public void LogWarning(string message)
        {
            Loggers.ForEach(l => l.LogWarning(message));
        }
    }
}
