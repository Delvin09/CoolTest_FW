namespace CoolTest.Abstarctions
{
    public static class Assert
    {
        public static void AreEqual<T>(T actual, T expected, string? message = null)
        {
            if (!object.Equals(actual, expected))
                throw new AssertFailException(message ?? "Actual not equal expected!");
        }

        public static void AreNotEqual<T>(T actual, T expected, string? message = null)
        {
            if (object.Equals(actual, expected))
                throw new AssertFailException(message ?? "Actual equal expected!");
        }

        public static void IsTrue(bool condition, string? message = null)
        {
            if (condition == false)
                throw new AssertFailException(message ?? "Condition is not true!");
        }

        public static void IsFalse(bool condition, string? message = null)
        {
            if (condition == true)
                throw new AssertFailException(message ?? "Condition is not false!");
        }
    }
}
