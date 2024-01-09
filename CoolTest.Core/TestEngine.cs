namespace CoolTest.Core
{
    public class TestEngine
    {
        public async Task Run(string[] assemblies)
        {
            await ProgressBar(2, 1, 100, 4, ConsoleColor.Yellow);

            foreach (var assembly in assemblies)
            {
                using (var provider = new TestProvider(assembly))
                {
                    var testGroups = provider.GetTests();
                    foreach (var group in testGroups)
                    {
                        group.Run();
                    }
                }
            }

            await Console.Out.WriteLineAsync("Test Passed");
        }

        public static async Task ProgressBar(int sol, int ust, int deger, int isaret, ConsoleColor color)
        {
            char[] symbol = new char[5] { '\u25A0', '\u2592', '\u2588', '\u2551', '\u2502' };

            int maxBarSize = Console.BufferWidth - 1;
            int barSize = deger;
            decimal f = 1;

            if (barSize + sol > maxBarSize)
            {
                barSize = maxBarSize - (sol + 5);
                f = (decimal)deger / (decimal)barSize;
            }

            Console.CursorVisible = false;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(sol + 5, ust);

            for (int i = 0; i < barSize + 1; i++)
            {
                await Task.Delay(20);
                Console.Write(symbol[isaret]);
                Console.SetCursorPosition(sol, ust);
                Console.Write("%" + (i * f).ToString("0,0"));
                Console.SetCursorPosition(sol + 5 + i, ust);
            }

            Console.ResetColor();
        }
    }
}
