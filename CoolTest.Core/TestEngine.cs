namespace CoolTest.Core
{
    public class TestEngine
    {
        public void Run(string[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                using (var provider = new TestProvider(assembly))
                {
                    var testGroups = provider.GetTests();
                    foreach (var group in testGroups)
                    {
                        group.Run();
                    }
                }
            }
        }
    }
}
