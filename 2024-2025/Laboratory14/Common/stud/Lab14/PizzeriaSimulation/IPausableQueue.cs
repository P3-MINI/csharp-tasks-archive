namespace PizzeriaSimulation;

/// <summary>
/// This interface should be implemented in STAGE02.
/// </summary>
public interface IPausableQueue<T>
{
	void PauseEnqueue();
	void PauseDequeue();
	void ResumeEnqueue();
	void ResumeDequeue();
}