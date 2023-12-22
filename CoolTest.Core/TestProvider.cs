using CoolTest.Abstarctions;
using System.Collections.Immutable;
using System.Reflection;

namespace CoolTest.Core
{
    internal class TestProvider
    {
        private readonly string _assemblyPath;

        public TestProvider(string assemblyPath)
        {
            this._assemblyPath = assemblyPath;
        }

        public IEnumerable<TestGroup> GetTests()
        {
            var assembly = Assembly.LoadFile(_assemblyPath);

            var result = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<TestGroupAttribute>() != null)
                .Select(t =>
                    new TestGroup
                    {
                        Name = t.GetCustomAttribute<TestGroupAttribute>()?.Name ?? t.Name,
                        Type = t,
                        Tests = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                .Where(m => m.GetCustomAttribute<TestAttribute>() != null)
                                .Select(m => new Test
                                {
                                    Method = m,
                                    Name = m.GetCustomAttribute<TestAttribute>()?.Name ?? m.Name
                                })
                                .ToImmutableArray()
                    }
                );

            return result;
        }
    }
}
