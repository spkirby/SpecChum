using System;

namespace SpecChum
{
    public readonly struct Word : IEquatable<Word>
    {
        public byte High { get; }
        public byte Low { get; }
        public ushort Value => (ushort)((High << 8) + Low);

        public Word(byte high, byte low)
        {
            High = high;
            Low = low;
        }

        public Word(ushort value)
        {
            High = (byte)(value >> 8);
            Low = (byte)(value & 0xff);
        }

        public static explicit operator Word(ushort value)
        {
            return new Word(value);
        }

        public static explicit operator ushort(Word word)
        {
            return word.Value;
        }

        public static implicit operator int(Word word)
        {
            return word.Value;
        }

        public static bool operator ==(Word a, Word b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Word a, Word b)
        {
            return !(a == b);
        }

        public override bool Equals(object other)
        {
            return other is Word word
                && Equals(word);
        }

        public bool Equals(Word other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString("x4");
        }
    }
}
