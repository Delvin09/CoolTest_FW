using System.Collections.Immutable;
using CoolTest.Abstarctions.TestResults;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup() { }

        public string Name { get; set; }
        public Type Type { get; set; }
        public ImmutableArray<Test> Tests { get; set; }

        public void Run(TestResult testResult, string name)
        {
            GroupTestResult groupTestResult = testResult.RunGroupTest(name);
            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);

                test.Run(subject, groupTestResult);
            }
            groupTestResult.End();
        }
    }
}
