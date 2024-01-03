﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTest.Core.Logger
{
    public class Logger : ILogger
    {
        public enum MessageLevel 
        {
            ERROR = 1,
            WARNING,
            INFO,
        }

        private const string dateFormat = "yyyy-MM-dd HH:mm:ss";
        private void logMessageLevel (string msg, MessageLevel level)
        {
            string log = String.Join(" | ", DateTime.Now.ToString(dateFormat), $"[{level}]", msg);
            Console.WriteLine(log);
        }

        private void logExceptionLevel(Exception ex, MessageLevel level)
        {
            logMessageLevel(ex.Message, level);
            Console.WriteLine(ex.StackTrace);
        }

        public void LogInfo(string message)
        {
            logMessageLevel(message, MessageLevel.INFO);
        }

        public void LogWarning(string message)
        {
            logMessageLevel(message, MessageLevel.WARNING);
        }

        public void LogError(string message)
        {
            logMessageLevel(message, MessageLevel.ERROR);
        }

        public void LogInfo(Exception ex)
        {
            logExceptionLevel(ex, MessageLevel.INFO);
        }

        public void LogWarning(Exception ex)
        {
            logExceptionLevel(ex, MessageLevel.WARNING);
        }

        public void LogError(Exception ex)
        {
            logExceptionLevel(ex, MessageLevel.ERROR);
        }
    }
}
