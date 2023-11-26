using SpecChum.Extensions;
using System.IO;

namespace SpecChum.Media
{
    public class SnaReader : ISnapshotReader<SnaSnapshot>
    {
        public SnaSnapshot Read(byte[] image)
        {
            using (var memoryStream = new MemoryStream(image))
            {
                return Read(memoryStream);
            }
        }

        public SnaSnapshot Read(Stream stream)
        {
            var cpuState = new CpuState
            {
                I = stream.ReadByteOrThrow(),
                HLAlt = stream.ReadWord(),
                DEAlt = stream.ReadWord(),
                BCAlt = stream.ReadWord(),
                AFAlt = stream.ReadWord(),
                HL = stream.ReadWord(),
                DE = stream.ReadWord(),
                BC = stream.ReadWord(),
                IY = stream.ReadWord(),
                IX = stream.ReadWord(),
                IFF2 = stream.ReadByteOrThrow().IsBitSet(2),
                R = stream.ReadByteOrThrow(),
                AF = stream.ReadWord(),
                SP = stream.ReadWord(),
                InterruptMode = (InterruptMode)stream.ReadByteOrThrow(),
                BorderColour = (Colour)stream.ReadByteOrThrow()
            };

            if (!Enum<InterruptMode>.IsDefined(cpuState.InterruptMode))
            {
                throw new MediaReadException("Invalid interrupt mode: " + cpuState.InterruptMode);
            }
            
            if (!Enum<Colour>.IsDefined(cpuState.BorderColour))
            {
                throw new MediaReadException("Invalid border colour: " + cpuState.BorderColour);
            }

            var memory = new byte[SnaSnapshot.MemoryLength];
            var bytesRead = stream.Read(memory, 0, SnaSnapshot.MemoryLength);

            if (bytesRead != SnaSnapshot.MemoryLength)
            {
                throw new MediaReadException($"SNA image must be {SnaSnapshot.SnapshotLength} bytes long");
            }

            cpuState.PC = memory.GetWord(cpuState.SP - SnaSnapshot.MemoryStart);

            return new SnaSnapshot(cpuState, memory);
        }
    }
}
