using System;

namespace SpecChum
{
    internal static class Enum<T>
    {
        internal static bool IsDefined(T value)
        {
            return Enum.IsDefined(typeof(T), value);
        }
    }
}
