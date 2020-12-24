using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Arborescence;
using Arborescence.Traversal;

namespace AdventOfCode2020
{
    public abstract class Problem
    {
        private static readonly string[] s_groupSeparator = { "\n\n", "\r\n\r\n" };

        public async Task<long> Solve(string path)
        {
            string text = await File.ReadAllTextAsync(path).ConfigureAwait(false);
            string[] parts = text.Split(s_groupSeparator,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, RawTile> tileById = parts.Select(RawTile.Parse).ToDictionary(it => it.Id, it => it);
            return SolveCore(tileById);
        }

        protected abstract long SolveCore(Dictionary<int, RawTile> tileById);

        public static IReadOnlyList<OrientedTile> Reassemble(Dictionary<int, RawTile> tileById)
        {
            if (tileById is null)
                throw new ArgumentNullException(nameof(tileById));

            Node root = Node.Create(Array.Empty<OrientedTile>(), tileById.Values.ToHashSet());
            EnumerableDfs<
                Graph, Node, Endpoints<Node>, IEnumerator<Endpoints<Node>>, HashSet<Node>, ExploredSetPolicy> dfs =
                new(default);
            IEnumerator<Node> nodes = dfs.EnumerateVertices(Graph.Instance, root, new HashSet<Node>());
            while (nodes.MoveNext())
            {
                Node currentNode = nodes.Current;
                if (currentNode.IsTerminal)
                    return currentNode.OrientedTiles;
            }

            throw new InvalidOperationException(nameof(Reassemble));
        }
    }
}
