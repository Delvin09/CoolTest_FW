using CoolTest.Abstarctions;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.Loader;

namespace CoolTest.Core
{
    internal class TestProvider : IDisposable
    {
        private readonly string _assemblyPath;
        private readonly AssemblyLoadContext _context;

        private readonly Assembly _assembly;

        public TestProvider(string assemblyPath)
        {
            this._assemblyPath = assemblyPath;
            _context = new AssemblyLoadContext(_assemblyPath, true);
            _assembly = _context.LoadFromAssemblyPath(_assemblyPath);

            _context.Resolving += _context_Resolving;
        }

        public void Dispose()
        {
            try
            {
                _context.Resolving -= _context_Resolving;
                _context.Unload();
            }
            catch { }
        }

        public IEnumerable<TestGroup> GetTests()
        {
            var result = _assembly.GetTypes()
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
            foreach ( var group in result ) 
            {
                group.BeforeRun += Group_AfterRun;
                group.AfterRun += Group_AfterRun;
            }
            return result;
        }

        private void Group_BeforeRun(object sender, EventArgs e)
        {
            Console.WriteLine($"Testing started in group: '{((TestGroup)sender).Name}'");
        }

        private void Group_AfterRun(object sender, EventArgs e)
        {
            Console.WriteLine($"Testing finished in group: '{((TestGroup)sender).Name}'");
        }

        private Assembly? _context_Resolving(AssemblyLoadContext ctx, AssemblyName name)
        {
            var dirName = Path.GetDirectoryName(_assemblyPath);
            var assemblyPath = Path.Combine(dirName, name.Name);
            assemblyPath = assemblyPath += ".dll";
            return ctx.LoadFromAssemblyPath(assemblyPath);
        }
    }
}
