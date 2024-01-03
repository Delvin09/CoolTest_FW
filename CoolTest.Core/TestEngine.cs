using CoolTest.Core.Logger;

namespace CoolTest.Core
{
    public class TestEngine
    {
        private ILogger _logger { get; set; }

        public TestEngine() {
            _logger = new Logger.Logger();
        }
        
        public void Run(string[] assemblies)
        {
            _logger.LogInfo($"CoolTest engine started!");
            foreach (var assembly in assemblies)
            {
                _logger.LogInfo($"Loading assembly {assembly}");
                using (var provider = new TestProvider(assembly, _logger))
                {
                    var testGroups = provider.GetTests();
                    foreach (var group in testGroups)
                    {
                        _logger.LogInfo($"Run tests for test group {group}");
                        group.Run();
                    }
                }
            }
        }
    }
}
