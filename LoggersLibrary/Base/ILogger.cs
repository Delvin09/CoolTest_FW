using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggersLibrary.Base
{
    public interface ILogger : IDisposable
    {
        public void LogInfo(string message);

        public void LogWarning(string message);

        public void LogError(Exception ex);
    }
}
