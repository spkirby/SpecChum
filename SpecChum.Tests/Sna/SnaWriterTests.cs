using SpecChum.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SpecChum.Tests.Sna
{
    public class SnaWriterTests
    {
        [Fact]
        public void WriteToArray_SuccessfullyWritesCpuState()
        {
            var snapshot = CreateTestSnapshot();
            var image = new SnaWriter().WriteToArray(snapshot);

            Assert.Equal(0x00, image[0]); // I
            Assert.Equal(0x01, image[1]); // L'
            Assert.Equal(0x02, image[2]); // H'
            Assert.Equal(0x03, image[3]); // E'
            Assert.Equal(0x04, image[4]); // D'
            Assert.Equal(0x05, image[5]); // C'
            Assert.Equal(0x06, image[6]); // B'
            Assert.Equal(0x07, image[7]); // F'
            Assert.Equal(0x08, image[8]); // A'
            Assert.Equal(0x09, image[9]); // L
            Assert.Equal(0x0a, image[10]); // H
            Assert.Equal(0x0b, image[11]); // E
            Assert.Equal(0x0c, image[12]); // D
            Assert.Equal(0x0d, image[13]); // C
            Assert.Equal(0x0e, image[14]); // B
            Assert.Equal(0x0f, image[15]); // IY Lo
            Assert.Equal(0x10, image[16]); // IY Hi
            Assert.Equal(0x11, image[17]); // IX Lo
            Assert.Equal(0x12, image[18]); // IX Hi
            Assert.Equal(0x04, image[19]); // IFF2
            Assert.Equal(0x13, image[20]); // R
            Assert.Equal(0x14, image[21]); // F
            Assert.Equal(0x15, image[22]); // A
            Assert.Equal(0xf8, image[23]); // SP Lo 
            Assert.Equal(0xff, image[24]); // SP Hi (0xfff8)
            Assert.Equal(0x01, image[25]); // IM
            Assert.Equal(0x05, image[26]); // Border
        }

        [Fact]
        public void WriteToArray_SuccessfullyWritesMemory()
        {
            var snapshot = CreateTestSnapshot();
            var image = new SnaWriter().WriteToArray(snapshot);
            var expectedMemory = CreateTestMemory();

            Assert.True(
                image.Skip(SnaSnapshot.HeaderLength).SequenceEqual(expectedMemory)
            );
        }

        [Fact]
        public void Write_SuccessfullyWritesCpuState()
        {
            var snapshot = CreateTestSnapshot();
            using var memoryStream = new MemoryStream();
            
            new SnaWriter().Write(snapshot, memoryStream);

            var image = memoryStream.ToArray();

            Assert.Equal(0x00, image[0]); // I
            Assert.Equal(0x01, image[1]); // L'
            Assert.Equal(0x02, image[2]); // H'
            Assert.Equal(0x03, image[3]); // E'
            Assert.Equal(0x04, image[4]); // D'
            Assert.Equal(0x05, image[5]); // C'
            Assert.Equal(0x06, image[6]); // B'
            Assert.Equal(0x07, image[7]); // F'
            Assert.Equal(0x08, image[8]); // A'
            Assert.Equal(0x09, image[9]); // L
            Assert.Equal(0x0a, image[10]); // H
            Assert.Equal(0x0b, image[11]); // E
            Assert.Equal(0x0c, image[12]); // D
            Assert.Equal(0x0d, image[13]); // C
            Assert.Equal(0x0e, image[14]); // B
            Assert.Equal(0x0f, image[15]); // IY Lo
            Assert.Equal(0x10, image[16]); // IY Hi
            Assert.Equal(0x11, image[17]); // IX Lo
            Assert.Equal(0x12, image[18]); // IX Hi
            Assert.Equal(0x04, image[19]); // IFF2
            Assert.Equal(0x13, image[20]); // R
            Assert.Equal(0x14, image[21]); // F
            Assert.Equal(0x15, image[22]); // A
            Assert.Equal(0xf8, image[23]); // SP Lo 
            Assert.Equal(0xff, image[24]); // SP Hi (0xfff8)
            Assert.Equal(0x01, image[25]); // IM
            Assert.Equal(0x05, image[26]); // Border
        }

        [Fact]
        public void Write_SuccessfullyWritesMemory()
        {
            var snapshot = CreateTestSnapshot();
            using var memoryStream = new MemoryStream();
            
            new SnaWriter().Write(snapshot, memoryStream);

            var image = memoryStream.ToArray();
            var expectedMemory = CreateTestMemory();

            Assert.True(
                image.Skip(SnaSnapshot.HeaderLength).SequenceEqual(expectedMemory)
            );
        }

        private SnaSnapshot CreateTestSnapshot()
        {
            var cpuState = new CpuState
            {
                I = 0x00,
                HLAlt = new Word(0x0201),
                DEAlt = new Word(0x0403),
                BCAlt = new Word(0x0605),
                AFAlt = new Word(0x0807),
                HL = new Word(0x0a09),
                DE = new Word(0x0c0b),
                BC = new Word(0x0e0d),
                IY = new Word(0x100f),
                IX = new Word(0x1211),
                IFF2 = true,
                R = 0x13,
                AF = new Word(0x1514),
                SP = new Word(0xfff8),
                InterruptMode = InterruptMode.IM1,
                BorderColour = Colour.Cyan,
                PC = new Word(0x1234)
            };

            var memory = CreateTestMemory();

            return new SnaSnapshot(cpuState, memory);
        }

        private byte[] CreateTestSna()
        {
            var snapshot = new byte[SnaSnapshot.SnapshotLength];
            Array.Fill<byte>(snapshot, 0xc9);

            snapshot[0] = 0x00; // I
            snapshot[1] = 0x01; // L'
            snapshot[2] = 0x02; // H'
            snapshot[3] = 0x03; // E'
            snapshot[4] = 0x04; // D'
            snapshot[5] = 0x05; // C'
            snapshot[6] = 0x06; // B'
            snapshot[7] = 0x07; // F'
            snapshot[8] = 0x08; // A'
            snapshot[9] = 0x09; // L
            snapshot[10] = 0x0a; // H
            snapshot[11] = 0x0b; // E
            snapshot[12] = 0x0c; // D
            snapshot[13] = 0x0d; // C
            snapshot[14] = 0x0e; // B
            snapshot[15] = 0x0f; // IY Lo
            snapshot[16] = 0x10; // IY Hi
            snapshot[17] = 0x11; // IX Lo
            snapshot[18] = 0x12; // IX Hi
            snapshot[19] = 0x04; // IFF2
            snapshot[20] = 0x13; // R
            snapshot[21] = 0x14; // F
            snapshot[22] = 0x15; // A
            snapshot[23] = 0xf8; // SP Lo 
            snapshot[24] = 0xff; // SP Hi (0xfff8)
            snapshot[25] = 0x01; // IM
            snapshot[26] = 0x05; // Border

            var memory = CreateTestMemory();
            memory.CopyTo(snapshot, SnaSnapshot.HeaderLength);

            return snapshot;
        }

        private byte[] CreateTestMemory()
        {
            var memory = new byte[SnaSnapshot.MemoryLength];

            // Fill display file
            for (var i = 0; i < 6144; i++)
            {
                memory[i] = 0b10101010;
            }

            // Program counter (0x1234)
            memory[0xbff8] = 0x34;
            memory[0xbff9] = 0x12;

            return memory;
        }
    }
}
