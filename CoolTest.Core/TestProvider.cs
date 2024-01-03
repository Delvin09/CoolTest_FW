﻿using CoolTest.Abstarctions;
using CoolTest.Core.Logger;
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

        private readonly ILogger _logger;

        public TestProvider(string assemblyPath, ILogger logger)
        {
            this._assemblyPath = assemblyPath;
            _context = new AssemblyLoadContext(_assemblyPath, true);
            _assembly = _context.LoadFromAssemblyPath(_assemblyPath);
            _logger = logger;

            _context.Resolving += _context_Resolving;
        }

        public void Dispose()
        {
            try
            {
                _context.Resolving -= _context_Resolving;
                _context.Unload();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        public IEnumerable<TestGroup> GetTests()
        {
            var result = _assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<TestGroupAttribute>() != null)
                .Select(t =>
                    new TestGroup(t.GetCustomAttribute<TestGroupAttribute>()?.Name ?? t.Name, t, _logger)
                    {
                        Tests = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                .Where(m => m.GetCustomAttribute<TestAttribute>() != null)
                                .Select(m => new Test(m.GetCustomAttribute<TestAttribute>()?.Name ?? m.Name, m, _logger))
                                .ToImmutableArray()
                    }
                );

            return result;
        }

        private Assembly? _context_Resolving(AssemblyLoadContext ctx, AssemblyName name)
        {
            var dirName = Path.GetDirectoryName(_assemblyPath);
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(name.Name)) return null;

            var assemblyPath = Path.Combine(dirName, name.Name);
            assemblyPath += ".dll";
            return ctx.LoadFromAssemblyPath(assemblyPath);
        }
    }
}
