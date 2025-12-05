namespace Lab11_task2;

public interface IBackgroundWorker
{
    public string Name { get; }
    public WorkerStatus Status { get; }
    public bool IsCancellationRequested { get; }
    public void ReportProgress(int progress);
    public void RunWorkerAsync(string? name = null);
    public void Cancel();
}

public class WorkerCanceled : Exception;

public enum WorkerStatus
{
    NotStarted,
    IsRunning,
    Completed,
    Canceled,
    Failed
}
