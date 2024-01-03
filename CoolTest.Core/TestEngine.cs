using System.Xml.Serialization;

namespace CoolTest.Core
{
     public class TestEngine
    {
        public EventHandler<GroupRunEventArgs>? BeforeGroupRun;
        public EventHandler<GroupRunEventArgs>? AfterGroupRun;
        public EventHandler<GroupRunEventArgs>? ForwardedBeforeGroupRun;
        public EventHandler<GroupRunEventArgs>? ForwardedAfterGroupRun;
        public void Run(string[] assemblies)
        {
            BeforeGroupRun += OnBeforeGroupRun;
            AfterGroupRun += OnAfterGroupRun;

            foreach (var assembly in assemblies)
            {
                using (var provider = new TestProvider(assembly))
                {
                    var testGroups = provider.GetTests();
                    foreach (var group in testGroups)
                    {
                        OnBeforeGroupRun(this, new GroupRunEventArgs(group));
                        OnForwardedBeforeGroupRun(new GroupRunEventArgs(group));

                        group.Run();

                        OnAfterGroupRun(this, new GroupRunEventArgs(group));
                        OnForwardedAfterGroupRun(new GroupRunEventArgs(group));
                    }
                }
            }

            BeforeGroupRun -= OnBeforeGroupRun;
            AfterGroupRun -= OnAfterGroupRun;
        }

        private void OnBeforeGroupRun(object sender, GroupRunEventArgs e)
        {
            BeforeGroupRun?.Invoke(this, e);
        }

        private void OnAfterGroupRun(object sender, GroupRunEventArgs e)
        {
            AfterGroupRun?.Invoke(this, e);
        }

        private void OnForwardedBeforeGroupRun(GroupRunEventArgs e)
        {
            ForwardedBeforeGroupRun?.Invoke(this, e);
        }

        private void OnForwardedAfterGroupRun(GroupRunEventArgs e)
        {
            ForwardedAfterGroupRun?.Invoke(this, e);
        }
    }
    public class GroupRunEventArgs : EventArgs 
    {
        internal TestGroup TestGroup { get; }

        internal GroupRunEventArgs(TestGroup testGroup) => TestGroup = testGroup;
    }
}
