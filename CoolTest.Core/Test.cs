using CoolTest.Abstarctions.TestResults;
using CoolTest.Abstarctions;
using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        public string Name { get; set; }

        public MethodInfo Method { get; set; }

        public void Run(object subject, GroupTestResult groupTestResult)
        {
            SingleTestResult singleTestResult = groupTestResult.RunSingleTest(Method.Name);
            try
            {
                singleTestResult.TestState = TestState.Pending;
                Method.Invoke(subject, null);
                singleTestResult.TestState = TestState.Success;
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is AssertFailException)
                {
                    singleTestResult.TestState = TestState.Failed;
                    singleTestResult.Exception = ex.InnerException;
                } else
                {
                    singleTestResult.TestState = TestState.Error;
                    singleTestResult.Exception = ex;
                }
            }
            catch (Exception ex)
            {
                singleTestResult.TestState = TestState.Error;
                singleTestResult.Exception = ex;
            }

            singleTestResult.End();
        }
    }
}
