using System;

namespace AdventOfCode2020
{
    internal readonly struct Border : IEquatable<Border>
    {
        internal string Data { get; }

        internal Border(string data) => Data = data;

        public bool Equals(Border other) => Data == other.Data;

        public override string ToString() => Data;

        public override bool Equals(object? obj) => obj is Border other && Equals(other);

        public override int GetHashCode() => Data.GetHashCode(StringComparison.Ordinal);

        public static bool operator ==(Border left, Border right) => left.Equals(right);

        public static bool operator !=(Border left, Border right) => !left.Equals(right);
    }
}
