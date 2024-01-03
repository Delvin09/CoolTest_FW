using System.Linq;

namespace CoolTest.Abstarctions.TestResults
{
    public class GroupTestResult : SingleTestResult
    {
        public List<SingleTestResult> TestList = new List<SingleTestResult>();

        public SingleTestResult[] Results { get { return TestList.ToArray<SingleTestResult>(); } }

        public GroupTestResult() : base() { }

        public override void End()
        {
            base.End();
            TestState = TestList.All(test => test.Status == TestState.Success.ToString()) ? TestState.Success : TestState.Failed;
        }
    }
}
