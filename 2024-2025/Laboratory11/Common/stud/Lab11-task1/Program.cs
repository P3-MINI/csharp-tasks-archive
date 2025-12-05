namespace Lab11_task1;

class Program
{
    static string FormatThread(Thread t) => $"[{t.ManagedThreadId}] `{t.Name}`";

    // TODO: Add names to both threads
    static void Main()
    {
        // Create a new thread, you can use lambda or pass a function
        Thread newThread = new Thread(() => {
            Console.WriteLine($"Hello from the new thread! {FormatThread(Thread.CurrentThread)}");
            Thread.Sleep(3000);  // Simulates some work
        });
        // Start the thread
        newThread.Start();

        Console.WriteLine($"Hello from the main thread! {FormatThread(Thread.CurrentThread)}");

        // Optionally, wait for the thread to finish
        newThread.Join();
        Console.WriteLine($"Thread joined: {FormatThread(newThread)}");
    }
}