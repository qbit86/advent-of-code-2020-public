using System;
using System.Linq;

namespace AdventOfCode2020
{
    public static class TransformHelpers
    {
        private static string[] FlipX(string[] data)
        {
            string[] result = data.ToArray();
            Array.Reverse(result);
            return result;
        }

        private static string[] FlipY(string[] data) => data.Select(Reverse).ToArray();

        public static string[] Transpose(string[] data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            int count = data.Length;
            Span<char> buffer = stackalloc char[count * count];
            for (int i = 0; i < count; ++i)
            {
                string line = data[i];
                if (line.Length < count)
                    throw new InvalidOperationException(nameof(data));

                for (int j = 0; j < count; ++j)
                {
                    int destinationIndex = j * count + i;
                    buffer[destinationIndex] = line[j];
                }
            }

            string[] result = new string[count];
            for (int i = 0; i < count; ++i)
            {
                ReadOnlySpan<char> slice = buffer.Slice(i * count, count);
                result[i] = new string(slice);
            }

            return result;
        }

        public static string[] ApplyTransform(string[] data, TransformKinds transform)
        {
            string[] result = data ?? throw new ArgumentNullException(nameof(data));
            if ((transform & TransformKinds.Transpose) != 0)
                result = Transpose(result);

            if ((transform & TransformKinds.FlipY) != 0)
                result = FlipY(result);

            if ((transform & TransformKinds.FlipX) != 0)
                result = FlipX(result);

            return result;
        }

        private static string Reverse(string input)
        {
            char[] buffer = input.ToCharArray();
            Array.Reverse(buffer);
            return new string(buffer);
        }
    }
}
