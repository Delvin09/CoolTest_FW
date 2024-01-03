namespace CoolTest.Abstarctions.Results
{
    public interface ITestResult
    {
        string Name { get; set; }
        TimeSpan Duration { get; }
        string ExceptionMessage { get; }
        string Status { get; }

        void End();
    }
}
