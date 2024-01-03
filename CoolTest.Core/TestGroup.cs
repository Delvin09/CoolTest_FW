using System.Collections.Immutable;
using CoolTest.Abstarctions.TestResults;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public Type Type { get; }

        public ImmutableArray<Test> Tests { get; init; }

        public GroupTestResult Run(string name)
        {
            return TestResult.Create<GroupTestResult>(name, groupTest =>
            {
                foreach (var test in Tests)
                {
                    var subject = Activator.CreateInstance(Type);
                    if (subject == null) throw new InvalidOperationException("Can't create the object of test class!");
                    SingleTestResult testResult = test.Run(subject);

                    groupTest.TestList.Add(testResult);
                }
                return groupTest;
            });
        }
    }
}
