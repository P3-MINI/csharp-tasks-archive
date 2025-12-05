using System.Collections;

namespace lab7yield
{

interface IDoubleSequenceProcessor
    {
    IEnumerable Process(IEnumerable sequence1, IEnumerable sequence2);
    string GetSignature();
    }

/// <summary>
/// Implements a logic gate AND for a bit sequence.
/// </summary>
class LogicGateAND : IDoubleSequenceProcessor
    {

    public string GetSignature()
        {
        return "LogicGateAND";
        }

    public IEnumerable Process(IEnumerable sequence1, IEnumerable sequence2)
        {
        IEnumerator enumerator1 = sequence1.GetEnumerator();
        IEnumerator enumerator2 = sequence2.GetEnumerator();
        bool enumerator1HasEnded;
        bool enumerator2HasEnded;

        while ( true )
            {
            enumerator1HasEnded = !enumerator1.MoveNext();
            enumerator2HasEnded = !enumerator2.MoveNext();
            if ( enumerator1HasEnded || enumerator2HasEnded )
                {
                if ( !(enumerator1HasEnded && enumerator2HasEnded) )
                    yield return 0;
                break;
                }
            yield return (int)enumerator1.Current & (int)enumerator2.Current;
            }

        while ( enumerator1.MoveNext() || enumerator2.MoveNext() )
            yield return 0;
        }

    }

/// <summary>
/// Implements a logic gate OR for a bit sequence.
/// </summary>
class LogicGateOR : IDoubleSequenceProcessor
    {

    public string GetSignature()
        {
        return "LogicGateOR";
        }

    public IEnumerable Process(IEnumerable sequence1, IEnumerable sequence2)
        {
        IEnumerator enumerator1 = sequence1.GetEnumerator();
        IEnumerator enumerator2 = sequence2.GetEnumerator();
        bool enumerator1HasEnded;
        bool enumerator2HasEnded;

        while ( true )
            {
            enumerator1HasEnded = !enumerator1.MoveNext();
            enumerator2HasEnded = !enumerator2.MoveNext();
            if ( enumerator1HasEnded || enumerator2HasEnded )
                {
                if ( !enumerator1HasEnded )
                    yield return (int)enumerator1.Current;
                if ( !enumerator2HasEnded )
                    yield return (int)enumerator2.Current;
                break;
                }
            yield return (int)enumerator1.Current | (int)enumerator2.Current;
            }

        while (enumerator1.MoveNext())
            yield return (int)enumerator1.Current;
        while (enumerator2.MoveNext())
            yield return (int)enumerator2.Current;
        }
    }

}
