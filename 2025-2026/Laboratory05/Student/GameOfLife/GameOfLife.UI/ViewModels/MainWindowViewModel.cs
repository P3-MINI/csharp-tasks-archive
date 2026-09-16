using CommunityToolkit.Mvvm.ComponentModel;
using GameOfLife.UI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.ViewModels
{
    public record SimulationFrame(
        SimulationStepResult Stats,
        bool[,] GridState
    );

    public partial class MainWindowViewModel : ObservableObject, IDisposable
    {
        private async Task ReloadFilesAsync(string folderPath)
        {
            _fileLoadingCancellationTokenSource?.Cancel();
            _fileLoadingCancellationTokenSource?.Dispose();
            _fileLoadingCancellationTokenSource = new CancellationTokenSource();
            var token = _fileLoadingCancellationTokenSource.Token;

            StatusText = "Scanning directory...";
            IsLoadingFiles = true;
            FileList.Clear();

            try
            {
                await foreach (var file in _fileService.EnumerateFilesAsync(folderPath, token))
                {
                    FileList.Add(file);
                }
                StatusText = "Done.";
            }
            catch (OperationCanceledException)
            {
                StatusText = "Scanning cancelled.";
            }
            finally
            {
                IsLoadingFiles = false;
            }
        }

        private async Task StartSimulationFromFileAsync(string filePath)
        {
            try
            {
                // TODO: Implement loading the board from file and starting the simulation
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                StatusText = $"Error: {ex.Message}";
            }
        }

        private async Task SimulationLoop(IProgress<SimulationFrame> progress, CancellationToken token)
        {
            // TODO: Implement the simulation loop that calculates generations and reports progress
            throw new NotImplementedException();
        }
    }
}