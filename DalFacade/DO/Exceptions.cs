

using System.Runtime.Serialization;

namespace DO
{
    public class ExistException : Exception
     {
        public ExistException() : base() { }
        public ExistException(string message) : base(message) { }
        public ExistException(string message, Exception inner) : base(message, inner) { }
        protected ExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() => "The ID is already exist";
    }

    public class NotExistException : Exception
    {
        public NotExistException() : base() { }
        public NotExistException(string message) : base(message) { }
        public NotExistException(string message, Exception inner) : base(message, inner) { }
        protected NotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() => "The ID is not exist";
    }
}
