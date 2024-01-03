namespace CoolTest.Abstarctions.TestResults
{
    public class GroupTestResult : SingleTestResult
    {
        public List<SingleTestResult> TestList = new List<SingleTestResult>();

        public GroupTestResult() : base() { }

        public override void End()
        {
            base.End();
            TestState = TestList.All(test => test.TestState == TestState.Success) ? TestState.Success : TestState.Failed;
        }
    }
}
