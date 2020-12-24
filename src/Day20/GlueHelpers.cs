using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public static class GlueHelpers
    {
        public static string[] Glue(IReadOnlyList<OrientedTile> reassembledTiles)
        {
            if (reassembledTiles is null)
                throw new ArgumentNullException(nameof(reassembledTiles));

            int lengthInTiles = (int)Math.Sqrt(reassembledTiles.Count);
            int effectiveTileLength = reassembledTiles[0].Data.Length - 2;
            int resultLength = lengthInTiles * effectiveTileLength;
            string[] result = new string[resultLength];
            Span<char> currentLine = stackalloc char[resultLength];
            for (int i = 0; i < resultLength; ++i)
            {
                currentLine.Clear();
                // ReSharper disable once UselessBinaryOperation
                int tileRow = Math.DivRem(i, effectiveTileLength, out int rowWithinTile);
                for (int tileColumn = 0; tileColumn < lengthInTiles; ++tileColumn)
                {
                    Span<char> destinationSlice =
                        currentLine.Slice(tileColumn * effectiveTileLength, effectiveTileLength);
                    OrientedTile tile = reassembledTiles[tileRow * lengthInTiles + tileColumn];
                    string lineWithinTile = tile.Data[1 + rowWithinTile];
                    lineWithinTile.AsSpan(1, effectiveTileLength).CopyTo(destinationSlice);
                }

                result[i] = new string(currentLine);
            }

            return result;
        }
    }
}
