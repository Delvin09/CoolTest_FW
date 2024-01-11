using LoggersLibrary.Loggers;
using CoolTest.Abstarctions.Results;
using LoggersLibrary;

namespace CoolTest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loggerProvider = new LoggerProvider();
            loggerProvider.Registration(() => new ConsoleLogger()).Registration(() => new FileLogger());
            using var engine = new Core.TestEngine(loggerProvider);
            TestResult testResult = engine.Run(args);
            testResult.SaveToFile();
        }
    }
}
