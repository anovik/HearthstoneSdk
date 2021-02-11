using System;
using System.Runtime.Serialization;


namespace HearthstoneSdk
{
    [Serializable()]
    public class HearthstoneSdkException : Exception, ISerializable
    {
        public HearthstoneSdkException() : base() { }
        public HearthstoneSdkException(string message) : base(message) { }
        public HearthstoneSdkException(string message, Exception inner) : base(message, inner) { }
        public HearthstoneSdkException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
