using System;

namespace SpecChum.Extensions
{
    public static class ByteExtensions
    {
        public static bool IsBitSet(this byte value, int bit)
        {
            if (bit < 0 || bit > 7)
                throw new ArgumentException("Bit must be between 0 and 7");

            return IsBitSet((int)value, bit);
        }

        public static bool IsBitSet(this int value, int bit)
        {
            if (bit < 0 || bit > 31)
                throw new ArgumentException("Bit must be between 0 and 31");

            return (value >> bit) == 1;
        }

        public static byte SetBit(this byte value, int bit, bool isSet)
        {
            if (bit < 0 || bit > 7)
                throw new ArgumentException("Bit must be between 0 and 7");

            return (byte)SetBit((int)value, bit, isSet);
        }

        public static int SetBit(this int value, int bit, bool isSet)
        {
            if (bit < 0 || bit > 31)
                throw new ArgumentException("Bit must be between 0 and 31");

            return isSet
                ? (value | (1 << bit))
                : (int)(value & uint.MaxValue - (1 << bit));
        }

        public static byte GetBits(this byte value, int fromBit, int? toBit = null)
        {
            return (byte)GetBits((int)value, fromBit, toBit);
        }

        public static int GetBits(this int value, int fromBit, int? toBit = null)
        {
            if (toBit == null)
                toBit = fromBit;

            int mask = GetMask(fromBit, toBit.Value);
            int maskedValue = value & mask;

            return maskedValue >> fromBit;
        }

        public static byte SetBits(this byte value, int fromBit, int toBit, byte setValue)
        {
            return (byte)SetBits((int)value, fromBit, toBit, setValue);
        }

        public static int SetBits(this int value, int fromBit, int toBit, int setValue)
        {
            int mask = GetMask(fromBit, toBit);
            int maskedValue = setValue & mask;

            return value | maskedValue;
        }

        private static int GetMask(int fromBit, int toBit)
        {
            if (toBit < fromBit)
                throw new ArgumentException("To bit must be greater than or equal to from bit");

            return ((1 << (toBit + 1)) - 1) ^ ((1 << fromBit) - 1);
        }
    }
}
