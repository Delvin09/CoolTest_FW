using CoolTest.Abstarctions.TestResults;
using System.Linq;

namespace CoolTest.Abstarctions.Results
{
    public class AssemblyTestResult : SingleTestResult
    {
        public List<GroupTestResult> GroupList = new List<GroupTestResult>();

        public GroupTestResult[] Results { get { return GroupList.ToArray<GroupTestResult>(); } }

        public AssemblyTestResult() : base() { }

        public override void End()
        {
            base.End();
            TestState = GroupList.All(test => test.Status == TestState.Success.ToString()) ? TestState.Success : TestState.Failed;
        }
    }
}
