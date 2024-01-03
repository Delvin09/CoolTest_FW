using CoolTest.Abstarctions.TestResults;
using CoolTest.Abstarctions;
using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        public string Name { get; set; }

        public MethodInfo Method { get; set; }

        public SingleTestResult Run(object subject)
        {
            return TestResult.Create<SingleTestResult>(Method.Name, testResult =>
            {
                try
                {
                    testResult.TestState = TestState.Pending;
                    Method.Invoke(subject, null);
                    testResult.TestState = TestState.Success;
                    return testResult;
                }
                catch (TargetInvocationException ex)
                {
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
                    testResult.TestState = TestState.Error;
                    testResult.Exception = ex;
                    return testResult;
                }
            });
        }
    }
}
