
namespace LoggersLibrary.Base
{
    public static class LoggersConstants
    {
        public const string LogLineDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string FileDateTimeFormat = "yyyy-MM-dd HHmmss";
    }

    public enum MessageLevel
    {
        ERROR = 1,
        WARNING,
        INFO,
    }
}