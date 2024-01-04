namespace CoolTest.Abstarctions.Results
{
    public interface ITestResult
    {
        string? Name { get; set; }

        TimeSpan Duration { get; }

        string Status { get; }

        ExceptionInfo ExceptionInfo { get; }

        void End();
    }
}
