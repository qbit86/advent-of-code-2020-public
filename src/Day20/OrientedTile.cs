using System;
using System.Linq;

namespace AdventOfCode2020
{
    public sealed class OrientedTile : IEquatable<OrientedTile>
    {
        private OrientedTile(int id, string[] data, TransformKinds transform,
            Border leftBorder, Border topBorder, Border rightBorder, Border bottomBorder)
        {
            Id = id;
            Data = data;
            Transform = transform;
            LeftBorder = leftBorder;
            TopBorder = topBorder;
            RightBorder = rightBorder;
            BottomBorder = bottomBorder;
        }

        internal int Id { get; }
        internal string[] Data { get; }
        internal TransformKinds Transform { get; }
        internal Border LeftBorder { get; }
        internal Border TopBorder { get; }
        internal Border RightBorder { get; }
        internal Border BottomBorder { get; }

        public bool Equals(OrientedTile? other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id && Transform == other.Transform;
        }

        public override string ToString() =>
            $"{nameof(OrientedTile)} {{ {nameof(Id)} = {Id}, {nameof(Transform)} = {Transform}, Left = {LeftBorder}, Top = {TopBorder}, Right = {RightBorder}, Bottom = {BottomBorder} }}";

        internal static OrientedTile Create(int id, string[] data, TransformKinds transform)
        {
            string[] transformedData = TransformHelpers.ApplyTransform(data, transform);
            return new OrientedTile(id, transformedData, transform,
                GetLeftBorder(transformedData), GetTopBorder(transformedData),
                GetRightBorder(transformedData), GetBottomBorder(transformedData));
        }

        private static Border GetLeftBorder(string[] data)
        {
            char[] leftCharacters = data.Select(it => it[0]).ToArray();
            string s = new(leftCharacters);
            return new Border(s);
        }

        private static Border GetRightBorder(string[] data)
        {
            char[] rightCharacters = data.Select(it => it[^1]).ToArray();
            string s = new(rightCharacters);
            return new Border(s);
        }

        private static Border GetTopBorder(string[] data) => new(data[0]);

        private static Border GetBottomBorder(string[] data) => new(data[^1]);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || obj is OrientedTile other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Id, Transform);

        public static bool operator ==(OrientedTile? left, OrientedTile? right) => Equals(left, right);

        public static bool operator !=(OrientedTile? left, OrientedTile? right) => !Equals(left, right);
    }
}
