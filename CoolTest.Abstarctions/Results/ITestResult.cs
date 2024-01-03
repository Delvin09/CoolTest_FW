namespace CoolTest.Abstarctions.Results
{
    public interface ITestResult
    {
        string Name { get; set; }
        TimeSpan Duration { get; }
        Exception Exception { get; set; }
        TestState TestState { get; set; }

        void End();
    }
}
