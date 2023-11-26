using System;
using System.IO;

namespace SpecChum.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte ReadByteOrThrow(this Stream stream)
        {
            var data = stream.ReadByte();

            if (data == -1)
            {
                throw new MediaReadException("Unexpected end of stream");
            }

            return (byte)data;
        }

        public static Word ReadWord(this Stream stream)
        {
            var low = stream.ReadByteOrThrow();
            var high = stream.ReadByteOrThrow();

            return new Word(high, low);
        }

        public static void WriteWord(this Stream stream, Word word)
        {
            stream.WriteByte(word.Low);
            stream.WriteByte(word.High);
        }
    }
}
