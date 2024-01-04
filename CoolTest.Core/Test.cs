using CoolTest.Core.Logger;
using CoolTest.Abstarctions.Results;
using CoolTest.Abstarctions;
using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        private readonly ILogger _logger;
        public Test(string name, MethodInfo method, ILogger logger)
        {
            Name = name;
            Method = method;
            _logger = logger;
        }

        public string Name { get; }

        public MethodInfo Method { get; }

        public SingleTestResult Run(object subject)
        {
            return TestResult.Create<SingleTestResult>(Method.Name, testResult =>
            {
                try
                {
                    _logger.LogInfo($"Run test {Method.Name}");
                    testResult.TestState = TestState.Pending;
                    Method.Invoke(subject, null);
                    testResult.TestState = TestState.Success;
                    _logger.LogInfo($"Finish test {Method.Name}");
                    return testResult;
                }
                catch (TargetInvocationException ex)
                {
                    _logger.LogError(ex);
                    if (ex.InnerException is AssertFailException)
                    {
                        testResult.TestState = TestState.Failed;
                        testResult.Exception = ex.InnerException;
                    }
                    else
                    {
                        testResult.TestState = TestState.Error;
                        testResult.Exception = ex;
                    }
                    return testResult;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                    testResult.TestState = TestState.Error;
                    testResult.Exception = ex;
                    return testResult;
                }
            });
        }
    }
}
