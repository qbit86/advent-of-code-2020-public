using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace AdventOfCode2020
{
    public sealed class GlueHelpersTests
    {
        private static readonly string[] s_groupSeparator = { "\n\n", "\r\n\r\n" };

        [Theory]
        [InlineData("sample-input.txt", "glued-image.txt")]
        internal async Task Glue(string inputPath, string expectedPath)
        {
            string[] expected = await File.ReadAllLinesAsync(expectedPath).ConfigureAwait(false);

            string text = await File.ReadAllTextAsync(inputPath).ConfigureAwait(false);
            string[] parts = text.Split(s_groupSeparator,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, RawTile> tileById = parts.Select(RawTile.Parse).ToDictionary(it => it.Id, it => it);
            IReadOnlyList<OrientedTile> reassembledTiles = Problem.Reassemble(tileById);
            string[] rawImage = GlueHelpers.Glue(reassembledTiles);

            for (int transform = 0; transform < 8; ++transform)
            {
                string[] actual = TransformHelpers.ApplyTransform(rawImage, transform);
                if (expected.SequenceEqual(actual))
                    return;
            }

            throw new XunitException();
        }
    }
}
