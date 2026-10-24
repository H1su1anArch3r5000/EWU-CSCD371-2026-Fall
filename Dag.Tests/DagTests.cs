

namespace Dag.Tests
{
    [TestClass]
    public sealed class DagTests{
        [TestMethod]
        public void AddNode_AddDuplicate_ButDoesNotThrow() {
            Dag<string> tester = new();
            tester.AddNode("A");
            tester.AddNode("A");
            Assert.IsEmpty(tester.getDependents("A"));
        }
        [TestMethod]
        public void AddDependency_ConnectsProperly() { 
            Dag<string> dag = new();
            dag.AddNode("A");
            dag.AddNode("B");

            dag.AddDependency("A", "B");

            Assert.Contains("B", dag.getDependents("A"));
        }
    }
}
