using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System;

namespace Windowing;

public partial class MainWindow : Window
{
    private WriteableBitmap? _bitmap;
    private int _width;
    private int _height;

    public MainWindow()
    {
        InitializeComponent();
    }

    public void InitializeImage(int width, int height)
    {
        _width = width;
        _height = height;
        _bitmap = new WriteableBitmap(new PixelSize(width, height), new Vector(96, 96), PixelFormat.Rgba8888, AlphaFormat.Opaque);
        RenderImage.Source = _bitmap;
    }

    public void UpdateStatus(string status)
    {
        Dispatcher.UIThread.Post(() => StatusText.Text = status);
    }

    public void UpdateImage(ReadOnlySpan<byte> data)
    {
        if (_bitmap == null) return;
        
        using (var lockedBitmap = _bitmap.Lock())
        {
            for (int y = 0; y < _height; y++)
            {
                IntPtr destRow = lockedBitmap.Address + (y * lockedBitmap.RowBytes);

                // Slice the span for the current row
                var rowData = data.Slice(y * _width * 4, _width * 4);

                unsafe
                {
                    fixed (byte* srcPtr = rowData)
                    {
                        Buffer.MemoryCopy(srcPtr, (void*)destRow, lockedBitmap.RowBytes, _width * 4);
                    }
                }
            }
        }
        
        Dispatcher.UIThread.Post(RenderImage.InvalidateVisual);
    }
}