using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;

namespace Quack.Engine.Extra;

public class GLSynchronizationContext : SynchronizationContext
{
    private readonly ConcurrentQueue<(SendOrPostCallback callback, object? state)> _queue = new();
    private readonly int _mainThreadId = Environment.CurrentManagedThreadId;

    public override void Post(SendOrPostCallback callback, object? state)
    {
        _queue.Enqueue((callback, state));
    }

    public override void Send(SendOrPostCallback callback, object? state)
    {
        if (Environment.CurrentManagedThreadId == _mainThreadId)
        {
            callback(state);
            return;
        }

        using var handle = new ManualResetEventSlim(false);
        var context = new SendContext(callback, state, handle);

        Post(ExecuteSend, context);

        handle.Wait();

        if (context.CaughtException != null)
        {
            ExceptionDispatchInfo.Capture(context.CaughtException).Throw();
        }
    }

    private static void ExecuteSend(object? state)
    {
        if (state is not SendContext context)
        {
            return;
        }

        try
        {
            context.Callback(context.State);
        }
        catch (Exception ex)
        {
            context.CaughtException = ex;
        }
        finally
        {
            context.Handle.Set();
        }
    }

    public void Update(double maxMilliseconds = 10.0)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        while (stopwatch.Elapsed.TotalMilliseconds < maxMilliseconds && _queue.TryDequeue(out var workItem))
        {
            workItem.callback(workItem.state);
        }
    }

    public override SynchronizationContext CreateCopy()
    {
        return this;
    }

    private class SendContext(SendOrPostCallback callback, object? state, ManualResetEventSlim handle)
    {
        public readonly SendOrPostCallback Callback = callback;
        public readonly object? State = state;
        public readonly ManualResetEventSlim Handle = handle;
        public Exception? CaughtException;
    }
}