# Programming 3 - Advanced
## Laboratory 11 - Thread, Task, Async, Await and Parallel

### Introduction
Before we start writing a code, here is a short explanation of today's topics.
#### Thread
A thread in .NET is a fundamental unit of CPU utilization consisting of an instruction pointer, a stack, and a set of processor registers. .NET applications start with a single thread, known as the main thread, but can create additional threads.
- Usage: Threads are used to perform operations in parallel, particularly when those operations involve waiting or performing CPU-bound tasks.
- Control: Creating and managing threads manually (using the Thread class in .NET) gives you a lot of control but requires careful handling of thread safety, synchronization, and resource management.

#### Task
Introduced in .NET Framework 4.0, the `Task` class represents an asynchronous operation. `Task` is more abstract than a thread and does not necessarily correspond to a new thread in the operating system.
- Usage: Tasks can be used to make your application more responsive, particularly for I/O-bound operations but also for CPU-bound operations by leveraging the thread pool efficiently.
- Features: Tasks support cancellation, exception handling, chaining (continuations), and more. They are typically used with async programming models and can return results.

#### Async and Await
The `async` and `await` keywords are used in C# to simplify the process of writing asynchronous code, which can help keep your application responsive.
- `async` keyword: Used to mark a method that performs asynchronous operations. An `async` method can contain `await` expressions and will usually return `Task` or `Task<T>`.
- `await` keyword: Used before a call to an asynchronous method that returns a `Task` or `Task<T>`. This keyword yields control to the caller until the awaited task is completed, at which point it resumes the async method.

#### Parallel
The `Parallel` class in .NET provides support for parallel execution by using multiple threads, typically from the thread pool. It includes methods for parallel loops (e.g., `Parallel.For` and `Parallel.ForEach`) and parallel LINQ (PLINQ).
- Usage: Ideal for performing CPU-bound operations across multiple threads. It abstracts the complexity of threading and provides a simple model for running tasks concurrently.


### What are Threads in .NET?
Threads in .NET are the smallest units of execution that are managed independently by the scheduler of the operating system. A .NET application starts with a single thread, known as the main thread, but can create additional threads to execute code in parallel. These threads are managed by the .NET runtime, which provides a higher-level abstraction over the native OS threads.

.NET threads work by utilizing the underlying operating system's threading infrastructure (like Windows threads in the case of a Windows-based .NET application). The .NET runtime uses the Thread class from the `System.Threading` namespace to create and manage threads. The runtime schedules these threads onto available cores of the CPU, allowing tasks to be carried out concurrently.

.NET threads are essentially wrappers around the native OS threads, but they have additional features provided by the .NET runtime, such as exception handling and garbage collection. The key differences in comparison to C and C++ threads include:
- Management and Scheduling: .NET runtime provides garbage collection and exception handling, which are not directly managed by OS threads.
- Ease of Use: .NET abstracts many of the complexities of thread creation and synchronization that are typically handled manually with OS threads.

#### Key Properties of the Thread Class
- `IsAlive`: This property checks whether the thread is still running. It returns true if the thread has been started and has not yet been terminated.
- `Name`: This is a string property that can be set to identify the thread, which can be useful for debugging purposes.
- `Priority`: This property sets the scheduling priority of the thread relative to other threads in the operating system.
- `CurrentThread`: This static property retrieves the current thread, which is running the code.
- `ThreadState`: This property gets the current lifecycle state of the thread, such as Running, Stopped, WaitSleepJoin, etc.

#### Key Methods of the Thread Class
- `Start()`: This method starts the execution of a new thread. Once called, the thread begins executing the method that was passed to its constructor.
- `Join()`: This method blocks the calling thread until the thread represented by this instance terminates. Overloads of Join() allow for the specification of a timeout.
- `Abort()`: Used in older .NET versions to stop the thread forcibly. It is now deprecated and not recommended due to its unsafe nature, potentially leaving resources or a shared state in an undefined condition.
- `Interrupt()`: This method interrupts a thread that is in the WaitSleepJoin thread state.
- `Sleep()`: This static method suspends the current thread for a specified time or until a specified time period has elapsed, thus releasing the CPU to allow other threads to execute.

