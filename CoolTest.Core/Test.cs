using CoolTest.Core.Logger;
using CoolTest.Abstarctions.Results;
using CoolTest.Abstarctions;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using static CoolTest.Core.TestEngine;
using System.ComponentModel;

namespace CoolTest.Core
{
    internal class Test
    {
        private readonly ILogger _logger;
        private readonly string _assemblyName;
        private readonly string _groupName;

        public Test(string name, MethodInfo method, ILogger logger, string assemblyName, string groupName)
        {
            Name = name;
            Method = method;
            _logger = logger;
            _assemblyName = assemblyName;
            _groupName = groupName;
        }

        public string Name { get; }

        public MethodInfo Method { get; }

        public event EventHandler<TestEventArgs> BeforeTest;

        public event EventHandler<AfterTestEventArgs> AfterTest;

        public MethodInfo GetMethod()
        {
            return Method;
        }

        public SingleTestResult Run(object subject, MethodInfo method)
        {
            return TestResult.Create<SingleTestResult>(Method.Name, testResult =>
            {
                TestEventArgs? startTest = new TestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name };
                
                OnBeforeTest(startTest);

                try
                {
                    _logger.LogInfo($"Run test {Method.Name}");
                    testResult.TestState = TestState.Pending;
                    Method.Invoke(subject, null);
                    testResult.TestState = TestState.Success;
                    _logger.LogInfo($"Finish test {Method.Name}");
                    var afterTest = new AfterTestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name, Result = TestState.Success };
                    OnAfterTest(afterTest);
                    return testResult;
                }
                catch (TargetInvocationException ex)
                {
                    _logger.LogError(ex);
                    if (ex.InnerException is AssertFailException)
                    {
                        testResult.TestState = TestState.Failed;
                        testResult.Exception = ex.InnerException;
                        var afterTest = new AfterTestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name, Result = TestState.Failed };
                        OnAfterTest(afterTest);
                    }
                    else
                    {
                        testResult.TestState = TestState.Error;
                        testResult.Exception = ex;
                        var afterTest = new AfterTestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name, Result = TestState.Error };
                        OnAfterTest(afterTest);
                    }
                    return testResult;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                    testResult.TestState = TestState.Error;
                    testResult.Exception = ex;
                    var afterTest = new AfterTestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name, Result = TestState.Error };
                    OnAfterTest(afterTest);
                    return testResult;
                }
            });
        }

        public void OnBeforeTest(TestEventArgs e)
           => BeforeTest?.Invoke(this, e);

        public void OnAfterTest(AfterTestEventArgs e)
            => AfterTest?.Invoke(this, e);
    }
}
