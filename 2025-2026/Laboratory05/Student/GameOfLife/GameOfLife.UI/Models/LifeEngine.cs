using System;
using System.Threading;

namespace GameOfLife.UI.Models;

/// <summary>
/// Represents the statistics of a single simulation step.
/// </summary>
/// <param name="LiveCells">Total number of living cells in the new generation.</param>
/// <param name="Duration">Time taken to calculate the generation.</param>
public record SimulationStepResult(
    int LiveCells,
    TimeSpan Duration
);

/// <summary>
/// Core logic engine for the Game of Life simulation.
/// Responsible for managing the grid state and calculating cell evolution.
/// </summary>
public class LifeEngine
{
    public int Rows { get; }
    public int Cols { get; }

    // The 2D grid representing the board. True = Alive, False = Dead.
    public bool[,] Grid { get; private set; }

    public LifeEngine(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Grid = new bool[rows, cols];
    }

    /// <summary>
    /// Replaces the current grid state with a new one (e.g., loaded from a file).
    /// </summary>
    /// <param name="newGrid">The new state of the board.</param>
    public void LoadState(bool[,] newGrid)
    {
        Grid = newGrid;
    }

    /// <summary>
    /// Calculates the next generation of the simulation based on Conway's rules.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A result object containing the live cell count and calculation duration.</returns>
    public SimulationStepResult CalculateNextGeneration(CancellationToken token = default)
    {
        // TODO: Implement the Game of Life rules to calculate the next generation
        //
        //       For each cell, count live neighbors and apply rules:
        //          - Alive & 2 or 3 neighbors -> Stays Alive
        //          - Dead & exactly 3 neighbors -> Becomes Alive
        //          - Otherwise -> Dies/Stays Dead

        throw new NotImplementedException();
    }

    /// <summary>
    /// Helper method to count how many living neighbors surround a specific cell.
    /// </summary>
    /// <param name="y">Row index.</param>
    /// <param name="x">Column index.</param>
    /// <returns>Number of living neighbors (0-8).</returns>
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