# Lab05 - Asynchronous Programming and Parallel Processing

## Simulation of _[Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)_ – Responsive UI and Parallel Computations

The goal of this task is to complete fragments of the logic for a desktop application that simulates the cellular automaton _"Game of Life"_.
The main focus of our application is not graphics itself, but rather application performance and responsiveness.

> Description of elements that make up the desktop application.
> Everything mentioned here is already implemented.
> Your "part" begins in **Stage 1**.

The application can be divided into 3 parts:

- Left panel: This area is used to control input data.
  - `Choose directory...` button: Opens a system folder selection dialog.
  - Progress bar: Shows the progress of asynchronous file scanning.
  - File list: Displays files found in the folder. Selecting an item from this list immediately stops the current simulation and starts a new one for the selected file.
- Simulation area: Main part of the screen where the game is rendered.
- Bottom status bar: This area is used to monitor performance and control the simulation progress in real time.
  - Status: Displays messages: `Choose directory...`, `Scanning directory...`, `Done.` and `Simulation: <filename>`.
  - Speed slider: Allows you to dynamically change the delay between generations without restarting the simulation.
- Statistics:
  - `Generation`: Number of the current simulation step.
  - `Living cells`: Number of living cells (algorithm correctness test).
  - `Elapsed`: Time of the last generation calculation in milliseconds.

## Useful Links:

- [Microsoft Learn: Directory.EnumerateFiles Method](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-10.0)
- [Microsoft Learn: CancellationTokenSource.IsCancellationRequested Property](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.iscancellationrequested?view=net-10.0)
- [Microsoft Learn: File.ReadAllLinesAsync Method](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-9.0)
- [Microsoft Learn: Task cancellation](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation)
- [Microsoft Learn: Parallel.For Method](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.for?view=net-10.0)
- [Microsoft Learn: Interlocked.Add](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.add?view=net-9.0)
- [Microsoft Learn: Stopwatch Class](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?view=net-10.0)
- [Microsoft Learn: Progress<T> Class](https://learn.microsoft.com/en-us/dotnet/api/system.progress-1?view=net-9.0)
- [Microsoft Learn: Task.Run Method](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.run?view=net-9.0)
- [Microsoft Learn: TaskCanceledException Class](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcanceledexception?view=net-9.0)

## Stage 1: Asynchronous Loading of Examples

The application must load boards from files without "freezing" the user interface.

Open the file `Services/FileService.cs`.

1. Method `EnumerateFilesAsync`: (**2 pts.**)
   - Return paths to text files located directly in the `folderPath` folder as `IAsyncEnumerable<string>`.
   - Use `Directory.EnumerateFiles` to get a list of `.txt` files.
   - Handle `CancellationToken` – if cancellation is requested, break the loop.
   - Before returning each path, add a delay of `100` milliseconds.
2. Method `LoadBoardAsync`: (**1 pt.**)
   - Asynchronously load all lines from the file `filePath`.
   - Return a filled two-dimensional array `bool[,]` of size `rows × cols`.
   - Character `0` in a content line corresponds to a living cell (value `true` in the array). Character `\n` starts a new row.
   - Any other character corresponds to a dead cell (value `false` in the array).
   - **Note:** The file may contain fewer or more lines/characters per line than `rows` or `cols`. Fill the array starting from the top-left corner. We assume that all remaining cells are dead.

Exemplary contents of the input file:

```txt
.....
...0.
.0.0.
..00.
.....
```

### Testing:

Click the `Choose directory...` button and select a folder with examples.
The list on the left side of the window should fill with the contents of the selected folder.

## Stage 2: Game Engine and Parallelism

Your task is to calculate the state of the board in the next generation.
The game takes place on a two-dimensional board consisting of cells that can be living (`true`) or dead (`false`).

Open the file `Models/LifeEngine.cs`.

1. Method `CalculateNextGeneration`: (**3 pts.**)
   - Return a `SimulationStepResult` object containing information about the total number of living cells and the time in which the new state was calculated.
   - The state of a cell in the next turn depends on the number of its living neighbors, which is returned by the ready-made function `int CountLiveNeighbors(int y, int x)`.
   - Apply the game rules:
     - Survival: A living cell with 2 or 3 neighbors stays alive.
     - Birth: A dead cell with exactly 3 neighbors becomes alive.
     - Death: In any other case, the cell becomes/remains dead.
   - Use `Parallel.For` to compute board rows in parallel.
   - **Hint:** Remember that during calculations, you cannot modify the `Grid` array (current state) from which you are reading data.
   - **Hint:** To sum the total number of living cells in a parallel loop, use `Interlocked.Add`.
   - **Hint:** Use the `Parallel.For` loop and `ParallelOptions` to pass the `token` parameter to the loop.

## Stage 3: Connecting UI to Logic

This is the most important stage where you will connect the UI to the logic, ensuring that the application does not "hang" and responds to changes.

Open the file `ViewModels/MainWindowViewModel.cs`. It contains a partial class definition that you will be extending.
The remaining part (closely related to the Avalonia graphical framework used) is in `ViewModels/UI/MainWindowViewModel.UI.cs` (you don't need to edit it).

All variables written in `special font` are either function parameters or exist in the file `ViewModels/UI/MainWindowViewModel.UI.cs` and you don't need to declare them.

1. Method `SimulationLoop`: (**2 pts.**)
   - Calculates the new state of the game in a loop.
   - In each iteration:
     - Reports progress using the `progress` argument. An object of type `SimulationFrame` should contain a cloned (`.Clone()`) internal array of the engine `_engine`.
     - Delays its execution by `SimulationDelay` milliseconds.
   - Ends execution depending on the state of the `token` parameter.
2. Method `StartSimulationFromFileAsync`: (**4 pts.**)
   - Stops execution of the previous simulation (use `_simulationCancellationTokenSource`).
   - If the previous simulation was in progress, `await` the `_currentSimulationTask`.
   - Loads the board contents from the file `filePath` using `_fileService`. Its size is `BoardSize × BoardSize`.
   - Sets the state of the game engine `_engine` (using `LoadState` method of `LifeEngine` class) and initial values for the status bar (statistics: `Generation`, `StatusText`).
   - Runs the `SimulationLoop` method without waiting (without `await`) using `Task.Run`. The `Task` object returned by the method should be assigned to `_currentSimulationTask`.
   - Defines the progress reporting logic (creates an object of type `Progress<SimulationFrame>`).
   - Progress updates:
     - The number of living cells `LiveCells`.
     - The last calculation time `LastCalculationTimeMs`.
     - The generation `Generation`
     - Refreshes the board by assigning the new board state to the variable `CurrentGrid`.

### Testing Everything:

1. Click `Choose directory...` – files should appear on the list one by one (thanks to `Task.Delay` in the service).
2. Click a file on the list – the simulation should start.
3. Change the speed slider – the simulation should speed up/slow down immediately.
