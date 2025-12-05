// PW: It can be also written using '=>' operator as Expression body definition
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition
// Looks just simpler:
// public static Func<double, double> Constant(double constantValue) => x => x;

// PW: It should be private property e.g.
// private Func<double, double> InternalFunction { get; set; }

// PW: It should store copy of original argument
// e.g. (double[])coefficientValues.Clone();

// Stage1 (1pts)
// static Functions class
// Func<double, double> Constant(double constantValue)
// Func<double, double> Identity()
// Func<double, double> Exp(double coefficient)

// Stage2(1pts)
// Function
// private property to hold Func<>
// constructor with Func<>
// implicit cast Func<> -> Function
// Value method that evaluate Func<>
// 0.5pts
// IEnumerable<double> GetValues(double aValue, double bValue, int nValue) -> [a, b]

// Stage3(1.5pts)
// class Polynomial : Function
// static ToFunction(double[] coefficientValues) [a0, a1, ..., an] => f(x) = a0 + a1x^1 + a2x^2 + ... + anx^n (Horner method)
// (0.5pts)Constructor double[] coefficientValues assigns using ToFunction ,call base constructor, copy of coefficients
// (0.5pts) Derivative(double xValue)

// Stage4 (1.5pts)
// class NumericalMethods
// (0.5pts) extension method Derivative for Function
// F'(x) = (F(x+h) - F(x-h)) / (2*h) where h defaults to 0.001
// (0.5pts) Integral extension method