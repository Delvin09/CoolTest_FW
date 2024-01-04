using CoolTest.Abstarctions.Results;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CoolTest.Abstarctions.TestResults
{
    public class TestResult
    {
        public TimeSpan Duration { get { return (EndTime - StartTime); } }

        private DateTime StartTime { get; set; }

        private DateTime EndTime { get; set; }

        public List<AssemblyTestResult> AssemblyList { get; private set; } = new List<AssemblyTestResult>();

        public TestResult()
        {
            StartTime = DateTime.Now;
        }

        public void End()
        {
            EndTime = DateTime.Now;
        }

        public void SaveToFile(string path = "")
        {
            string fileName = $"TestResults-{Regex.Replace(DateTime.Now.ToString(), "[ :]", "-")}.json";
            string filePath = Path.Combine(path == "" ? Directory.GetCurrentDirectory() : path, fileName);
            string jsonData = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
        }

        public static T Create<T>(string? name, Func<T, T> func) 
            where T : ITestResult, new()
        {
            T instance = new T() { Name = name };
            func(instance);
            instance.End();
            return instance;
        }
    }
}
