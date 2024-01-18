using CoolTest.Core.Logger;
using System.Collections.Immutable;
using CoolTest.Abstarctions.Results;
using CoolTest.Abstarctions;

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

        public event EventHandler<TestEventArgs> BeforeTest;

        public event EventHandler<AfterTestEventArgs> AfterTest;


        public GroupTestResult Run(string name)
        {
            return TestResult.Create<GroupTestResult>(name, groupTest =>
            {
                foreach (var test in Tests)
                {
                    test.BeforeTest += OnBeforeTest;
                    test.AfterTest += OnAfterTest;

                    var subject = Activator.CreateInstance(Type);
                    if (subject == null)
                    {
                        var ex = new InvalidOperationException("Can't create the object of test class!");
                        _logger.LogError(ex);
                        throw ex;
                    }
                    SingleTestResult testResult = test.Run(subject, test.GetMethod());

                    test.BeforeTest -= OnBeforeTest;
                    test.AfterTest -= OnAfterTest;

                    groupTest.TestList.Add(testResult);
                }
                return groupTest;
            });
        }

        public void OnBeforeTest(object? sender, TestEventArgs e)
           => BeforeTest?.Invoke(this, e);

        public void OnAfterTest(object? sender, AfterTestEventArgs e)
            => AfterTest?.Invoke(this, e);
    }
}
