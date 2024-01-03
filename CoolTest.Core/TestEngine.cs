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
                        GroupTestResult groupTest = group.Run(group.Name);
                        testResult.GroupList.Add(groupTest);
                    }
                    testResult.End();
                    tests.Add(testResult);
                }
            }
            return tests;
        }
    }
}
