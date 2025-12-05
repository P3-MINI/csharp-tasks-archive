namespace Lab11_task2;

public class SimpleBackgroundWorker<T> : IBackgroundWorker
{
    private Thread? _thread;
    private WorkerStatus _status = WorkerStatus.NotStarted;
    private Func<IBackgroundWorker, T>? _doWorkAction;
    private CancellationTokenSource? _cts;

    public event EventHandler<int>? ProgressChanged;
    public event EventHandler? WorkerStatusChanged;

    public bool IsBusy => _status == WorkerStatus.IsRunning;
    public WorkerStatus Status { get { return _status; } }
    public string Name { get => _thread?.Name ?? $"Thread_{_thread?.ManagedThreadId}"; }
    public bool IsCancellationRequested { get => _cts?.IsCancellationRequested ?? false; }

    public void SetDoWorkAction(Func<IBackgroundWorker, T> doWorkAction)
    {
        _doWorkAction = doWorkAction;
    }
    public void RunWorkerAsync(string? name = null)
    {
        if (_doWorkAction == null)
        {
            throw new InvalidOperationException("No work action specified. Use SetDoWorkAction before calling RunWorkerAsync.");
        }

        if (_status == WorkerStatus.IsRunning)
        {
            throw new InvalidOperationException($"The worker `{Name}` is already running.");
        }

        _status = WorkerStatus.IsRunning;
        _cts = new CancellationTokenSource();

        _thread = new Thread(() =>
        {
            try
            {
                _doWorkAction(this);
                OnRunWorkerCompleted();
            }
            catch (WorkerCanceled)
            {
                OnRunWorkerCanceled();
            }
            catch (Exception e)
            {
                OnRunWorkerFailed(e);
            }
        });

        if (name != null) _thread.Name = name;

        _thread.IsBackground = true;
        _thread.Start();
    }

    public void ReportProgress(int progress)
    {
        ProgressChanged?.Invoke(this, progress);
    }

    protected virtual void OnRunWorkerCompleted()
    {
        _status = WorkerStatus.Completed;
        WorkerStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnRunWorkerCanceled()
    {
        _status = WorkerStatus.Canceled;
        WorkerStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnRunWorkerFailed(Exception e)
    {
        _status = WorkerStatus.Failed;
        WorkerStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Cancel()
    {
        if (_status == WorkerStatus.IsRunning && _cts != null)
        {
            _cts.Cancel();
        }
    }
}