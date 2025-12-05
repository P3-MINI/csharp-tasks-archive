using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Platform;
using Task2_Filters.Common;

namespace Task2_ImageFilters
{
    internal static class BitmapConverters
    {
        public static PixelColor[,] BitmapTo2DArray(WriteableBitmap bitmap)
        {
            int width = bitmap.PixelSize.Width;
            int height = bitmap.PixelSize.Height;

            PixelColor[,] pixels = new PixelColor[width, height];

            using (var data = bitmap.Lock())
            {
                unsafe
                {
                    byte* ptr = (byte*)data.Address;
                    int stride = data.RowBytes;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int index = y * stride + x * 4; // Assuming 32bpp BGRA
                            byte b = ptr[index];
                            byte g = ptr[index + 1];
                            byte r = ptr[index + 2];

                            pixels[x, y] = new PixelColor(r, g, b);
                        }
                    }
                }
            }
            return pixels;
        }

        public static WriteableBitmap ArrayToBitmap(PixelColor[,] pixels)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);
            var bitmap = new WriteableBitmap(new PixelSize(width, height), new Vector(96, 96), PixelFormat.Bgra8888);

            using (var data = bitmap.Lock())
            {
                unsafe
                {
                    byte* ptr = (byte*)data.Address;
                    int stride = data.RowBytes;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int index = y * stride + x * 4;
                            ptr[index] = (byte)pixels[x, y].B;
                            ptr[index + 1] = (byte)pixels[x, y].G;
                            ptr[index + 2] = (byte)pixels[x, y].R;
                            ptr[index + 3] = 255; // Alpha channel
                        }
                    }
                }
            }
            return bitmap;
        }

        public static WriteableBitmap LoadImageAsWriteableBitmap(Bitmap bitmap)
        {
            var writeableBitmap = new WriteableBitmap(bitmap.PixelSize, bitmap.Dpi, PixelFormat.Bgra8888);
            using (var framebuffer = writeableBitmap.Lock())
            {
                bitmap.CopyPixels(framebuffer, AlphaFormat.Opaque);
            }

            return writeableBitmap;
        }
    }
}
