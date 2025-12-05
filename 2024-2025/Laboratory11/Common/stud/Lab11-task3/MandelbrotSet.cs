namespace Lab11_task3;

public class MandelbrotSet
{
    public const int MaxIterations = 10000;

    public static int Calculate(double a, double b)
    {
        double x = 0, y = 0;
        int iterations = 0;

        while (x * x + y * y <= 4 && iterations < MaxIterations)
        {
            double tempX = x * x - y * y + a;
            y = 2 * x * y + b;
            x = tempX;
            iterations++;
        }

        return iterations;
    }
}
