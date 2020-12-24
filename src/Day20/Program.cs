using System;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]

namespace AdventOfCode2020
{
    internal static class Program
    {
        private static async Task Main()
        {
            long result = await ProblemPartTwo.Instance.Solve("input.txt").ConfigureAwait(false);
            Console.WriteLine(result);
        }
    }
}
