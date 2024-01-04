namespace CoolTest.Abstarctions.Results
{
    public class GroupTestResult : SingleTestResult
    {
        public List<SingleTestResult> TestList { get; private set; } = new List<SingleTestResult>();

        public GroupTestResult() : base() { }

        public override void End()
        {
            base.End();
            TestState = TestList.All(test => test.Status == TestState.Success.ToString()) ? TestState.Success : TestState.Failed;
        }
    }
}
