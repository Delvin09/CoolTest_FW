using CoolTest.Core.Logger;
using CoolTest.Abstarctions.Results;
using CoolTest.Abstarctions;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace CoolTest.Core
{
    internal class Test
    {
        private readonly ILogger _logger;
        private readonly AssemblyName _assemblyName;
        private readonly string _groupName;
        private readonly TestEngine _testEngine;

        public Test(string name, MethodInfo method, ILogger logger, AssemblyName assemblyName, string groupName)
        {
            Name = name;
            Method = method;
            _logger = logger;
            _assemblyName = assemblyName;
            _groupName = groupName;

        }

        public string Name { get; }

        public MethodInfo Method { get; }

        public SingleTestResult Run(object subject)
        {
            return TestResult.Create<SingleTestResult>(Method.Name, testResult =>
            {
                var startTest = new TestEngine.TestEventArgs { AssemblyName = _assemblyName.ToString(), GroupName = _groupName, TestName = Name };

                _testEngine.OnBeforeTest(startTest);

                try
                {
                    _logger.LogInfo($"Run test {Method.Name}");
                    testResult.TestState = TestState.Pending;
                    Method.Invoke(subject, null);
                    testResult.TestState = TestState.Success;
                    _logger.LogInfo($"Finish test {Method.Name}");
                    var afterTest = new TestEngine.AfterTestEventArgs { AssemblyName = _assemblyName.ToString(), GroupName = _groupName, TestName = Name, Result = TestState.Success };
                    _testEngine.OnAfterTest(afterTest);
                    return testResult;
                }
                catch (TargetInvocationException ex)
                {
                    _logger.LogError(ex);
                    if (ex.InnerException is AssertFailException)
                    {
                        testResult.TestState = TestState.Failed;
                        testResult.Exception = ex.InnerException;
                        var afterTest = new TestEngine.AfterTestEventArgs { AssemblyName = _assemblyName.ToString(), GroupName = _groupName, TestName = Name, Result = TestState.Failed };
                        _testEngine.OnAfterTest(afterTest);
                    }
                    else
                    {
                        testResult.TestState = TestState.Error;
                        testResult.Exception = ex;
                        var afterTest = new TestEngine.AfterTestEventArgs { AssemblyName = _assemblyName.ToString(), GroupName = _groupName, TestName = Name, Result = TestState.Error };
                        _testEngine.OnAfterTest(afterTest);
                    }
                    return testResult;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                    testResult.TestState = TestState.Error;
                    testResult.Exception = ex;
                    var afterTest = new TestEngine.AfterTestEventArgs { AssemblyName = _assemblyName.ToString(), GroupName = _groupName, TestName = Name, Result = TestState.Error };
                    _testEngine.OnAfterTest(afterTest);
                    return testResult;
                }
            });
        }
    }
}
