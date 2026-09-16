namespace SharpArgs.Exceptions;

/// <summary>
/// Base class for all argument exceptions in the library.
/// </summary>
public class SharpArgsException : Exception
{
    public SharpArgsException() { }

    public SharpArgsException(string message) 
        : base(message) { }

    public SharpArgsException(string message, Exception inner) 
        : base(message, inner) { }
}