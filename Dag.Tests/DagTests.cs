namespace Dag.Tests
{
    [TestClass]
    public sealed class DagTests
    {
        [TestMethod]
        public void AddNode_AddDuplicate_ButDoesNotThrow()
        {
            Dag<string> tester = new();
            tester.AddNode("A");
            tester.AddNode("A");
            Assert.IsEmpty(tester.GetDependents("A"));
        }
        [TestMethod]
        public void AddDependency_ConnectsProperly()
        {
            Dag<string> dag = new();
            dag.AddNode("A");
            dag.AddNode("B");

            dag.AddDependency("A", "B");

            Assert.Contains("B", dag.GetDependents("A"));
        }
        [TestMethod]
        public void Edge_Is_VisibleFromBothSides()
        {
            var dag = new Dag<string>();
            dag.AddNode("A");
            dag.AddNode("B");
            dag.AddDependency("B", "A");   // B depends on A

            CollectionAssert.Contains(dag.GetDependencies("B").ToList(), "A");
            CollectionAssert.Contains(dag.GetDependents("A").ToList(), "B");
        }
    }
}
