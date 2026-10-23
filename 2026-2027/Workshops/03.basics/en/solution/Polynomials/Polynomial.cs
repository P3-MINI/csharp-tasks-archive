namespace Polynomials;

public class Polynomial : IPolynomial
{
    private readonly List<Term> _terms = new();

    public Polynomial()
    {
    }

    public Polynomial(params Term[] terms)
    {
        foreach (var term in terms)
        {
            Add(term);
        }
    }

    public void Add(Term term)
    {
        for (int i = 0; i < _terms.Count; i++)
        {
            if (_terms[i].Power == term.Power)
            {
                _terms[i] = new Term(_terms[i].Coeff + term.Coeff, term.Power);
                return;
            }
        }
        _terms.Add(term);
    }

    public int Degree
    {
        get
        {
            int maxDegree = 0;
            bool hasNonZero = false;
            foreach (var term in _terms)
            {
                if (term.Coeff != 0)
                {
                    if (!hasNonZero || term.Power > maxDegree)
                    {
                        maxDegree = term.Power;
                        hasNonZero = true;
                    }
                }
            }
            return hasNonZero ? maxDegree : 0;
        }
    }

    public float Evaluate(float x)
    {
        float sum = 0;
        foreach (var term in _terms)
        {
            sum += term.Evaluate(x);
        }
        return sum;
    }

    public static Polynomial operator +(Polynomial a, Polynomial b)
    {
        var result = new Polynomial();
        foreach (var term in a._terms)
        {
            result.Add(term);
        }
        foreach (var term in b._terms)
        {
            result.Add(term);
        }
        return result;
    }

    public override string ToString()
    {
        if (_terms.Count == 0)
        {
            return "0";
        }
        return string.Join(" + ", _terms);
    }

    public bool FindRoot(float a, float b, out float root, float eps = 1e-4f)
    {
        float fa = Evaluate(a);
        float fb = Evaluate(b);

        if (fa * fb >= 0)
        {
            root = 0;
            return false;
        }

        if (a > b)
        {
            (a, b) = (b, a);
            (fa, fb) = (fb, fa);
        }

        int maxIterations = 100;
        while ((b - a) > eps && maxIterations-- > 0)
        {
            float mid = (a + b) / 2f;
            if (mid == a || mid == b)
            {
                break;
            }

            float fmid = Evaluate(mid);

            if (MathF.Abs(fmid) <= eps)
            {
                root = mid;
                return true;
            }

            if (fa * fmid < 0)
            {
                b = mid;
                fb = fmid;
            }
            else
            {
                a = mid;
                fa = fmid;
            }
        }

        root = (a + b) / 2f;
        return true;
    }
}
