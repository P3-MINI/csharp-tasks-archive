namespace MiniTest;

public static class Assert
{
    public static TException ThrowsException<TException>(Action action, string message = "") where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException ex)
        {
            return ex;
        }
        catch (Exception ex)
        {
            message = $"Expected exception type:<{typeof(TException)}>. Actual exception type:<{ex.GetType()}>. {message}";
            throw new AssertionException(message);
        }

        message = $"Expected exception type:<{typeof(TException)}> but no exception was thrown. {message}";
        throw new AssertionException(message);
    }

    public static void AreEqual<T>(T? expected, T? actual, string message = "")
    {
        IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
        if (comparer.Equals(expected, actual))
        {
            return;
        }
        
        message = $"Expected: {expected?.ToString() ?? "null"}. Actual: {actual?.ToString() ?? "null"}. {message}";
        throw new AssertionException(message);
    }
    
    public static void AreNotEqual<T>(T? notExpected, T? actual, string message = "")
    {
        IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
        if (!comparer.Equals(notExpected, actual))
        {
            return;
        }

        message = $"Expected any value except: {notExpected?.ToString() ?? "null"}. Actual: {actual?.ToString() ?? "null"}. {message}";
        throw new AssertionException(message);
    }

    public static void IsTrue(bool condition, string message = "")
    {
        if (!condition)
        {
            throw new AssertionException(message);
        }
    }
    
    public static void IsFalse(bool condition, string message = "")
    {
        if (condition)
        {
            throw new AssertionException(message);
        }
    }

    public static void Fail(string message = "")
    {
        throw new AssertionException(message);
    }
}