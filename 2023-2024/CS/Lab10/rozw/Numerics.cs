namespace Lab10EN;

public static class Numerics
{
    public delegate double Function(double x);

    public static double HalleyRootFinding(Function f, Function? df = null, Function? ddf = null, double x0 = 0, double eps = 1e-6)
    {
        df ??= x => (f(x + eps) - f(x - eps)) / (2 * eps);
        ddf ??= x => (df(x + eps) - df(x - eps)) / (2 * eps);
        double x;
        double xn = x0;

        do
        {
            x = xn;
            double fx = f(x), dfx = df(x), ddfx = ddf(x);
            xn = x - 2 * fx * dfx / (2 * dfx * dfx - fx * ddfx);
        } while (Math.Abs(x - xn) >= eps);

        return xn;
    }

    public static Function Const(double c) => _ => c;
    public static Function Linear(double a, double b) => x => a * x + b;
    public static Function Quadratic(double a, double b, double c) => x => (a * x + b) * x + c;

    public static double FindLinearRoot()
    {
        return HalleyRootFinding(Linear(2, -1), Const(2), Const(0));
    }

    public static double FindQuadraticRoot()
    {
        return HalleyRootFinding(Quadratic(2, 3, 1));
    }

    public static double FindLogRoot()
    {
        return HalleyRootFinding(double.Log, double.ReciprocalEstimate, null, 2);
    }
}