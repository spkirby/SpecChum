using SpecChum.Media;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace SpecChum.Tests.Sna
{
    public class SnaReaderTests
    {
        [Fact]
        public void Read_FromArray_ValidImage_ReadsCpuStateCorrectly()
        {
            var image = CreateTestSna();
            var snapshot = new SnaReader().Read(image);

            Assert.Equal(0x00, snapshot.Cpu.I);
            Assert.Equal(new Word(0x0201), snapshot.Cpu.HLAlt);
            Assert.Equal(new Word(0x0403), snapshot.Cpu.DEAlt);
            Assert.Equal(new Word(0x0605), snapshot.Cpu.BCAlt);
            Assert.Equal(new Word(0x0807), snapshot.Cpu.AFAlt);
            Assert.Equal(new Word(0x0a09), snapshot.Cpu.HL);
            Assert.Equal(new Word(0x0c0b), snapshot.Cpu.DE);
            Assert.Equal(new Word(0x0e0d), snapshot.Cpu.BC);
            Assert.Equal(new Word(0x100f), snapshot.Cpu.IY);
            Assert.Equal(new Word(0x1211), snapshot.Cpu.IX);
            Assert.True(snapshot.Cpu.IFF2);
            Assert.Equal(0x13, snapshot.Cpu.R);
            Assert.Equal(new Word(0x1514), snapshot.Cpu.AF);
            Assert.Equal(new Word(0xfff8), snapshot.Cpu.SP);
            Assert.Equal(InterruptMode.IM1, snapshot.Cpu.InterruptMode);
            Assert.Equal(Colour.Cyan, snapshot.Cpu.BorderColour);
            Assert.Equal(new Word(0x1234), snapshot.Cpu.PC);
        }

        [Fact]
        public void Read_FromArray_ValidImage_ReadsMemoryCorrectly()
        {
            var image = CreateTestSna();
            var snapshot = new SnaReader().Read(image);

            var expectedMemory = CreateTestMemory();

            Assert.True(expectedMemory.SequenceEqual(snapshot.Memory));
        }

        [Fact]
        public void Read_FromArray_InvalidImage_ThrowsException()
        {
            var image = new byte[10];
            var reader = new SnaReader();

            Assert.Throws<MediaReadException>(() =>
                reader.Read(image)
            );
        }

        [Fact]
        public void Read_FromStream_ValidImage_ReadsCpuStateCorrectly()
        {
            var image = CreateTestSna();
            SnaSnapshot snapshot;

            using (var memoryStream = new MemoryStream(image))
            {
                snapshot = new SnaReader().Read(memoryStream);
            }

            Assert.Equal(0x00, snapshot.Cpu.I);
            Assert.Equal(new Word(0x0201), snapshot.Cpu.HLAlt);
            Assert.Equal(new Word(0x0403), snapshot.Cpu.DEAlt);
            Assert.Equal(new Word(0x0605), snapshot.Cpu.BCAlt);
            Assert.Equal(new Word(0x0807), snapshot.Cpu.AFAlt);
            Assert.Equal(new Word(0x0a09), snapshot.Cpu.HL);
            Assert.Equal(new Word(0x0c0b), snapshot.Cpu.DE);
            Assert.Equal(new Word(0x0e0d), snapshot.Cpu.BC);
            Assert.Equal(new Word(0x100f), snapshot.Cpu.IY);
            Assert.Equal(new Word(0x1211), snapshot.Cpu.IX);
            Assert.True(snapshot.Cpu.IFF2);
            Assert.Equal(0x13, snapshot.Cpu.R);
            Assert.Equal(new Word(0x1514), snapshot.Cpu.AF);
            Assert.Equal(new Word(0xfff8), snapshot.Cpu.SP);
            Assert.Equal(InterruptMode.IM1, snapshot.Cpu.InterruptMode);
            Assert.Equal(Colour.Cyan, snapshot.Cpu.BorderColour);
            Assert.Equal(new Word(0x1234), snapshot.Cpu.PC);
        }

        [Fact]
        public void Read_FromStream_ValidImage_ReadsMemoryCorrectly()
        {
            var image = CreateTestSna();
            SnaSnapshot snapshot;

            using (var memoryStream = new MemoryStream(image))
            {
                snapshot = new SnaReader().Read(memoryStream);
            }
            
            var expectedMemory = CreateTestMemory();

            Assert.True(expectedMemory.SequenceEqual(snapshot.Memory));
        }

        [Fact]
        public void Read_FromStream_InvalidImage_ThrowsException()
        {
            var image = new byte[10];
            var reader = new SnaReader();
            using var memoryStream = new MemoryStream(image);

            Assert.Throws<MediaReadException>(() =>
                reader.Read(memoryStream)
            );
        }

        [Fact]
        public void Read_FromStream_ClosedStream_ThrowsException()
        {
            var image = CreateTestSna();
            var reader = new SnaReader();
            var memoryStream = new MemoryStream(image);
            memoryStream.Close();

            Assert.Throws<ObjectDisposedException>(() =>
                reader.Read(memoryStream)
            );
        }

        private byte[] CreateTestSna()
        {
            var snapshot = new byte[SnaSnapshot.SnapshotLength];
            Array.Fill<byte>(snapshot, 0xc9);

            snapshot[ 0] = 0x00; // I
            snapshot[ 1] = 0x01; // L'
            snapshot[ 2] = 0x02; // H'
            snapshot[ 3] = 0x03; // E'
            snapshot[ 4] = 0x04; // D'
            snapshot[ 5] = 0x05; // C'
            snapshot[ 6] = 0x06; // B'
            snapshot[ 7] = 0x07; // F'
            snapshot[ 8] = 0x08; // A'
            snapshot[ 9] = 0x09; // L
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
