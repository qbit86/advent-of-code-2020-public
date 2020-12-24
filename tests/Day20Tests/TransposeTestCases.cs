using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2020
{
#pragma warning disable CA1812 // TransposeTestCases is an internal class that is apparently never instantiated.
    internal sealed class TransposeTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string[] input =
            {
                ".#.",
                "..#",
                "###"
            };
            string[] expected =
            {
                "..#",
                "#.#",
                ".##"
            };
            yield return new object[] { input, expected };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
#pragma warning restore CA1812
}
