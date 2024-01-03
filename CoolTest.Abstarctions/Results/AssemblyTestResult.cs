using CoolTest.Abstarctions.TestResults;

namespace CoolTest.Abstarctions.Results
{
    public class AssemblyTestResult : SingleTestResult
    {
        public List<GroupTestResult> GroupList = new List<GroupTestResult>();

        public AssemblyTestResult() : base() { }

        public override void End()
        {
            base.End();
            TestState = GroupList.All(test => test.TestState == TestState.Success) ? TestState.Success : TestState.Failed;
        }
    }
}
