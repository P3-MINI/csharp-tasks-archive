namespace PizzeriaSimulation;

public interface IPausableQueue<T>
{
	void PauseEnqueue();
	void PauseDequeue();
	void ResumeEnqueue();
	void ResumeDequeue();
}