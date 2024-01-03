using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoolTest.Core.Logger.Logger;

namespace CoolTest.Core.Logger
{
    public interface ILogger
    {
        public void LogInfo(string message);

        public void LogWarning(string message);

        public void LogError(string message);

        public void LogInfo(Exception ex);

        public void LogWarning(Exception ex);

        public void LogError(Exception ex);
    }
}
