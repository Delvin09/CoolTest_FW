namespace CoolTest.Abstarctions
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class TestGroupAttribute : Attribute
    {
        public TestGroupAttribute()
        {
        }

        public string? Name { get; set; }
    }
}
