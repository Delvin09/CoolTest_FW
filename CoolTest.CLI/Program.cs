using CoolTest.Abstarctions.TestResults;
namespace CoolTest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new Core.TestEngine();
            TestResult testResult = engine.Run(args);
        }
    }
}
