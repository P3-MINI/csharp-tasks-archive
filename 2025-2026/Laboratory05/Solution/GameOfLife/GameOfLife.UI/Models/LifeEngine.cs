using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.Models;

public record SimulationStepResult(
    int LiveCells,
    TimeSpan Duration
);

public class LifeEngine
{
    public int Rows { get; }
    public int Cols { get; }
    public bool[,] Grid { get; private set; }

    public LifeEngine(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Grid = new bool[rows, cols];
    }

    public void LoadState(bool[,] newGrid)
    {
        Grid = newGrid;
    }

    public SimulationStepResult CalculateNextGeneration(CancellationToken token = default)
    {
        var stopwatch = Stopwatch.StartNew();
        var newGrid = new bool[Rows, Cols];
        var totalLiveCount = 0;

        var parallelOptions = new ParallelOptions
        {
            CancellationToken = token,
        };

        try
        {
            Parallel.For(0, Rows, parallelOptions,
                () => 0,
                (y, loopState, localLiveCount) =>
                {
                    token.ThrowIfCancellationRequested();

                    for (var x = 0; x < Cols; x++)
                    {
                        var liveNeighbors = CountLiveNeighbors(y, x);
                        var isAlive = Grid[y, x];
                        var willBeAlive = false;

                        if (isAlive && (liveNeighbors == 2 || liveNeighbors == 3))
                            willBeAlive = true;
                        else if (!isAlive && liveNeighbors == 3)
                            willBeAlive = true;

                        if (willBeAlive)
                        {
                            newGrid[y, x] = true;
                            localLiveCount++;
                        }
                    }

                    return localLiveCount;
                },

                (finalThreadLiveCount) =>
                {
                    Interlocked.Add(ref totalLiveCount, finalThreadLiveCount);
                }
            );

            Grid = newGrid;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SimulationStepResult(totalLiveCount, stopwatch.Elapsed);
    }

    private int CountLiveNeighbors(int y, int x)
    {
        var count = 0;
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                var ny = y + i;
                var nx = x + j;

                if (ny >= 0 && ny < Rows && nx >= 0 && nx < Cols)
                {
                    if (Grid[ny, nx]) count++;
                }
            }
        }
        return count;
    }
}