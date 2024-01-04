namespace CoolTest.Abstarctions.Results
{
    public class ExceptionInfo
    {
        private Exception? exception;

        public ExceptionInfo(Exception? exception)
        {
            if (exception != null)
            {
                this.exception = exception;
            }
        }

        public string? Message { get { return exception?.Message; } }

        public string? Type { get { return exception?.GetType().ToString(); } }

        public string? StackTrace { get { return exception?.StackTrace; } }

    }
}