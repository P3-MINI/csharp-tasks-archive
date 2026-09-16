using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.Services;

public interface IFileService
{
    IAsyncEnumerable<string> EnumerateFilesAsync(string folderPath, CancellationToken token);

    Task<bool[,]> LoadBoardAsync(string filePath, int rows, int cols);
}