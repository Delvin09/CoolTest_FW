namespace CoolTest.Abstarctions.TestResults
{
    public class GroupTestResult : SingleTestResult
    {
        public List<SingleTestResult> TestList = new List<SingleTestResult>();

        public GroupTestResult(string name) : base(name) { }

        public SingleTestResult RunSingleTest(string name)
        {
            SingleTestResult test = new SingleTestResult(name);
            TestList.Add(test);
            return test;
        }

        public override void End()
        {
            base.End();
            TestState = TestList.All(test => test.TestState == TestState.Success) ? TestState.Success : TestState.Failed;
        }
    }
}
