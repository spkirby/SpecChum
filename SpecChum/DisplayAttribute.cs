namespace SpecChum
{
    public struct DisplayAttribute
    {
        public byte Value { get; }

        public Colour Foreground => (Colour)(Value & 0x07);
        public Colour Background => (Colour)((Value >> 3) & 0x07);
        public bool IsBright => (Value & 0x40) != 0;
        public bool IsFlash => (Value & 0x80) != 0;

        public DisplayAttribute(byte value)
        {
            Value = value;
        }

        public DisplayAttribute(Colour foreground, Colour background = Colour.Black, bool isBright = false, bool isFlash = false)
        {
            Value = (byte)((isFlash ? 128 : 0) + (isBright ? 64 : 0) + ((int)background << 3) + (int)foreground);
        }
    }
}
