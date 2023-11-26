using SpecChum.Extensions;
using System.IO;

namespace SpecChum.Media
{
    public class SnaWriter : ISnapshotWriter<SnaSnapshot>
    {
        public void Write(SnaSnapshot snapshot, Stream stream)
        {
            stream.WriteByte(snapshot.Cpu.I);
            stream.WriteWord(snapshot.Cpu.HLAlt);
            stream.WriteWord(snapshot.Cpu.DEAlt);
            stream.WriteWord(snapshot.Cpu.BCAlt);
            stream.WriteWord(snapshot.Cpu.AFAlt);
            stream.WriteWord(snapshot.Cpu.HL);
            stream.WriteWord(snapshot.Cpu.DE);
            stream.WriteWord(snapshot.Cpu.BC);
            stream.WriteWord(snapshot.Cpu.IY);
            stream.WriteWord(snapshot.Cpu.IX);
            stream.WriteByte((byte)(snapshot.Cpu.IFF2 ? 0x04 : 0));
            stream.WriteByte(snapshot.Cpu.R);
            stream.WriteWord(snapshot.Cpu.AF);
            stream.WriteWord(snapshot.Cpu.SP);
            stream.WriteByte((byte)snapshot.Cpu.InterruptMode);
            stream.WriteByte((byte)snapshot.Cpu.BorderColour);
            stream.Write(snapshot.Memory, 0, SnaSnapshot.MemoryLength);
        }

        public byte[] WriteToArray(SnaSnapshot snapshot)
        {
            using (var output = new MemoryStream(SnaSnapshot.SnapshotLength))
            {
                Write(snapshot, output);
                return output.ToArray();
            }
        }
    }
}
