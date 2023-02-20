
namespace Simulator
{
    public class SimDoesNotExistException : Exception
    {
        public SimDoesNotExistException() { }
        public SimDoesNotExistException(string? message) : base(message) { }
    }
}
