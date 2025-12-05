namespace Lab10;

/// <summary>
/// Napisz statyczną funkcję RandomValueFunctionFactory, która zwraca funkcję anonimową,
/// zwracającą (pseudo)losową liczbę typu double z przedziału zadanego przez parametry:
/// double min i double max.<br/>
/// Napisz statyczną funkcję RandomPointFunctionFactory, która zwraca funkcję anonimową,
/// zwracającą (pseudo)losowy wektor dwóch liczb typu double (przez krotkę)
/// z przedziału zadanego przez parametry: double xMin, double xMax, double yMin, double yMax.
/// Użyj poprzednio zdefiniowanej metody.<br/>
/// Napisz statyczna funkcję MonteCarloPiEstimation zwracającą przybliżenie liczby π (typu double).
/// Użyj poprzednio zdefiniowanej metody. Metoda powinna przyjmować liczbę próbek (punktów) użytych
/// do przybliżenia typu int (z wartością domyślną 1024).<br/>
/// Metoda Monte Carlo: https://en.wikipedia.org/wiki/Monte_Carlo_method<br/>
/// Wskazówka: Generuj losowe punkty w prostokącie zawierającym (wycinek) koło jednostkowe i sprawdzaj,
/// czy są one wewnątrz (wycinka) koła jednostkowego.
/// Oblicz stosunek punktów wewnątrz (wycinka) koła do ogólnej liczby punktów, aby oszacować wartość π.
/// </summary>
public static class FactoryMethods
{
    public static Func<double> RandomValueFunctionFactory(double min, double max, int seed = 0)
    {
        var random = new Random(seed);
        return () => random.NextDouble() * (max - min) + min;
    }

    public static Func<(double, double)> RandomPointFunctionFactory(double xMin, double xMax, double yMin, double yMax, int seed = 0)
    {
        var xRandom = RandomValueFunctionFactory(xMin, xMax, seed);
        var yRandom = RandomValueFunctionFactory(yMin, yMax, seed + 1);
        return () => (xRandom(), yRandom());
    }

    public static double MonteCarloPiEstimation(int samples = 1024)
    {
        var random = RandomPointFunctionFactory(0, 1, 0, 1);
        var inside = 0;
        for (var i = 0; i < samples; i++)
        {
            var (x, y) = random();
            if (x * x + y * y < 1.0) inside++;
        }
        return 4.0 * inside / samples;
    }
}