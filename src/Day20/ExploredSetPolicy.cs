using System.Collections.Generic;
using Arborescence;

namespace AdventOfCode2020
{
    internal readonly struct ExploredSetPolicy : ISetPolicy<HashSet<Node>, Node>
    {
        public bool Contains(HashSet<Node> items, Node item) => items.Contains(item);

        public void Add(HashSet<Node> items, Node item) => items.Add(item);
    }
}
