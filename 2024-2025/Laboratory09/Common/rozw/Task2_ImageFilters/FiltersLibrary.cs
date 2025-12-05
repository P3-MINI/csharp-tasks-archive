using System.IO;
using System;
using Task2_Filters.Common;

namespace Task2_ImageFilters;
public  static class FiltersLibrary
{
    
    public static PixelColor[,] ApplyGaussianBlurFilter(PixelColor[,] array)
    {
        // Part 1 - Invoke GaussianBlur Filter from the Task2_Filters.GaussianBlur.dll
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        return Helpers.ApplyFilterOperation(array, projectDirectory + "/Filters/Task2_Filters.GaussianBlur.dll");
        // throw new NotImplementedException();
    }

    public static PixelColor[,] ApplyLaplaceFilter(PixelColor[,] array)
    {
        // Part 2 - Invoke Laplace Filter from the Task2_Filters.Laplace.dll
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        return Helpers.ApplyFilterOperation(array, projectDirectory + "/Filters/Task2_Filters.Laplace.dll");
        //throw new NotImplementedException();
    }

    public static PixelColor[,] ApplyEmbossFilter(PixelColor[,] array)
    {

        // Part 3 - Invoke Emboss Filter from the Task2_Filters.Emboss.dll
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        return Helpers.ApplyFilterOperation(array, projectDirectory + "/Filters/Task2_Filters.Emboss.dll");
        //throw new NotImplementedException();
    }
}