using System.Drawing;
using CoolTest.Core;
namespace CoolTest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new Core.TestEngine();
            List<TestResult> tests = engine.Run(args);
            foreach (TestResult result in tests)
            {
                result.DisplayResults();
            }
        }
    }
}
