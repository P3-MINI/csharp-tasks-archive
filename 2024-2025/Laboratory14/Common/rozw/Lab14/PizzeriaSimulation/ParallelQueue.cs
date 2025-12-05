namespace PizzeriaSimulation;

public sealed class ParallelQueue<T>
	: IParallelQueue<T>, IPausableQueue<T>, IDisposable
	where T : class
{
	private readonly List<T> _queue = [];
	private readonly SemaphoreSlim _itemsSemaphore;
	private readonly SemaphoreSlim _spacesSemaphore;

	private readonly ManualResetEventSlim _enqueuePauseEvent = new(true);
	private readonly ManualResetEventSlim _dequeuePauseEvent = new(true);

	private readonly object _lockObject = new();

	public ParallelQueue(int maxSize)
	{
		_itemsSemaphore = new SemaphoreSlim(0, maxSize);
		_spacesSemaphore = new SemaphoreSlim(maxSize, maxSize);
	}

	public async Task<bool> TryEnqueueAsync(T item, int timeoutMilliseconds, CancellationToken cancellationToken)
	{
		if (!await _spacesSemaphore.WaitAsync(timeoutMilliseconds, cancellationToken))
		{
			Console.WriteLine("Timeout or cancellation during enqueue operation.");
			return false;
		}

		_enqueuePauseEvent.Wait(cancellationToken);

		lock (_lockObject)
		{
			_queue.Add(item);
			Console.WriteLine($"Element enqueued: {item}");
		}

		_itemsSemaphore.Release();
		return true;
	}

	public async Task<T?> TryDequeueAsync(int timeoutMilliseconds, CancellationToken cancellationToken)
	{
		if (!await _itemsSemaphore.WaitAsync(timeoutMilliseconds, cancellationToken))
		{
			Console.WriteLine("Timeout or cancellation during dequeue operation.");
			return null;
		}

		_dequeuePauseEvent.Wait(cancellationToken);

		T? item;

		lock (_lockObject)
		{
			item = _queue[0];
			_queue.RemoveAt(0);
			Console.WriteLine($"Element dequeued: {item}");
		}

		_spacesSemaphore.Release();
		return item;
	}

	public T Dequeue()
	{
		_itemsSemaphore.Wait();

		_dequeuePauseEvent.Wait();

		T item;

		lock (_lockObject)
		{
			item = _queue[0];
			_queue.RemoveAt(0);
			Console.WriteLine($"Element dequeued: {item}");
		}

		_spacesSemaphore.Release();
		return item;
	}

	public void Enqueue(T item)
	{
		_spacesSemaphore.Wait();

		_enqueuePauseEvent.Wait();

		lock (_lockObject)
		{
			_queue.Add(item);
			Console.WriteLine($"Element enqueued: {item}");
		}

		_itemsSemaphore.Release();
	}

	public void Dispose()
	{
		_itemsSemaphore.Dispose();
		_spacesSemaphore.Dispose();
	}

	public void PauseEnqueue()
	{
		_enqueuePauseEvent.Reset();
		Console.WriteLine("Enqueue operations are paused.");
	}

	public void ResumeEnqueue()
	{
		_enqueuePauseEvent.Set();
		Console.WriteLine("Enqueue operations are resumed.");
	}

	public void PauseDequeue()
	{
		_dequeuePauseEvent.Reset();
		Console.WriteLine("Dequeue operations are paused.");
	}

	public void ResumeDequeue()
	{
		_dequeuePauseEvent.Set();
		Console.WriteLine("Dequeue operations are resumed.");
	}
}