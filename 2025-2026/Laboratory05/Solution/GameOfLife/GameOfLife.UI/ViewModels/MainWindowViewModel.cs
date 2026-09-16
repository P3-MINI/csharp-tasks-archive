using CommunityToolkit.Mvvm.ComponentModel;
using GameOfLife.UI.Models;
using System;
using System.IO;
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
            if (_simulationCancellationTokenSource != null)
            {
                _simulationCancellationTokenSource.Cancel();
                if (_currentSimulationTask != null)
                {
                    try
                    {
                        await _currentSimulationTask;
                    }
                    catch (OperationCanceledException) { /* Ignored */ }
                }
                _simulationCancellationTokenSource.Dispose();
            }

            _simulationCancellationTokenSource = new CancellationTokenSource();
            var token = _simulationCancellationTokenSource.Token;

            try
            {
                var initialState = await _fileService.LoadBoardAsync(filePath, BoardSize, BoardSize);

                _engine.LoadState(initialState);
                CurrentGrid = (bool[,])_engine.Grid.Clone();

                Generation = 0;
                StatusText = $"Simulation: {Path.GetFileName(filePath)}";

                var progress = new Progress<SimulationFrame>(frame =>
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    LiveCells = frame.Stats.LiveCells;
                    LastCalculationTimeMs = frame.Stats.Duration.TotalMilliseconds;
                    Generation++;
                    CurrentGrid = frame.GridState;
                });

                _currentSimulationTask = Task.Run(() => SimulationLoop(progress, token), token);
            }
            catch (Exception ex)
            {
                StatusText = $"Error: {ex.Message}";
            }
        }

        private async Task SimulationLoop(IProgress<SimulationFrame> progress, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    SimulationStepResult stats;
                    bool[,] gridSnapshot;

                    stats = _engine.CalculateNextGeneration(token);
                    gridSnapshot = (bool[,])_engine.Grid.Clone();

                    progress.Report(new SimulationFrame(stats, gridSnapshot));

                    var delay = Math.Max(0, SimulationDelay);
                    await Task.Delay(delay, token);
                }
            }
            catch (TaskCanceledException) { /* Ignored */ }
        }
    }
}