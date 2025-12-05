using Task2_Filters.Common;

namespace Task2_Filters.Emboss
{
    public class EmbossFilter : IImageFilter
    {
        public PixelColor[,] ApplyFilter(PixelColor[,] pixels)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);

            // Define the Emboss filter kernel
            double[,] kernel = new double[,]
            {
                { -2, -1,  0 },
                { -1,  1,  1 },
                {  0,  1,  2 }
            };

            PixelColor[,] result = new PixelColor[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double r = 0, g = 0, b = 0;
                    double sum = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int pixelX = Math.Clamp(x + ky, 0, width - 1);
                            int pixelY = Math.Clamp(y + kx, 0, height - 1);

                            PixelColor neighbor = pixels[pixelX, pixelY];

                            double weight = kernel[kx + 1, ky + 1];
                            r += neighbor.R * weight;
                            g += neighbor.G * weight;
                            b += neighbor.B * weight;
                            sum += weight;
                        }
                    }

                   
                    r = Math.Clamp(r + 128, 0, 255);
                    g = Math.Clamp(g + 128, 0, 255);
                    b = Math.Clamp(b + 128, 0, 255);

                    result[x, y] = new PixelColor((byte)r, (byte)g, (byte)b);
                }
            }

            return result;
        }
    }
}
