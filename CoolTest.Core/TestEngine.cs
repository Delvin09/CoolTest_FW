using CoolTest.Abstarctions.Results;
using CoolTest.Abstarctions.TestResults;

namespace CoolTest.Core
{
    public class TestEngine
    {
        public TestResult Run(string[] assemblies)
        {
            TestResult testResult = new TestResult();
            foreach (var assembly in assemblies)
            {
                using (var provider = new TestProvider(assembly))
                {
                    AssemblyTestResult assemblyTestResult = TestResult.Create<AssemblyTestResult>(provider.ModuleName, assemblyTest =>
                        {
                    
                        var testGroups = provider.GetTests();
                        foreach (var group in testGroups)
                        {
                            GroupTestResult groupTest = group.Run(group.Name);
                            assemblyTest.GroupList.Add(groupTest);
                        }

                        return assemblyTest;
                    
                    });
                    testResult.AssemblyList.Add(assemblyTestResult);
                }
            }
            testResult.End();
            return testResult;
        }
    }
}
