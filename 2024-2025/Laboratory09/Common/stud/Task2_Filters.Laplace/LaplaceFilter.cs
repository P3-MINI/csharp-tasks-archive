using System.Drawing;
using Task2_Filters.Common;

namespace Task2_Filters.Laplace
{
    public class LaplaceFilter : IImageFilter
    {
        public PixelColor[,] ApplyFilter(PixelColor[,] pixels)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);

            PixelColor[,] result = new PixelColor[width, height];

            int[,] laplaceKernel = new int[,]
            {
                { -1, -1, -1 },
                { -1,  8, -1 },
                { -1, -1, -1 }
            };

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int sumR = 0, sumG = 0, sumB = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            PixelColor neighbor = pixels[x + i, y + j];

                            sumR += neighbor.R * laplaceKernel[i + 1, j + 1];
                            sumG += neighbor.G * laplaceKernel[i + 1, j + 1];
                            sumB += neighbor.B * laplaceKernel[i + 1, j + 1];
                        }
                    }
                    sumR = Math.Clamp(sumR, 0, 255);
                    sumG = Math.Clamp(sumG, 0, 255);
                    sumB = Math.Clamp(sumB, 0, 255);

                    result[x, y] = new PixelColor(sumR, sumG, sumB);
                }
            }

            return result;
        }
    }
}
