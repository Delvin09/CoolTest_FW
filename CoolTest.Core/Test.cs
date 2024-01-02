using System.Reflection;
using CoolTest.Abstarctions;

namespace CoolTest.Core
{
    internal class Test
    {
        public Test(string name, MethodInfo method)
        {
            Name = name;
            Method = method;
        }

        public string Name { get; }

        public MethodInfo Method { get; }

        public void Run(object subject)
        {
            try
            {
                Method.Invoke(subject, null);
                RunClean(subject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling a test method {Name}: {ex.Message}");
                throw new AssertFailException($"Error calling a test method {Name}: {ex.Message}");
            }
            finally
            {
                var cleanMethod = subject.GetType().GetMethod("Clean");
                if (cleanMethod != null && cleanMethod.GetCustomAttribute<TestAttribute>() != null)
                {
                    Console.WriteLine($"The method of cleaning up after the test {Name} has a test attribute. Stop the execution of a group of tests.");
                    throw new AssertFailException($"The method of cleaning up after the test {Name} has a test attribute. Stop the execution of a group of tests.");
                }
            }
        }
        private void RunClean(object subject)
        {
            var cleanMethod = subject.GetType().GetMethod("Clean");
            if (cleanMethod != null)
            {
                try
                {
                    cleanMethod.Invoke(subject, null);

                    if (cleanMethod.GetCustomAttribute<TestAttribute>() != null)
                    {
                        Console.WriteLine($"The method of cleaning up after the test {Name} has a test attribute. Stop the execution of a group of tests.");
                        throw new AssertFailException($"The method of cleaning up after the test {Name} has a test attribute. Stop the execution of a group of tests.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calling the clean up method after a test {Name}: {ex.Message}");
                    throw new AssertFailException($"Error calling the clean up method after a test {Name}: {ex.Message}");
                }
            }
        }
    }
}
