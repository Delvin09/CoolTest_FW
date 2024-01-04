using CoolTest.Core.Logger;

namespace CoolTest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new Core.TestEngine(new Logger());
            engine.Run(args);
        }
    }
}
