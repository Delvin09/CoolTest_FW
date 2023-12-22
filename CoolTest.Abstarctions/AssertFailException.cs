namespace CoolTest.Abstarctions
{
    [Serializable]
    public class AssertFailException : Exception
    {
        public AssertFailException(string message) : base(message) { }
    }
}
