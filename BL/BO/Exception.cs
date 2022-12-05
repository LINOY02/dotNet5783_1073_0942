
namespace BO
{
    public class DalDoesNotExistException : Exception
    {
        public DalDoesNotExistException() { }
        public DalDoesNotExistException(string? message) : base(message) { }
    }

    public class DalAlreadyExistException : Exception
    {
        public DalAlreadyExistException() { }
        public DalAlreadyExistException(string? message) : base(message) { }
    }
    public class WrongValue : Exception
    {
        public WrongValue() { }
        public WrongValue(string? message) : base(message) { }
    }
}
