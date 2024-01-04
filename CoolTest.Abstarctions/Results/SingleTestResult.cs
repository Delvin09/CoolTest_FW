namespace CoolTest.Abstarctions.Results
{
    public class SingleTestResult : ITestResult
    {
        public string? Name { get; set; } = string.Empty;

        public TimeSpan Duration { get { return (EndTime - StartTime); } }

        public Exception? Exception { private get; set; }

        public ExceptionInfo ExceptionInfo { get { return new ExceptionInfo(Exception); }}

        protected DateTime StartTime { get; set; }

        protected DateTime EndTime { get; set; }

        public TestState TestState { protected get; set; }

        public string Status { get { return TestState.ToString(); } }

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
