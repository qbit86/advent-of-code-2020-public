using System;
using System.Globalization;

namespace AdventOfCode2020
{
    public sealed class RawTile : IEquatable<RawTile>
    {
        private static readonly string[] s_lineSeparator = { "\n", "\r\n" };

        private RawTile(int id, string[] data)
        {
            Id = id;
            Data = data;
        }

        public int Id { get; }
        internal string[] Data { get; }

        public bool Equals(RawTile? other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id;
        }

        public override string ToString() => $"{nameof(RawTile)} {{ {nameof(Id)} = {Id} }}";

        public static RawTile Parse(string input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            string[] outerParts = input.Split(':', StringSplitOptions.TrimEntries);
            string[] innerParts = outerParts[0].Split(' ');
            if (!string.Equals(innerParts[0], "Tile", StringComparison.Ordinal))
                throw new InvalidOperationException("Tile");

            int id = int.Parse(innerParts[1], CultureInfo.InvariantCulture);
            string[] data = outerParts[1].Split(s_lineSeparator, StringSplitOptions.TrimEntries);

            return new RawTile(id, data);
        }

        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is RawTile other && Equals(other);

        public override int GetHashCode() => Id;

        public static bool operator ==(RawTile? left, RawTile? right) => Equals(left, right);

        public static bool operator !=(RawTile? left, RawTile? right) => !Equals(left, right);
    }
}
