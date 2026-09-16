using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.Services;

/// <summary>
/// Defines the contract for file input/output operations.
/// This service abstracts the logic of scanning directories and parsing text files.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Asynchronously enumerates files in the specified directory.
    /// Using IAsyncEnumerable allows the UI to populate the list item-by-item (streaming)
    /// without freezing the interface, even if there are thousands of files.
    /// </summary>
    /// <param name="folderPath">The full path to the directory to scan.</param>
    /// <param name="token">A token to cancel the operation if the user selects a different folder.</param>
    /// <returns>An async stream of file paths.</returns>
    IAsyncEnumerable<string> EnumerateFilesAsync(string folderPath, CancellationToken token);

    /// <summary>
    /// Asynchronously loads a game board from a text file.
    /// Reads the file content and parses it into a 2D boolean grid.
    /// </summary>
    /// <param name="filePath">The full path to the text file.</param>
    /// <param name="rows">The required height of the grid.</param>
    /// <param name="cols">The required width of the grid.</param>
    /// <returns>A task representing the asynchronous operation, containing the initialized grid.</returns>
    Task<bool[,]> LoadBoardAsync(string filePath, int rows, int cols);
}