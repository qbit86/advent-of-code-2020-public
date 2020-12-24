using Xunit;

namespace AdventOfCode2020
{
    public sealed class TransformHelpersTests
    {
        [Theory]
        [ClassData(typeof(TransposeTestCases))]
        internal void Transpose(string[] input, string[] expected)
        {
            string[] actual = TransformHelpers.Transpose(input);

            Assert.Equal(expected, actual);
        }
    }
}
