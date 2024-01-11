using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggersLibrary.Base;

namespace LoggersLibrary.Loggers
{
    public class FileLogger : ILogger
    {
        private FileStream _stream;
        private MessageLevel _messageLoggingLevel;
        public FileLogger(MessageLevel messageLoggingLevel = MessageLevel.INFO)
        {
            var logPath = Environment.CurrentDirectory;

            _messageLoggingLevel = messageLoggingLevel;

            _stream = File.OpenWrite(Path.Combine(logPath, $"log_{DateTime.Now.ToString(LoggersConstants.FileDateTimeFormat)}.txt"));
        }

        public void Dispose() { _stream.Dispose(); }

        private void logMessageLevel(string msg, MessageLevel level)
        {
            if (_messageLoggingLevel <= level)
            {
                string log = string.Join(" | ", DateTime.Now.ToString(LoggersConstants.LogLineDateTimeFormat), $"[{level}]", msg) + Environment.NewLine;
                if (_stream != null && _stream.CanWrite)
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
