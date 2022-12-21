

using System.Runtime.Serialization;

namespace DO
{
    public class DalDoesNotExistException : Exception
    {
        public DalDoesNotExistException(string? message) : base(message) { }
    }

    public class DalAlreadyExistException : Exception
    {
        public DalAlreadyExistException(string? message) : base(message) { }
    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}
