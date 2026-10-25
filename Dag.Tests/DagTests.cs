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
        [TestMethod]
        public void RemoveNode_MissingNode_ReturnsFalse()
        {
            var g = new Dag<string>();
            g.AddNode("A");

            Assert.IsFalse(g.RemoveNode("Z"));
        }
        [TestMethod]
        public void RemoveNode_RemovesNodeAndEdges_LeavesOthersUnchanged()
        {
            var testDag = new Dag<string>();
            foreach (var n in new[] { "A", "B", "C" })
                testDag.AddNode(n);

            testDag.AddDependency("A", "B");   // incoming to B
            testDag.AddDependency("B", "C");   // outgoing from B
            testDag.AddDependency("A", "C");   // unrelated to B

            Assert.IsTrue(testDag.RemoveNode("B"));

            // B is gone from everyone's edges
            CollectionAssert.AreEquivalent(new[] { "C" }, testDag.GetDependencies("A").ToList());
            CollectionAssert.AreEquivalent(new[] { "A" }, testDag.GetDependents("C").ToList());

            // Nothing else changed
            Assert.AreEqual(0, testDag.GetDependents("A").Count());
            Assert.AreEqual(0, testDag.GetDependencies("C").Count());
        }
    }
}

