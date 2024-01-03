using CoolTest.Core.Logger;
using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        private ILogger _logger { get; set; }
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
                Method.Invoke(subject, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}
