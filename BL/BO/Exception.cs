
namespace BO
{
    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException() { }
        public BlDoesNotExistException(string? message) : base(message) { }
    }

    public class BlAlreadyExistException : Exception
    {
        public BlAlreadyExistException() { }
        public BlAlreadyExistException(string? message) : base(message) { }
    }
    public class BlInvalidInputException : Exception
    {
        public BlInvalidInputException() { }
        public BlInvalidInputException(string? message) : base(message) { }
    }
    public class BlProductIsOrderedException : Exception
    {
        public BlProductIsOrderedException() { }
        public BlProductIsOrderedException(string? message) : base(message) { }
    }
    public class BlProductIsNotOrderedException : Exception
    {
        public BlProductIsNotOrderedException() { }
        public BlProductIsNotOrderedException(string? message) : base(message) { }
    }

    public class BlOutOfStockException : Exception
    {
        public BlOutOfStockException() { }
        public BlOutOfStockException(string? message) : base(message) { }
    }

    public class BlStatusAlreadyUpdateException : Exception
    {
        public BlStatusAlreadyUpdateException() { }
        public BlStatusAlreadyUpdateException(string? message) : base(message) { }
    }

    public class BlMissingInputException : Exception
    {
        public BlMissingInputException() { }
        public BlMissingInputException(string? message) : base(message) { }
    }
}
