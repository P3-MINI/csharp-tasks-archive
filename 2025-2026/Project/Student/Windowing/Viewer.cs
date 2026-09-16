using System;
using System.Threading.Tasks;
using Avalonia;

namespace Windowing;

public interface IWindowUpdate
{
    void UpdateImage(ReadOnlySpan<byte> data);
    void UpdateStatus(string text);
    bool IsClosed { get; }
}

public class Viewer
{
    public static void Show(int width, int height, string title, Action<IWindowUpdate> renderLoop)
    {
        App.Width = width;
        App.Height = height;
        App.Title = title;
        
        App.OnStartup = (window) =>
        {
            var updater = new WindowUpdater(window);
            Task.Run(() =>
            {
                try
                {
                    renderLoop(updater);
                }
                catch (Exception ex)
                {
                    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                    {
                        updater.UpdateStatus($"Error: {ex.Message}");
                    });
                    Console.WriteLine($"Render Loop Crash: {ex}");
                }
            });
        };

        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .StartWithClassicDesktopLifetime(args: Array.Empty<string>(), shutdownMode: Avalonia.Controls.ShutdownMode.OnMainWindowClose);
    }
    
    private class WindowUpdater : IWindowUpdate
    {
        private readonly MainWindow _win;
        private bool _isClosed = false;
        
        public WindowUpdater(MainWindow win)
        {
            _win = win;
            _win.Closed += (s, e) => _isClosed = true;
        }
        
        public void UpdateImage(ReadOnlySpan<byte> data)
        {
            if (_isClosed) return;
            _win.UpdateImage(data);
        }

        public void UpdateStatus(string text)
        {
            if (_isClosed) return;
            _win.UpdateStatus(text);
        }
        
        public bool IsClosed => _isClosed;
    }
}
