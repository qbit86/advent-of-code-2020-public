using System;
using System.Collections.Generic;
using System.Diagnostics;
using Arborescence;

namespace AdventOfCode2020
{
    internal sealed class Graph : IIncidenceGraph<Node, Endpoints<Node>, IEnumerator<Endpoints<Node>>>
    {
        internal static Graph Instance { get; } = new();

        public bool TryGetHead(Endpoints<Node> edge, out Node head)
        {
            head = edge.Head;
            return true;
        }

        public bool TryGetTail(Endpoints<Node> edge, out Node tail)
        {
            tail = edge.Tail;
            return true;
        }

        public IEnumerator<Endpoints<Node>> EnumerateOutEdges(Node vertex)
        {
            int orientedTileCount = vertex.OrientedTiles.Count;
            int width = (int)Math.Sqrt(orientedTileCount + vertex.RawTiles.Count);
            foreach (RawTile rawTile in vertex.RawTiles)
            {
                for (TransformKinds transform = TransformKinds.None; transform < (TransformKinds)8; ++transform)
                {
                    var orientedTile = OrientedTile.Create(rawTile.Id, rawTile.Data, transform);
                    if (orientedTileCount % width != 0)
                    {
                        if (vertex.OrientedTiles[^1].RightBorder != orientedTile.LeftBorder)
                            continue;
                    }

                    if (orientedTileCount >= width)
                    {
                        if (vertex.OrientedTiles[orientedTileCount - width].BottomBorder != orientedTile.TopBorder)
                            continue;
                    }

                    List<OrientedTile> orientedTiles = new(vertex.OrientedTiles) { orientedTile };

                    HashSet<RawTile> rawTiles = new(vertex.RawTiles);
                    bool success = rawTiles.Remove(rawTile);
                    Debug.Assert(success, $"{nameof(rawTiles)}.Remove({nameof(rawTile)})");
                    Node head = Node.Create(orientedTiles, rawTiles);
                    yield return new Endpoints<Node>(vertex, head);
                }
            }
        }
    }
}
