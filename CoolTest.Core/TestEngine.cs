using CoolTest.Core.Logger;
using CoolTest.Abstarctions.TestResults;
namespace CoolTest.Core
{
    public class TestEngine
    {
        private readonly ILogger _logger;

        public TestEngine(ILogger logger) {
            _logger = logger;
        }

        public List<TestResult> Run(string[] assemblies)
        {
            _logger.LogInfo($"CoolTest engine started!");
            List<TestResult> tests = new List<TestResult>();
            foreach (var assembly in assemblies)
            {
                _logger.LogInfo($"Loading assembly {assembly}");
                using (var provider = new TestProvider(assembly, _logger))
                {
                    TestResult testResult = new TestResult();
                    var testGroups = provider.GetTests();

                    foreach (var group in testGroups)
                    {
                        _logger.LogInfo($"Run tests for test group {group}");
                        GroupTestResult groupTest = group.Run(group.Name);
                        testResult.GroupList.Add(groupTest);
                        _logger.LogInfo($"Finished tests for test group {group}");
                    }
                    testResult.End();
                    tests.Add(testResult);
                }
            }
            return tests;
        }
    }
}
