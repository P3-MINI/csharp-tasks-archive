using System.IO;
using System.Reflection;
using System.Linq;
using Task2_Filters.Common;
using System;

namespace Task2_ImageFilters;
public static class Helpers
{
    public static PixelColor[,] ApplyFilterOperation(PixelColor[,] pixels, string pluginPath)
    {
        FilterLoadContext context = new FilterLoadContext(pluginPath);
        try
        {
            Assembly assembly = context.LoadFromAssemblyPath(Path.GetFullPath(pluginPath));
            Type pluginType = assembly
                .ExportedTypes
                .Single(t => typeof(IImageFilter).IsAssignableFrom(t));
            var plugin = Activator.CreateInstance(pluginType) as IImageFilter;
            return plugin?.ApplyFilter(pixels);
        }
        finally
        {
            if (context.IsCollectible) context.Unload();
        }
    }
}