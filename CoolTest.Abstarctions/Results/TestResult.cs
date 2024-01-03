using CoolTest.Abstarctions.Results;

namespace CoolTest.Abstarctions.TestResults
{
    public class TestResult
    {
        public TimeSpan Duration { get { return (EndTime - StartTime); } }

        private DateTime StartTime { get; set; }

        private DateTime EndTime { get; set; }

        public List<GroupTestResult> GroupList = new List<GroupTestResult>();

        public TestResult()
        {
            StartTime = DateTime.Now;
        }

        public void End()
        {
            EndTime = DateTime.Now;
        }

        public static T Create<T>(string name, Func<T, T> func) 
            where T : ITestResult, new()
        {
            T instance = new T() { Name = name };
            func(instance);
            instance.End();
            return instance;
        }
    }
}
