using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolTest.Abstarctions
{
    public class TestEventArgs : EventArgs
    {
        public string AssemblyName { get; init; }
        public string GroupName { get; init; }
        public string TestName { get; init; }
    }

    public class AfterTestEventArgs : TestEventArgs
    {
        public TestState Result { get; init; }
    }
}
