using System.Collections.Immutable;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup() { }

        public string Name { get; set; }
        public Type Type { get; set; }
        public ImmutableArray<Test> Tests { get; set; }

        public void Run(GroupTestResult groupTestResult)
        {
            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);

                test.Run(subject, groupTestResult);
            }
        }
    }
}
