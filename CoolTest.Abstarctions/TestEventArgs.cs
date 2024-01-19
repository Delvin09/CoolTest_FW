namespace CoolTest.Abstarctions
{
    public class TestEventArgs : EventArgs
    {
        public required string AssemblyName { get; init; }
        public required string GroupName { get; init; }
        public required string TestName { get; init; }

        public override string ToString() => $"[ {nameof(AssemblyName)}:{AssemblyName}, {nameof(GroupName)}:{GroupName}, {nameof(TestName)}:{TestName}]";
    }

    public class AfterTestEventArgs : TestEventArgs
    {
        public TestState Result { get; init; }

        public override string ToString() => $"{base.ToString()} with result: {Result}";
    }
}
