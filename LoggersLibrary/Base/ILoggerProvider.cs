using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggersLibrary.Base
{
    public interface ILoggerProvider
    {
        ILogger GetLogger();
        ILoggerProvider Registration(Func<ILogger> createLogger);
    }
}
