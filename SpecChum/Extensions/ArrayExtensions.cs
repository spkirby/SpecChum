using System;

namespace SpecChum.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Copy<T>(this T[] source)
        {
            return Copy(source, 0, source.Length);
        }

        public static T[] Copy<T>(this T[] source, int offset, int length)
        {
            T[] destination = new T[length];

            Array.Copy(source, offset, destination, 0, length);

            return destination;
        }

        public static Word GetWord(this byte[] array, int offset)
        {
            return new Word(array[offset + 1], array[offset]);
        }

        public static void SetWord(this byte[] array, int offset, Word word)
        {
            array[offset] = word.Low;
            array[offset + 1] = word.High;
        }

        public static void SetByte(this byte[] array, int offset, byte value)
        {
            array[offset] = value;
        }
    }
}
