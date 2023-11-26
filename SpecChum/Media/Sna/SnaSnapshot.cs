using System;
using System.Linq;

namespace SpecChum.Media
{
    public class SnaSnapshot : ISpectrumImage, IEquatable<SnaSnapshot>
    {
        public const int HeaderLength = 27;
        public const int MemoryStart = 16384;
        public const int MemoryLength = 1024 * 48;
        public const int SnapshotLength = HeaderLength + MemoryLength;

        public CpuState Cpu { get; }
        public byte[] Memory { get; }

        public SnaSnapshot()
        {
            Cpu = new CpuState();
            Memory = new byte[MemoryLength];
        }

        public SnaSnapshot(CpuState cpuState, byte[] memory)
        {
            Cpu = cpuState ?? throw new ArgumentNullException(nameof(cpuState));
            Memory = memory ?? throw new ArgumentNullException(nameof(memory));

            if (memory.Length != MemoryLength)
            {
                throw new ArgumentException($"Memory array must be {MemoryLength} bytes long");
            }
        }

        public bool Equals(SnaSnapshot other)
        {
            return other != null
                && Cpu.Equals(other.Cpu)
                && Memory.SequenceEqual(other.Memory);
        }
    }
}
