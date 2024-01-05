using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTest.Core.Logger
{
    public class Logger : ILogger
    {
        private const string logLineDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string fileDateTimeFormat = "yyyy-MM-dd HHmmss";
        public enum MessageLevel
        {
            ERROR = 1,
            WARNING,
            INFO,
        }

        private FileStream _stream;
        private MessageLevel _messageLoggingLevel;
        public Logger(MessageLevel messageLoggingLevel = MessageLevel.INFO)
        {
            var logPath = Environment.CurrentDirectory;

            _messageLoggingLevel = messageLoggingLevel;

            _stream = File.OpenWrite(Path.Combine(logPath, $"log_{DateTime.Now.ToString(fileDateTimeFormat)}.txt"));
        }

        public void Dispose() { _stream.Dispose(); }

        private void logMessageLevel (string msg, MessageLevel level)
        {
            if (_messageLoggingLevel <= level)
            {
                string log = String.Join(" | ", DateTime.Now.ToString(logLineDateTimeFormat), $"[{level}]", msg) + Environment.NewLine;
                Console.Write(log);
                if(_stream != null && _stream.CanWrite)
                {
                    _stream.Write(Encoding.UTF8.GetBytes(log));
                }
            }
        }

        private void logExceptionLevel(Exception ex, MessageLevel level)
        {
            if (_messageLoggingLevel <= level)
            {
                logMessageLevel(ex.Message, level);
                Console.WriteLine(ex.StackTrace);
                if (_stream != null && _stream.CanWrite && ex.StackTrace != null)
                {
                    _stream.Write(Encoding.UTF8.GetBytes(ex.StackTrace));
                }
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
