using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    internal sealed class Node : IEquatable<Node>
    {
        private readonly int _hashCode;

        private Node(IReadOnlyList<OrientedTile> orientedTiles, IReadOnlySet<RawTile> rawTiles, int hashCode)
        {
            OrientedTiles = orientedTiles;
            RawTiles = rawTiles;
            _hashCode = hashCode;
        }

        internal IReadOnlyList<OrientedTile> OrientedTiles { get; }
        internal IReadOnlySet<RawTile> RawTiles { get; }
        internal bool IsTerminal => RawTiles.Count == 0;

        public bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return _hashCode == other._hashCode && OrientedTiles.SequenceEqual(other.OrientedTiles);
        }

        public override string ToString() =>
            $"{nameof(Node)} {{ {nameof(OrientedTiles)} = {OrientedTiles.Count}, {nameof(RawTiles)} = {RawTiles.Count} }}";

        internal static Node Create(IReadOnlyList<OrientedTile> orientedTiles, IReadOnlySet<RawTile> rawTiles)
        {
            int hashCode = orientedTiles.Aggregate(1729, (total, tile) => HashCode.Combine(total, tile.Id));
            return new Node(orientedTiles, rawTiles, hashCode);
        }

        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Node other && Equals(other);

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(Node? left, Node? right) => Equals(left, right);

        public static bool operator !=(Node? left, Node? right) => !Equals(left, right);
    }
}
