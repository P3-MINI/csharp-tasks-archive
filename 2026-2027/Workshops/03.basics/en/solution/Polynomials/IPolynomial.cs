namespace Polynomials;

public interface IPolynomial
{
    int Degree { get; }
    float Evaluate(float x);
}
