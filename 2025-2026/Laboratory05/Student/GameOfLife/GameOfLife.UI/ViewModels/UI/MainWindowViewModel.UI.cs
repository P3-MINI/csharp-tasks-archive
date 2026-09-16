using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameOfLife.UI.Models;
using GameOfLife.UI.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.UI.ViewModels;
public partial class MainWindowViewModel : ObservableObject, IDisposable
{
    public int BoardSize { get; } = 100;
    public ObservableCollection<string> FileList { get; } = [];

    private readonly LifeEngine _engine;
    private readonly IFileService _fileService;

    private CancellationTokenSource? _simulationCancellationTokenSource;
    private CancellationTokenSource? _fileLoadingCancellationTokenSource;
    private Task? _currentSimulationTask;

    // UI roperties
    [ObservableProperty] private bool[,]? _currentGrid;
    [ObservableProperty] private string _statusText = "Choose directory...";
    [ObservableProperty] private int _simulationDelay = 10;
    [ObservableProperty] private int _generation;
    [ObservableProperty] private int _liveCells;
    [ObservableProperty] private double _lastCalculationTimeMs;
    [ObservableProperty] private bool _isLoadingFiles;
    [ObservableProperty] private string? _selectedFile;

    public MainWindowViewModel()
    {
        _engine = new LifeEngine(BoardSize, BoardSize);
        _fileService = new FileService();

        CurrentGrid = new bool[BoardSize, BoardSize];
    }

    [RelayCommand]
    public async Task BrowseFolder(IStorageProvider storageProvider)
    {
        var resourcesPath = Path.Combine(AppContext.BaseDirectory, "Examples");
        var startLocation = await storageProvider.TryGetFolderFromPathAsync(resourcesPath);

        var result = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Choose the directory with simulation input files",
            AllowMultiple = false,
            SuggestedStartLocation = startLocation
        });

        if (result != null && result.Count > 0 && result[0].TryGetLocalPath() is string path)
        {
            await ReloadFilesAsync(path);
        }
    }

    partial void OnSelectedFileChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _ = StartSimulationFromFileAsync(value);
        }
    }

    public void Dispose()
    {
        _simulationCancellationTokenSource?.Cancel();
        _fileLoadingCancellationTokenSource?.Cancel();

        _simulationCancellationTokenSource?.Dispose();
        _fileLoadingCancellationTokenSource?.Dispose();
    }
}