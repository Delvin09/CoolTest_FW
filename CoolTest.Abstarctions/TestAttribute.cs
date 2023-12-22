namespace CoolTest.Abstarctions
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class TestAttribute : Attribute
    {
        public TestAttribute()
        {
        }

        public string? Name { get; set; }
    }
}
