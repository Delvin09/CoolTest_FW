using CoolTest.Core.Logger;
using System.Collections.Immutable;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        private readonly ILogger _logger;

        public TestGroup(string name, Type type, ILogger logger)
        {
            Name = name;
            Type = type;
            _logger = logger;
        }

        public string Name { get; }

        public Type Type { get; }

        public ImmutableArray<Test> Tests { get; init; }

        public void Run()
        {
            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);
                if (subject == null)
                {
                    var ex = new InvalidOperationException("Can't create the object of test class!");
                    _logger.LogError(ex);
                    throw ex;
                }

                test.Run(subject);
            }
        }
    }
}
