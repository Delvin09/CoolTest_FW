namespace CoolTest.Abstarctions.TestResults
{
    public class SingleTestResult
    {
        public string Name { get; set; }

        public double Duration { get { return (EndTime - StartTime).TotalSeconds; } }

        public Exception Exception { get; set; }

        protected DateTime StartTime { get; set; }

        protected DateTime EndTime { get; set; }

        public TestState TestState { get; set; }

        public SingleTestResult(string name)
        {
            StartTime = DateTime.Now;
            Name = name;
        }
        public virtual void End()
        {
            EndTime = DateTime.Now;
        }
    }
}
