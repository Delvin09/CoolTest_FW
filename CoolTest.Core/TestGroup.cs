﻿using CoolTest.Abstarctions;
using System.Collections.Immutable;
using System.Reflection;

namespace CoolTest.Core
{
    internal class TestGroup
    {
        public TestGroup() { }

        public string Name { get; set; }
        public Type Type { get; set; }
        public ImmutableArray<Test> Tests { get; set; }

        public void Run()
        {
            foreach (var test in Tests)
            {
                var subject = Activator.CreateInstance(Type);

                try
                {
                    test.Run(subject);
                }
                catch (AssertFailException ex)
                {
                    Console.WriteLine($"Error in test {test.Name}: {ex.Message}");
                    throw new AssertFailException($"Stop the execution of the test group {Name}: {ex.Message}");
                }
                finally
                {
                    RunClean(subject, test);
                }
            }
        }
        private void RunClean(object subject, Test test)
        {
            var cleanMethods = subject.GetType()
                .GetMethods()
                .Where(m => m.Name == "Clean" && m.GetCustomAttribute<TestAttribute>() != null);

            foreach (var cleanMethod in cleanMethods)
            {
                try
                {
                    cleanMethod.Invoke(subject, null);

                    if (cleanMethod.GetCustomAttribute<TestAttribute>() != null)
                    {
                        Console.WriteLine($"The clean method after the test {test.Name} has a test attribute. Stop the execution of the test group.");
                        throw new AssertFailException($"The clean method after the test {test.Name} has a test attribute. Stop the execution of the test group.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calling the clean method after a test {test.Name}: {ex.Message}");
                    throw new AssertFailException($"Error calling the clean method after a test {test.Name}: {ex.Message}");
                }
            }
        }
    }
}
