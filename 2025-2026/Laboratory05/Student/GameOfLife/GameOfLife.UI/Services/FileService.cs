using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.Services;

public class FileService : IFileService
{
    public async IAsyncEnumerable<string> EnumerateFilesAsync(
        string folderPath, 
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken token)
    {
        // TODO: Implement asynchronous file enumeration
        // The line below be deleted as it was added to suppress error CS8420:
        // The body of an async-iterator method must contain a 'yield' statement.
        yield break; 
        
        throw new NotImplementedException();
    }

    public async Task<bool[,]> LoadBoardAsync(string filePath, int rows, int cols)
    {
        // TODO: Implement board loading from file
        throw new NotImplementedException();
    }
}