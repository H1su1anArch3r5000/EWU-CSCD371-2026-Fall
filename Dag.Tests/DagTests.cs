namespace Dag.Tests
{
    [TestClass]
    public sealed class DagTests
    {
        [TestMethod]
        public void AddNode_DuplicateNode_DoesNotDuplicateButDoesNotThrow(){
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
        [TestMethod]
        public void AddDependency_CycleDetected_Throws() {
            Dag<string> dag = new();
            dag.AddNode("A");
            dag.AddNode("B");
            dag.AddNode("C");

            dag.AddDependency("A", "B");
            dag.AddDependency("B", "C");
            
            Assert.Throws<InvalidOperatorException>(() => dag.AddDependency("C","A"));
        }
        [TestMethod]
        public void RemoveNode_RemovesIncomingAndOutgoingEdges()
        {
            Dag<string> dag = new();

            dag.AddNode("A");
            dag.AddNode("B");
            dag.AddNode("C");
            dag.AddNode("D");

            dag.AddDependency("A", "B");
            dag.AddDependency("A", "C");
            dag.AddDependency("B", "D");
            dag.AddDependency("C", "D");

            dag.RemoveNode("B");

            CollectionAssert.DoesNotContain(
                dag.GetDependents("A").ToList(),
                "B");

            CollectionAssert.Contains(
                dag.GetDependents("A").ToList(),
                "C");

            CollectionAssert.Contains(
                dag.GetDependents("C").ToList(),
                "D");
        }
        [TestMethod]
        public void GetDependents_ReturnsDirectDependents()
        {
            Dag<string> dag = new();

            dag.AddNode("A");
            dag.AddNode("B");
            dag.AddNode("C");

            dag.AddDependency("A", "B");
            dag.AddDependency("A", "C");

            IReadOnlyCollection<string> result =
                dag.GetDependents("A");

            Assert.Equal(2, result.Count);
            Assert.Contains("B", result);
            Assert.Contains("C", result);
        }

    }
}

