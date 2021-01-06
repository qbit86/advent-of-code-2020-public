using System;
using System.Threading.Tasks;
using Xunit;

[assembly: CLSCompliant(false)]

namespace AdventOfCode2020
{
    public sealed class Tests
    {
        [Theory]
        [InlineData("sample-input.txt", 20899048083289L)]
        [InlineData("input.txt", 30425930368573L)]
        internal async Task SolvePartOne(string path, long expected)
        {
            long actual = await ProblemPartOne.Instance.Solve(path).ConfigureAwait(false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("sample-input.txt", 273L)]
        [InlineData("input.txt", 2453L)]
        internal async Task SolvePartTwo(string path, long expected)
        {
            long actual = await ProblemPartTwo.Instance.Solve(path).ConfigureAwait(false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("input.txt", 2576L)]
        internal async Task PartTwo_ReturnsLess(string path, long tooHigh)
        {
            long actual = await ProblemPartTwo.Instance.Solve(path).ConfigureAwait(false);
            Assert.InRange(actual, 0L, tooHigh - 1L);
        }
    }
}
