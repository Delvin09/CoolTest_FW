using System.Collections.Immutable;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup() { }

        public string Name { get; set; }
        public Type Type { get; set; }
        public ImmutableArray<Test> Tests { get; set; }
        public EventHandler BeforeRun;
        public EventHandler AfterRun;

        public void Run()
        {
            OnBeforeRun(EventArgs.Empty);

            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);

                test.Run(subject);
            }

            OnAfterRun(EventArgs.Empty);
        }

        protected virtual void OnBeforeRun(EventArgs e) 
        {
            BeforeRun?.Invoke(this, e);
        }

        protected virtual void OnAfterRun(EventArgs e) 
        {
            AfterRun?.Invoke(this, e);
        }
    }
}
