using System.Collections;

namespace lab7yield
{
    
interface ISingleSequenceProcessor
    {
    IEnumerable Process(IEnumerable sequence);
    string GetSignature();
    }

class SequenceSum : ISingleSequenceProcessor
    {

    public string GetSignature()
        {
        return "SequenceSum";
        }

    public IEnumerable Process(IEnumerable sequence)
        {
        int s = 0;
        foreach (int el in sequence)
            yield return s += el;
        }
    }

/// <summary>
/// Calculates sequence element remainders.
/// </summary>
class SequenceRemainderer : ISingleSequenceProcessor
    {

    private int modulus;

    public SequenceRemainderer(int _modulus)
        {
        modulus = _modulus;
        }

    public string GetSignature()
        {
        return string.Format("SequenceRemainderer {{ Modulus = {0} }}", modulus);
        }

    public IEnumerable Process(IEnumerable sequence)
        {
        foreach (int el in sequence)
            yield return el % modulus;
        }
    }

/// <summary>
/// Takes every N element (with optional memory state).
/// </summary>
class EveryNFilter : ISingleSequenceProcessor
    {

    private int start;
    private int N;
    private bool memory;

    public EveryNFilter(int _start, int _N, bool _memory=false)
        {
        start = _start;
        N = _N;
        memory = _memory;
        }

    public string GetSignature()
        {
            return string.Format("EveryNFilter {{ Start={0}, N={1}, Memory = {2} }}", start, N, memory);
        }

    public IEnumerable Process(IEnumerable sequence)
        {
        int k = 1;
        object el = null;
        foreach (var i in sequence)
            {
            if (k++ < start) continue;
            if ((k - start - 1) % N == 0)
                {
                el = i;
                yield return i;
                }
            else
                if ( memory )
                    yield return el;
            }
        }
    }

/// <summary>
/// For every N element takes sum of M elements.
/// </summary>
    class EveryNSumMFilter : ISingleSequenceProcessor
    {
    private int N;
    private int M;
    private int start;

    public EveryNSumMFilter(int _start, int _N, int _M)
        {
        N = _N;
        M = _M;
        start = _start;
        }

    public string GetSignature()
        {
        return string.Format("EveryNSumMFilter {{ N = {0}, Start = {1}, M = {2} }}", N, start, M);
        }

    public IEnumerable Process(IEnumerable sequence)
        {
        int k = 0;
        int el = 0;
        int check;
        foreach (int i in sequence)
            {
            if ( ++k<start) continue;
            check = (k-start)%N;
            if ( check<M )
                {
                el+=i;
                if ( check==M-1 )
                    {
                    yield return el;
                    el = 0;
                    }
                }
            }

        }

    }

}
