namespace PizzeriaSimulation;

/// <summary>
/// Main data structure for our simulation.
/// </summary>
public sealed class ParallelQueue<T>
	: IParallelQueue<T>, IPausableQueue<T>, IDisposable
	where T : class
{
	public ParallelQueue(int maxSize)
	{
		
	}

	public void Dispose()
	{
		// there is nothing to dispose of yet...
	}

	public T Dequeue()
	{
		throw new NotImplementedException();
	}

	public void Enqueue(T item)
	{
		throw new NotImplementedException();
	}

	public Task<T?> TryDequeueAsync(int timeoutMilliseconds, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<bool> TryEnqueueAsync(T item, int timeoutMilliseconds, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void PauseDequeue()
	{
		throw new NotImplementedException();
	}

	public void PauseEnqueue()
	{
		throw new NotImplementedException();
	}

	public void ResumeDequeue()
	{
		throw new NotImplementedException();
	}

	public void ResumeEnqueue()
	{
		throw new NotImplementedException();
	}
}