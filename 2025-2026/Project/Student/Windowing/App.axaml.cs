using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;

namespace Windowing;

public partial class App : Application
{
    public static int Width { get; set; } = 800;
    public static int Height { get; set; } = 450;
    public static string Title { get; set; } = "Viewer";
    public static Action<MainWindow>? OnStartup { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = new MainWindow();
            desktop.MainWindow = window;
            
            window.InitializeImage(Width, Height);
            window.Title = Title;

            if (OnStartup != null)
            {
                OnStartup(window);
                OnStartup = null; // Cleanup
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}
