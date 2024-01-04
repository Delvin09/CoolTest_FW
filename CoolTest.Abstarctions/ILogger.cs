using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTest.Core.Logger
{
    public interface ILogger
    {
        public void LogInfo(string message);

        public void LogWarning(string message);

        public void LogError(Exception ex);
    }
}
