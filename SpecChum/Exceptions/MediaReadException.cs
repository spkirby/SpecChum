using System;
using System.Runtime.Serialization;

namespace SpecChum
{
    public class MediaReadException : Exception
    {
        public MediaReadException()
        {
        }

        public MediaReadException(string message) : base(message)
        {
        }

        public MediaReadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MediaReadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
