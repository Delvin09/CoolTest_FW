using CoolTest.Abstarctions.TestResults;
namespace CoolTest.Core
{
    public class TestEngine
    {
        public List<TestResult> Run(string[] assemblies)
        {
            List<TestResult> tests = new List<TestResult>();
            foreach (var assembly in assemblies)
            {
                using (var provider = new TestProvider(assembly))
                {
                    TestResult testResult = new TestResult();
                    var testGroups = provider.GetTests();
                    foreach (var group in testGroups)
                    {
                        group.Run(testResult, group.Name);
                    }
                    testResult.End();
                    tests.Add(testResult);
                }
            }
            return tests;
        }
    }
}
