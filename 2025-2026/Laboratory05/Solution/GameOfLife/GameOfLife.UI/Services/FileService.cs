using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.Services;

public class FileService : IFileService
{
    public async IAsyncEnumerable<string> EnumerateFilesAsync(
        string folderPath, 
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken token)
    {
        await Task.Yield();

        var options = new EnumerationOptions 
        { 
            IgnoreInaccessible = true, 
            RecurseSubdirectories = false 
        };

        foreach (var file in Directory.EnumerateFiles(folderPath, "*.txt", options))
        {
            if (token.IsCancellationRequested)
            {
                yield break;
            }

            await Task.Delay(50, token);

            yield return file;
        }
    }

    public async Task<bool[,]> LoadBoardAsync(string filePath, int rows, int cols)
    {
        var lines = await File.ReadAllLinesAsync(filePath);

        var grid = new bool[rows, cols];

        for (var y = 0; y < Math.Min(lines.Length, rows); y++)
        {
            var line = lines[y];
            for (var x = 0; x < Math.Min(line.Length, cols); x++)
            {
                grid[y, x] = line[x] == '0';
            }
        }

        return grid;
    }
}