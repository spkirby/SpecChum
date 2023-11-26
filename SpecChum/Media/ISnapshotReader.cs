using System.IO;

namespace SpecChum.Media
{
    public interface ISnapshotReader<T> where T : SnaSnapshot
    {
        T Read(byte[] image);
        T Read(Stream stream);
    }
}
