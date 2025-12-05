using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.ComponentModel;
using System;
using Task2_Filters.Common;

namespace Task2_ImageFilters;

public partial class ImageFilterViewerWindow : Window, INotifyPropertyChanged
{
    private WriteableBitmap _bitmap;
    
    public ImageFilterViewerWindow()
    {
        InitializeComponent();
        DataContext = this;
        LoadImage();
    }

    private void LoadImage()
    {
        var uri = new Uri("avares://Task2_ImageFilters/placeholder_image.jpeg");
        var bitmap = new Bitmap(AssetLoader.Open(uri));
        _bitmap = BitmapConverters.LoadImageAsWriteableBitmap(bitmap);
        InputImage.Source = _bitmap;
    }

    private void ApplyFilter(PixelColor[,] array)
    {
        _bitmap = BitmapConverters.ArrayToBitmap(array);
        InputImage.Source = _bitmap;
    }

    private void ResetImageButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        LoadImage();
    }

    private void ApplyGaussianBlurFilterButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PixelColor[,] inputArray = BitmapConverters.BitmapTo2DArray(_bitmap);
        ApplyFilter(FiltersLibrary.ApplyGaussianBlurFilter(inputArray));
    }

    private void ApplyLaplaceFilterButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PixelColor[,] inputArray = BitmapConverters.BitmapTo2DArray(_bitmap);
        ApplyFilter(FiltersLibrary.ApplyLaplaceFilter(inputArray));
    }

    private void ApplyEmbossFilterButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PixelColor[,] inputArray = BitmapConverters.BitmapTo2DArray(_bitmap);
        ApplyFilter(FiltersLibrary.ApplyEmbossFilter(inputArray));
    }
}