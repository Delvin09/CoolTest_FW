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
                singleTestResult.TestState = Abstarctions.TestState.Pending;
                Method.Invoke(subject, null);
                singleTestResult.TestState = Abstarctions.TestState.Success;
            }
            catch (Exception ex)
            {
                singleTestResult.TestState = Abstarctions.TestState.Failed;
                singleTestResult.ExceptionMessage = ex.InnerException.Message;
            }

            singleTestResult.End();
        }
    }
}
