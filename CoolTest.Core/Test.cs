using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        public Test(string name, MethodInfo method)
        {
            Name = name;
            Method = method;
        }

        public string Name { get; }

        public MethodInfo Method { get; }

        public void Run(object subject)
        {
            try
            {
                Method.Invoke(subject, null);
            }
            catch (Exception ex)
            {
                //TODO: handle ex to results
            }
        }
    }
}
