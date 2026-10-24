namespace Dag
{
    public sealed class Dag<T>
    {
        private readonly Dictionary<T, HashSet<T>> _dependents;
        public Dag()
        {
            _dependents = new Dictionary<T, HashSet<T>>();
        }
        public void AddNode(T node)
        {
            if (!_dependents.ContainsKey(node))
            {
                _dependents[node] = [];
            }
        }
        public void AddDependency(T dependsOn, T dependency)
        {
            if (!_dependents.ContainsKey(dependsOn))
            {
                throw new ArgumentException($"Node '{dependsOn}' does not exist in this context.");
            }
            if (!_dependents.ContainsKey(dependency))
            {
                throw new ArgumentException($"Node '{dependency}' does not exist in this context.");
            }
            //Note to add Cyclic dependency preventor here after you make the methods 
            /*if (WouldCreateCycle(dependsOn, dependency){
             *    throw new InvalidOperationException("Adding this dependency would create a cycle.");
             * }
               */
            _dependents[dependsOn].Add(dependency);
        }
        public IReadOnlyCollection<T> getDependents(T value)
        {

            return _dependents[value].ToList();
        }

    }
}

