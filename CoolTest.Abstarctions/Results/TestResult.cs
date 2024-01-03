using CoolTest.Abstarctions.Results;
using System.Text.Json;
using System.IO;

namespace CoolTest.Abstarctions.TestResults
{
    public class TestResult
    {
        public TimeSpan Duration { get { return (EndTime - StartTime); } }

        private DateTime StartTime { get; set; }

        private DateTime EndTime { get; set; }

        public List<AssemblyTestResult> AssemblyList = new List<AssemblyTestResult>();

        public AssemblyTestResult[] Results { get { return AssemblyList.ToArray<AssemblyTestResult>(); }}

        public TestResult()
        {
            StartTime = DateTime.Now;
        }

        public void End()
        {
            EndTime = DateTime.Now;
        }

        private string ReadString(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            try
            {
                if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Field can not be empty");
                    return ReadString(message);
                }
                return input;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ReadString(message);
            }

        }

        public void SaveToFile()
        {
            string answer = ReadString("Do you want save result of tests to file?(y/n)");

            if (answer == "y")
            {
                string fileName = "TestResults.json";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                string jsonData = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Data has been saved to 'TestResults.json'");
            }
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
