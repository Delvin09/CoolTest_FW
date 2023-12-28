namespace CoolTest.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new Core.TestEngine();
            engine.Run(args);
        }
    }
}
