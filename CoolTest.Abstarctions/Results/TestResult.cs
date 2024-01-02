namespace CoolTest.Abstarctions.TestResults
{
    public class TestResult
    {
        public double Duration { get; set; }

        private List<GroupTestResult> GroupList = new List<GroupTestResult>();
        public GroupTestResult RunGroupTest(string name)
        {
            GroupTestResult group = new GroupTestResult(name);
            GroupList.Add(group);
            return group;
        }

        public void End()
        {
            Duration = GroupList.Sum(group => group.Duration);
        }
        public void DisplayResults()
        {
            foreach (var group in GroupList)
            {
                Console.Write($"{group.Name}: ");

                if (group.TestState == TestState.Success)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition(25, Console.CursorTop);
                Console.Write($"{group.TestState}");
                Console.ResetColor();

                Console.SetCursorPosition(45, Console.CursorTop);
                Console.Write($"{group.Duration}s");

                Console.WriteLine(" ");
                foreach (var item in group.TestList)
                {
                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.Write($"{item.Name}: ");

                    if (item.TestState == TestState.Success)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.SetCursorPosition(25, Console.CursorTop);
                    Console.Write($"{item.TestState}");
                    Console.ResetColor();

                    Console.SetCursorPosition(45, Console.CursorTop);
                    Console.Write($"{item.Duration}s");

                    if (item.Exception != null && !string.IsNullOrEmpty(item.Exception.Message))
                    {
                        Console.SetCursorPosition(65, Console.CursorTop);
                        Console.Write($"{item.Exception.Message}");
                    }
                    Console.WriteLine(" ");
                }

                Console.WriteLine(" ");
            }
        }
    }
}
