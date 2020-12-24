using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public sealed class ProblemPartOne : Problem
    {
        public static ProblemPartOne Instance { get; } = new();

        protected override long SolveCore(Dictionary<int, RawTile> tileById)
        {
            IReadOnlyList<OrientedTile> reassembledTiles = Reassemble(tileById);
            int length = (int)Math.Sqrt(reassembledTiles.Count);
            long topLeftCornerId = reassembledTiles[0].Id;
            long topRightCornerId = reassembledTiles[length - 1].Id;
            long bottomLeftCornerId = reassembledTiles[(length - 1) * length].Id;
            long bottomRightCornerId = reassembledTiles[^1].Id;
            long result = checked(topLeftCornerId * topRightCornerId * bottomLeftCornerId * bottomRightCornerId);
            return result;
        }
    }
}
