﻿using LoggersLibrary.Base;
using CoolTest.Abstarctions.Results;

namespace CoolTest.Core
{
    public class TestEngine: IDisposable
    {
        private readonly ILogger _logger;

        public TestEngine(ILoggerProvider loggerProvider) {
            _logger = loggerProvider.GetLogger();
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
                            _logger.LogInfo($"Run tests for test group {group}");
                            GroupTestResult groupTest = group.Run(group.Name);
                            assemblyTest.GroupList.Add(groupTest);
                            _logger.LogInfo($"Finished tests for test group {group}");
                        }

                        return assemblyTest;

                    });
                    testResult.AssemblyList.Add(assemblyTestResult);
                }
            }
            testResult.End();
            return testResult;
        }

        public void Dispose()
        {
            _logger.Dispose();
        }
    }
}
