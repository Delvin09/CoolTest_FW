using CoolTest.Core.Logger;
using CoolTest.Abstarctions.Results;

namespace CoolTest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new Core.TestEngine(new Logger());
            TestResult testResult = engine.Run(args);
            testResult.SaveToFile();
        }
    }
}
