using CoolTest.Abstarctions;

namespace CoolTest.Core
{
    public class TestResult
    {
        public double Duration { get; set; }

        private List<GroupTestResult> GroupList = new List<GroupTestResult>();
        public GroupTestResult RunGroupTest(string name) {
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
            Console.WriteLine($"Total duration: {Duration}");
            Console.WriteLine("");
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

                    if (!string.IsNullOrEmpty(item.ExceptionMessage))
                    {
                        Console.SetCursorPosition(65, Console.CursorTop);
                        Console.Write($"{item.ExceptionMessage}");
                    }
                    Console.WriteLine(" ");
                }

                Console.WriteLine(" ");
            }
        }
    }

    public class GroupTestResult : SingleTestResult
    {
        public List<SingleTestResult> TestList = new List<SingleTestResult>();

        public GroupTestResult(string name) : base(name) { }

        public SingleTestResult RunSingleTest(string name)
        {
            SingleTestResult test = new SingleTestResult(name);
            TestList.Add(test);
            return test;
        }

        public override void End()
        {
            Duration = (DateTime.Now - StartTime).TotalSeconds;
            TestState = TestList.All(test => test.TestState == TestState.Success) ? TestState.Success : TestState.Failed;
        }
    }

    public class SingleTestResult
    {
        public string Name { get; set; }

        public double Duration { get; protected set; }

        public string ExceptionMessage { get; set; }

        protected DateTime StartTime { get; set; }

        public TestState TestState { get; set; }

        public SingleTestResult(string name)
        {
            StartTime = DateTime.Now;
            Name = name;
        }
        public virtual void End()
        {
            Duration = (DateTime.Now - StartTime).TotalSeconds;
        }
    }
}