#### TASK 1: Creating and Starting Threads
Here is a simple example to illustrate the creation and starting of a thread using the Thread class:
```csharp
class Program
{
    static string FormatThread(Thread t) => $"[{t.ManagedThreadId}] `{t.Name}`";

    static void Main()
    {
        // Create a new thread; you can use lambda or pass a function
        Thread newThread = new Thread(() => {
            Console.WriteLine($"Hello from the new thread! {FormatThread(Thread.CurrentThread)}");
            Thread.Sleep(3000);  // Simulates some work
        });

        // Start the thread
        newThread.Start();

        Console.WriteLine($"Hello from the main thread! {FormatThread(Thread.CurrentThread)}");

        // Optionally, wait for the thread to finish
        newThread.Join();
        Console.WriteLine($"Thread joined: {FormatThread(newThread)}");
    }
}
```
Use your debugger to see what's happening inside this program. Look at the 'Threads' tab. Add Names to threads to get proper names in the output.

NOTE: The name "Main Thread" that you see in the Visual Studio debugger is merely a convenience provided by Visual Studio and does not reflect the actual `Thread.Name` property unless you have precisely set it in your code.

NOTE2: In addition to our two threads, there are other threads created by .NET.

#### TASK 2: Simple Background Worker
Use the `Thread` class to create a `SimpleBackgroundWorker<T>` (where `T` is the result type), which implements the provided `IBackgroundWorker` interface.
- Add the possibility of naming worker threads while running it. If not provided, the thread should have the name `Thread_{TID}`.
- Use `CancellationTokenSource` to properly request thread cancellation.
- See how defining the thread as a background thread changes the app termination behavior

<!-- TODO: Add more task 2 description -->

#### Thread synchronization
Thread synchronization is essential when multiple threads need to access shared resources or coordinate their execution. Without proper synchronization, race conditions, deadlocks, or inconsistent states may occur. More low-level technics will be covered in Operating System 1 & 2 courses. 
The .NET Framework provides several tools and mechanisms to achieve thread synchronization, including ManualResetEvent, UI thread synchronization (Invoke), and SynchronizationContext. Here’s an overview of these key concepts:

##### ManualResetEvent
ManualResetEvent is a synchronization primitive that allows threads to signal one another. It works like a "barrier" that threads can wait on and pass through when it's signaled.

How it works:
- The ManualResetEvent class has two states: signaled and unsignaled.
- When signaled (`Set` method is called), all waiting threads are released, and the event remains in the signaled state until explicitly reset (`Reset` method).
- When unsignaled, threads calling `WaitOne` are blocked until the event is set.

Example Usage:
```csharp
ManualResetEvent manualResetEvent = new ManualResetEvent(false);

void WorkerThread()
{
    Console.WriteLine("Worker thread waiting...");
    manualResetEvent.WaitOne(); // Wait until signaled
    Console.WriteLine("Worker thread proceeding.");
}

void MainThread()
{
    Console.WriteLine("Main thread doing work...");
    Thread.Sleep(1000); // Simulate work
    manualResetEvent.Set(); // Signal the worker thread
}
```

