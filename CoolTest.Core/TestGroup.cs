using System.Collections.Immutable;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup() { }

        public string Name { get; set; }
        public Type Type { get; set; }
        public ImmutableArray<Test> Tests { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsRunSuccessful { get; private set; }
        public string StartedGroupName { get; private set; }
        public ImmutableArray<string> AllTestNames { get; private set; }
        public ImmutableArray<string> FailedTestNames { get; private set; }
        public EventHandler BeforeRun;
        public EventHandler AfterRun;

        public void Run()
        {
            StartTime = DateTime.Now;
            StartedGroupName = Name;
            AllTestNames = Tests.Select(test => test.Name).ToImmutableArray();
            OnBeforeRun(EventArgs.Empty);

            List<string> failedTests = new List<string>();

            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);
                try
                {
                    test.Run(subject);
                }
                catch (Exception)
                {
                    failedTests.Add(test.Name);
                }
            }

            EndTime = DateTime.Now;
            OnAfterRun(EventArgs.Empty);

            FailedTestNames = failedTests.ToImmutableArray();
            IsRunSuccessful = FailedTestNames.IsEmpty;
        }

        protected void OnBeforeRun(EventArgs e)
        {
            BeforeRun?.Invoke(this, e);
        }

        protected void OnAfterRun(EventArgs e)
        {
            AfterRun?.Invoke(this, e);
        }
    }
}
