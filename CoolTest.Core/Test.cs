using System.Reflection;

namespace CoolTest.Core
{
    internal class Test
    {
        public string Name { get; set; }

        public MethodInfo Method { get; set; }

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
