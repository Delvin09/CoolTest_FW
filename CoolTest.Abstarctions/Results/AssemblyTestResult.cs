namespace CoolTest.Abstarctions.Results
{
    public class AssemblyTestResult : SingleTestResult
    {
        public List<GroupTestResult> GroupList { get; private set; } = new List<GroupTestResult>();

        public AssemblyTestResult() : base() { }

        public override void End()
        {
            base.End();
            TestState = GroupList.All(test => test.Status == TestState.Success.ToString()) ? TestState.Success : TestState.Failed;
        }
    }
}
