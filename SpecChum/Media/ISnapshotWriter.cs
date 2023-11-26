using System.IO;

namespace SpecChum.Media
{
    public interface ISnapshotWriter<T> where T : SnaSnapshot
    {
        byte[] WriteToArray(T snapshot);
        void Write(T snapshot, Stream stream);
    }
}
