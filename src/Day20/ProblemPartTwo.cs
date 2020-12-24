using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public sealed class ProblemPartTwo : Problem
    {
        public static ProblemPartTwo Instance { get; } = new();

        protected override long SolveCore(Dictionary<int, RawTile> tileById)
        {
            IReadOnlyList<OrientedTile> reassembledTiles = Reassemble(tileById);
            string[] rawImage = GlueHelpers.Glue(reassembledTiles);

            string[] monsterPattern =
            {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };
            for (int transform = 0; transform < 8; ++transform)
            {
                string[] transformedImage = TransformHelpers.ApplyTransform(rawImage, transform);
                if (ContainsPattern(transformedImage, monsterPattern))
                    return DetermineRoughness(transformedImage, monsterPattern);
            }

            return rawImage.Select(line => line.Count(it => it == '#')).Sum();
        }

        private static int DetermineRoughness(string[] image, string[] pattern)
        {
            int imageHeight = image.Length;
            if (imageHeight == 0)
                throw new ArgumentException(nameof(imageHeight), nameof(image));

            int imageWidth = image[0].Length;
            if (imageWidth == 0)
                throw new ArgumentException(nameof(imageWidth), nameof(image));

            int patternHeight = pattern.Length;
            if (patternHeight == 0)
                throw new ArgumentException(nameof(patternHeight), nameof(pattern));

            int patternWidth = pattern[0].Length;
            if (patternWidth == 0)
                throw new ArgumentException(nameof(patternWidth), nameof(pattern));

            int patternCount = 0;
            for (int row = 0; row < imageHeight - patternHeight; ++row)
            {
                for (int column = 0; column < imageWidth - patternWidth; ++column)
                {
                    if (ContainsPatternAtPosition(image, pattern, row, column))
                        ++patternCount;
                }
            }

            int totalSharpCount = image.Select(line => line.Count(it => it == '#')).Sum();
            int patternSharpCount = pattern.Select(line => line.Count(it => it == '#')).Sum();
            return totalSharpCount - patternSharpCount * patternCount;
        }

        private static bool ContainsPattern(string[] image, string[] pattern)
        {
            int imageHeight = image.Length;
            if (imageHeight == 0)
                throw new ArgumentException(nameof(imageHeight), nameof(image));

            int imageWidth = image[0].Length;
            if (imageWidth == 0)
                throw new ArgumentException(nameof(imageWidth), nameof(image));

            int patternHeight = pattern.Length;
            if (patternHeight == 0)
                throw new ArgumentException(nameof(patternHeight), nameof(pattern));

            int patternWidth = pattern[0].Length;
            if (patternWidth == 0)
                throw new ArgumentException(nameof(patternWidth), nameof(pattern));

            for (int row = 0; row < imageHeight - patternHeight; ++row)
            {
                for (int column = 0; column < imageWidth - patternWidth; ++column)
                {
                    if (ContainsPatternAtPosition(image, pattern, row, column))
                        return true;
                }
            }

            return false;
        }

        private static bool ContainsPatternAtPosition(string[] image, string[] pattern, int row, int column)
        {
            int imageHeight = image.Length;
            if (imageHeight == 0)
                throw new ArgumentException(nameof(imageHeight), nameof(image));

            int imageWidth = image[0].Length;
            if (imageWidth == 0)
                throw new ArgumentException(nameof(imageWidth), nameof(image));

            int patternHeight = pattern.Length;
            if (patternHeight == 0)
                throw new ArgumentException(nameof(patternHeight), nameof(pattern));

            int patternWidth = pattern[0].Length;
            if (patternWidth == 0)
                throw new ArgumentException(nameof(patternWidth), nameof(pattern));

            if (unchecked((uint)row >= (uint)(imageHeight - patternHeight)))
                return false;

            if (unchecked((uint)column >= (uint)(imageWidth - patternWidth)))
                return false;

            for (int i = 0; i < patternHeight; ++i)
            {
                ReadOnlySpan<char> patternLine = pattern[i];
                ReadOnlySpan<char> imageSlice = image[row + i].AsSpan(column, patternLine.Length);
                for (int j = 0; j < patternLine.Length; ++j)
                {
                    if (patternLine[j] == '#' && imageSlice[j] != '#')
                        return false;
                }
            }

            return true;
        }
    }
}
