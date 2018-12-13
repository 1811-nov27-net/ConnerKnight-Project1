using System;
using System.Runtime.Serialization;

namespace Project0.Library
{
    public class BadOrderException : Exception
    {
        public BadOrderException()
        {
        }

        public BadOrderException(string message) : base(message)
        {
        }

        public BadOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
