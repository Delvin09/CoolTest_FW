using CoolTest.Abstarctions.Results;

namespace CoolTest.Abstarctions.TestResults
{
    public class SingleTestResult : ITestResult
    {
        public string Name { get; set; }

        public TimeSpan Duration { get { return (EndTime - StartTime); } }

        public Exception Exception { get; set; }

        protected DateTime StartTime { get; set; }

        protected DateTime EndTime { get; set; }

        public TestState TestState { get; set; }

        public SingleTestResult()
        {
            StartTime = DateTime.Now;
        }
        public virtual void End()
        {
            EndTime = DateTime.Now;
        }
    }
}
