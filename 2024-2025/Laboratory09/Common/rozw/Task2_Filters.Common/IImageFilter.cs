using System.Drawing;

namespace Task2_Filters.Common
{
    public struct PixelColor
    {
        public int R;
        public int G;
        public int B;

        public PixelColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public interface IImageFilter
    {
       public PixelColor[,] ApplyFilter(PixelColor[,] pixels);
    }
}
