using System.Collections.Immutable;

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

        public void Run()
        {
            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);
                if (subject == null) throw new InvalidOperationException("Can't create the object of test class!");

                test.Run(subject);
            }
        }
    }
}
