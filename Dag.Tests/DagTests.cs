

namespace Dag.Tests
{
    [TestClass]
    public sealed class DagTests{
        [TestMethod]
        public void AddNode_DuplicateNode_DoesNotDuplicateButDoesNotThrow() {
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
