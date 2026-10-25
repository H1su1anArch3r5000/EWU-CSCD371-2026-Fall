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
        //Extra Credit here:
        public void RemoveNode(T node) {
            EnsureNodeExists(node);
            foreach (HashSet<T> depender in _dependents.Values) { 
                depender.Remove(node);
            }
            _dependents.Remove(node);
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
             
            if (WouldCreateCycle(dependsOn, dependency)){
                 throw new InvalidOperationException("Adding this dependency would create a cycle.");
              }
              
            _dependents[dependsOn].Add(dependency);
        }
        public IReadOnlyCollection<T> getDependents(T value)
        {
            EnsureNodeExists(value);
            return _dependents[value].ToList();
        }
        private void EnsureNodeExists(T value) {
            if (!_dependents.ContainsKey(value))
            {
                throw new ArgumentException($"Node {Value} does not exist.");
            }
        }
        public bool WouldCreateCycle(T dependsOn, T dependency) {
            HashSet<T> visited = [];
            return CanReach(dependency, dependsOn, visited);
        }
        private bool CanReach(T current, T target, HashSet<T> visited)
        {
            
            if (EqualityComparer<T>.Default.Equals(current, target))
            {
                return true;
            }

            if (!visited.Add(current))
            {
                return false;
            }

            foreach (T neighbor in _dependents[current])
            {
                if (CanReach(neighbor, target, visited))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
