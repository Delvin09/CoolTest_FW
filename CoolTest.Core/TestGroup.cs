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

        public GroupTestResult Run(string name)
        {
            return TestResult.Create<GroupTestResult>(name, groupTest =>
            {
                foreach (var test in Tests)
                {
                    var subject = Activator.CreateInstance(Type);

                    SingleTestResult testResult = test.Run(subject);

                    groupTest.TestList.Add(testResult);
                }
                return groupTest;
            });
        }
    }
}
