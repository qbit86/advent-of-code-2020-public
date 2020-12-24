using System;

namespace AdventOfCode2020
{
    [Flags]
    public enum TransformKinds
    {
        None = 0,
        FlipX = 1,
        FlipY = 0b10,
        Transpose = 0b100
    }
}
