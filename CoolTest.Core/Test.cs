using CoolTest.Core.Logger;
using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        private readonly ILogger _logger;
        public Test(string name, MethodInfo method, ILogger logger)
        {
            Name = name;
            Method = method;
            _logger = logger;
        }

        public string Name { get; }

        public MethodInfo Method { get; }

        public void Run(object subject)
        {
            try
            {
                _logger.LogInfo($"Run test {Method.Name}");
                Method.Invoke(subject, null);
                _logger.LogInfo($"Finish test {Method.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}