##### Working with UI Frameworks
In UI frameworks such as WinForms and WPF (which will be covered in next semester's Programming in Graphical Environment course), the UI thread is responsible for rendering and responding to user input. Blocking such thread is not allowed, and all heavy calculations must be performed on a separate thread. Accessing or updating UI elements from a background thread can cause thread-affinity issues because UI frameworks are not thread-safe.

<!-- TODO: Add some examples, Invoke , Dispatcher  -->

##### SynchronizationContext
`SynchronizationContext` class is an abstraction for propagating thread-specific context, such as a UI or custom thread, across asynchronous operations.

How it works:
- A SynchronizationContext captures the environment in which a thread runs.
- It provides the `Post` and `Send` methods to execute code asynchronously or synchronously, ensuring execution occurs in the appropriate context.

```csharp
SynchronizationContext context = SynchronizationContext.Current;

Task.Run(() =>
{
    // Perform background work
    string result = "Background thread result";

    // Update UI context
    context.Post(_ => 
    {
        myLabel.Text = result;
    }, null);
});
```

#### Thread Pools
When you start a new thread, usually, several hundred microseconds are used to set up resources like a fresh local variable stack. A thread pool minimizes this overhead by maintaining a collection of pre-created, reusable threads. This approach is crucial for efficient parallel programming and fine-grained concurrency, as it enables short operations to execute without incurring the significant cost of thread initialization.

There are a few limitations of thread pools:
- You cannot set a `Name` for the pooled thread. Instead, you can attach a description of when debugging in Visual Studio's Threads window.
- Pooled threads are always background threads.
- You need to be careful when blocking pooled threads.

How to use a pooled thread:
```csharp
// Task is in System.Threading.Tasks
Task.Run (() => Console.WriteLine ("Hello from the thread pool"));
```

#### Useful links:

- [Thread Class](https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread?view=net-8.0)
- [Thread.CurrentThread Property](https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.currentthread?view=net-8.0)
- [Using threads and threading](https://learn.microsoft.com/en-us/dotnet/standard/threading/using-threads-and-threading)
- [Managed threading best practices](https://learn.microsoft.com/en-us/dotnet/standard/threading/managed-threading-best-practices)

### What is a `Task`?
A `Task` represents an asynchronous operation that may return a result. Unlike traditional threads, tasks are higher-level abstractions that work with thread pools under the hood (mentioned earlier `Task.Run` method). This allows the runtime to optimize thread usage and reduce overhead compared to manually managing threads.

Tasks can be used for:
- Asynchronous computation.
- Parallel execution of multiple operations.
- Coordinating workflows with continuations and chaining.

#### Returning a Value with `Task<T>`
A `Task<T>` represents a task that returns a value. This is useful for asynchronous computations.

```csharp
Task<int> task = Task.Run(() =>
{
    Thread.Sleep(3000); // Simulate some computation
    return 42;
});

int result = task.Result; // Blocking call to get the result
Console.WriteLine($"Result: {result}");
```

### Asynchronous Programming with `async` and `await`
The `async` and `await` keywords simplify working with tasks. Using these keywords, you can write asynchronous code that reads like synchronous code.

```csharp
async Task<string> FetchDataAsync()
{
    await Task.Delay(1000); // Simulate getting data
    return "Data fetched!";
}

async Task Main()
{
    string data = await FetchDataAsync();
    Console.WriteLine(data);
}
```

#### Chaining Tasks
Tasks can be chained using continuations with the ContinueWith method or by leveraging `await`.

Using ContinueWith:
```csharp
Task initialTask = Task.Run(() => Console.WriteLine("First task"));
Task continuation = initialTask.ContinueWith(t => Console.WriteLine("Continuation task"));
```

Using await for Chaining:
```csharp
async Task ProcessDataAsync()
{
    string data = await FetchDataAsync();
    Console.WriteLine($"Processing: {data}");
}
```

#### Waiting for Tasks
Sometimes, you may need to wait for a task to complete before proceeding. The `Wait` method or `await` keyword can be used.

Using Wait:
```csharp
Task task = Task.Run(() => Console.WriteLine("Task running..."));
task.Wait(); // Block until the task completes
```

Waiting for Multiple Tasks:
You can use `Task.WhenAll` or `Task.WhenAny` to wait for multiple tasks.

- `Task.WhenAll`: Waits for all tasks to complete.
```csharp
Task[] tasks = { Task.Delay(1000), Task.Delay(2000) };
await Task.WhenAll(tasks);
Console.WriteLine("All tasks completed.");
```
- `Task.WhenAny`: Waits for any one task to be completed.
```csharp
Task firstCompleted = await Task.WhenAny(tasks);
Console.WriteLine("First task completed.");
```

#### Handling Exceptions in Tasks
Tasks use an `AggregateException` to encapsulate exceptions that occur during execution. The `await` keyword simplifies exception handling by re-throwing the actual exception.

With `Wait` or `Result`:
```csharp
try
{
    Task task = Task.Run(() => throw new InvalidOperationException("Error occurred."));
    task.Wait();
}
catch (AggregateException ex)
{
    Console.WriteLine($"Exception: {ex.InnerException.Message}");
}
```
With `async`/`await`:
```csharp
try
{
    await Task.Run(() => throw new InvalidOperationException("Error occurred."));
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
}
```

#### Best Practices
- Use `async`/`await`: Prefer `async`/`await` over manually managing continuations for cleaner and more maintainable code.
- Avoid Blocking with `Wait` or `Result`: These methods can cause deadlocks in UI applications. Use `await` instead.
- Use Cancellation Tokens: Always integrate cancellation tokens in long-running tasks to provide users with the ability to cancel operations.
- Error Handling: Always handle exceptions in tasks using `try-catch` blocks or `AggregateException`.
- Minimize Overhead: Avoid creating too many short-lived tasks. Use batching or other techniques to reduce task management overhead.

#### TASK 3: 
Base on single thread implementation of rendering Mandelbrot set create two more:
- `GenerateFractalMultiThread` which is using `Thread` class
- `GenerateFractalWithTasks` which is using `Task` class

### What is a `Parallel`?
The `Parallel` class in .NET is another part of the Task Parallel Library (TPL) and provides a straightforward way to execute tasks in parallel. Designed for scenarios where tasks can run concurrently without dependencies, the `Parallel` class allows developers to utilize multi-core processors efficiently, improving performance for computationally intensive operations.

The `Parallel` class resides in the `System.Threading.Tasks` namespace and provides static methods to execute loops or invoke actions concurrently. Unlike traditional threading, the `Parallel` class abstracts thread management, making it simpler to write parallel code without dealing directly with threads or thread pools.

#### Key Features
- Parallel Loops: The `Parallel` class supports parallel execution of loops using `Parallel.For` and `Parallel.ForEach`.
- Task Partitioning: Automatically partitions tasks across available processors for optimal performance.
- Built-In Load Balancing: Adjusts workloads dynamically to balance execution across cores.
- Cancellation and Error Handling: Supports cancellation using `CancellationToken` and propagates exceptions for robust error handling.

#### `Parallel.For`
Executes a for loop where iterations can run in parallel.

```csharp
Parallel.For(0, 10, i =>
{
    Console.WriteLine($"Processing index {i} on thread {Thread.CurrentThread.ManagedThreadId}");
});
```

#### `Parallel.ForEach`
Executes iterations over a collection in parallel.

```csharp
var data = new[] { "A", "B", "C", "D" };
Parallel.ForEach(data, item =>
{
    Console.WriteLine($"Processing {item} on thread {Thread.CurrentThread.ManagedThreadId}");
});
```

#### `Parallel.Invoke`
Executes multiple independent actions in parallel.

```csharp
Parallel.Invoke(
    () => Console.WriteLine("Action 1"),
    () => Console.WriteLine("Action 2"),
    () => Console.WriteLine("Action 3")
);
```

#### Cancellation
Parallel methods support `CancellationToken` for gracefully stopping operations:

```csharp
var cts = new CancellationTokenSource();
try
{
    Parallel.For(0, 100, new ParallelOptions { CancellationToken = cts.Token }, i =>
    {
        if (i == 50) cts.Cancel(); // Simulate cancellation
        Console.WriteLine(i);
    });
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was canceled.");
}
```

#### Controlling Parallelism
The `ParallelOptions` class allows you to control the degree of parallelism:

```csharp
Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 4 }, i =>
{
    Console.WriteLine($"Processing {i} with limited parallelism.");
});
```

#### TASK 4:
Add implementation of rendering Mandelbrot set using `Parallel` class.

<!-- 
I/O-bound

spinning 


The CLR assigns each thread its own memory stack so that local variables
are kept separate.

lock
static readonly object _locker = new object();
exception inside thread

Foreground Versus Background Threads
Thread Priority
ManualResetEvent

UI Invoke

SynchronizationContext


The Thread Pool


Task.Run
  -->