using CoolTest.Core.Logger;
using System.Collections.Immutable;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup(string name, Type type, ILogger logger)
        {
            Name = name;
            Type = type;
            _logger = logger;
        }

        public string Name { get; }

        public Type Type { get; }

        private readonly ILogger _logger;

        public ImmutableArray<Test> Tests { get; init; }

        public void Run()
        {
            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);
                if (subject == null)
                {
                    _logger.LogWarning(new InvalidOperationException("Can't create the object of test class!"));
                    continue;
                }

                test.Run(subject);
            }
        }
    }
}
