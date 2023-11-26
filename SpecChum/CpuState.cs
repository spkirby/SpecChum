using System;

namespace SpecChum
{
    public class CpuState : IEquatable<CpuState>
    {
        public Word AF { get; set; }
        public Word BC { get; set; }
        public Word DE { get; set; }
        public Word HL { get; set; }
        public Word IX { get; set; }
        public Word IY { get; set; }
        public Word PC { get; set; }
        public Word SP { get; set; }
        public Word AFAlt { get; set; }
        public Word BCAlt { get; set; }
        public Word DEAlt { get; set; }
        public Word HLAlt { get; set; }
        public byte I { get; set; }
        public byte R { get; set; }
        public bool IFF1 { get; set; }
        public bool IFF2 { get; set; }
        public InterruptMode InterruptMode { get; set; }
        public Colour BorderColour { get; set; }

        public bool Equals(CpuState other)
        {
            return other != null
                && AF == other.AF
                && BC == other.BC
                && DE == other.DE
                && HL == other.HL
                && IX == other.IX
                && IY == other.IY
                && PC == other.PC
                && SP == other.SP
                && AFAlt == other.AFAlt
                && BCAlt == other.BCAlt
                && DEAlt == other.DEAlt
                && HLAlt == other.HLAlt
                && I == other.I
                && R == other.R
                && IFF1 == other.IFF1
                && IFF2 == other.IFF2
                && InterruptMode == other.InterruptMode
                && BorderColour == other.BorderColour;
        }
    }
}
