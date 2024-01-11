using System.Text;
using LoggersLibrary.Base;

namespace LoggersLibrary.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private MessageLevel _messageLoggingLevel;
        public ConsoleLogger(MessageLevel messageLoggingLevel = MessageLevel.INFO)
        {
            var logPath = Environment.CurrentDirectory;

            _messageLoggingLevel = messageLoggingLevel;
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        private void logMessageLevel(string msg, MessageLevel level)
        {
            if (_messageLoggingLevel <= level)
            {
                string log = string.Join(" | ", DateTime.Now.ToString(LoggersConstants.LogLineDateTimeFormat), $"[{level}]", msg) + Environment.NewLine;
                Console.Write(log);
            }
        }

        private void logExceptionLevel(Exception ex, MessageLevel level)
        {
            if (_messageLoggingLevel <= level)
            {
                logMessageLevel(ex.Message, level);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void LogInfo(string message)
        {
            logMessageLevel(message, MessageLevel.INFO);
        }

        public void LogWarning(string message)
        {
            logMessageLevel(message, MessageLevel.WARNING);
        }

        public void LogError(Exception ex)
        {
            logExceptionLevel(ex, MessageLevel.ERROR);
        }
    }
}
