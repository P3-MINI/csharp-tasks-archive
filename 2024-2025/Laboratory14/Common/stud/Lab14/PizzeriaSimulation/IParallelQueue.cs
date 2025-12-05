namespace PizzeriaSimulation;

/// <summary>
/// This interface should be implemented in STAGE01.
/// </summary>
public interface IParallelQueue<T>
{
	Task<bool> TryEnqueueAsync(T item, int timeoutMilliseconds, CancellationToken cancellationToken);
	Task<T?> TryDequeueAsync(int timeoutMilliseconds, CancellationToken cancellationToken);
	void Enqueue(T item);
	T Dequeue();
}