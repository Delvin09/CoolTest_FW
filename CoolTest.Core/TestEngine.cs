using CoolTest.Core.Logger;
using CoolTest.Abstarctions.Results;
using CoolTest.Abstarctions;

namespace CoolTest.Core
{
    public class TestEngine
    {
        private readonly ILogger _logger;

        public TestEngine(ILogger logger)
        {
            _logger = logger;
        }

        public TestResult Run(string[] assemblies)
        {
            _logger.LogInfo($"CoolTest engine started!");
            TestResult testResult = new TestResult(_logger);
            foreach (var assembly in assemblies)
            {
                _logger.LogInfo($"Loading assembly {assembly}");
                using (var provider = new TestProvider(assembly, _logger))
                {
                    AssemblyTestResult assemblyTestResult = TestResult.Create<AssemblyTestResult>(provider.ModuleName, assemblyTest =>
                        {
                            var testGroups = provider.GetTests();
                            foreach (var group in testGroups)
                            {
                                group.BeforeTest += OnBeforeTest;
                                group.AfterTest += OnAfterTest;
                                try
                                {
                                    _logger.LogInfo($"Run tests for test group {group}");
                                    GroupTestResult groupTest = group.Run(group.Name);

                                    assemblyTest.GroupList.Add(groupTest);
                                    _logger.LogInfo($"Finished tests for test group {group}");
                                }
                                finally
                                {
                                    group.BeforeTest -= OnBeforeTest;
                                    group.AfterTest -= OnAfterTest;
                                }
                            }

                            return assemblyTest;
                        });
                    testResult.AssemblyList.Add(assemblyTestResult);
                }
            }
            testResult.End();
            return testResult;
        }

        public event EventHandler<TestEventArgs>? BeforeTest;
        public event EventHandler<AfterTestEventArgs>? AfterTest;

        public void OnBeforeTest(object? sender, TestEventArgs e)
        {
            _logger.LogInfo($"Before test event invoke: {e}");
            BeforeTest?.Invoke(this, e);
        }


        public void OnAfterTest(object? sender, AfterTestEventArgs e)
        {
            _logger.LogInfo($"After test event invoke: {e}");
            AfterTest?.Invoke(this, e);
        }
    }
}
