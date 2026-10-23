namespace ImageFiltering.Core;

public abstract class FilterMatrix : FilterBase
{
    protected float[,] Kernel { get; }
    protected float Divisor { get; }

    protected FilterMatrix(float[,] kernel, float divisor = 1.0f)
    {
        ArgumentNullException.ThrowIfNull(kernel);

        if (kernel.GetLength(0) != 3 || kernel.GetLength(1) != 3)
        {
            throw new ArgumentException("Kernel dimensions must be 3x3.");
        }

        if (divisor == 0.0f)
        {
            throw new ArgumentException("Divisor cannot be zero.");
        }
        
        Kernel = (float[,])kernel.Clone();
        Divisor = divisor;
    }
    
    public override Image Apply(Image image)
    {
        ArgumentNullException.ThrowIfNull(image);

        Image result = image.CreateImage(image.Width, image.Height);

        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                float sumR = 0f;
                float sumG = 0f;
                float sumB = 0f;

                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        int sampleX = x + kx;
                        int sampleY = y + ky;
                        ColorRgb neighbor = image.SampleClamped(sampleX, sampleY);
                        float weight = Kernel[ky + 1, kx + 1];

                        sumR += neighbor.Red * weight;
                        sumG += neighbor.Green * weight;
                        sumB += neighbor.Blue * weight;
                    }
                }

                float outR = sumR / Divisor;
                float outG = sumG / Divisor;
                float outB = sumB / Divisor;

                result[x, y] = new ColorRgb(outR, outG, outB);
            }
        }

        return result;
    }
}