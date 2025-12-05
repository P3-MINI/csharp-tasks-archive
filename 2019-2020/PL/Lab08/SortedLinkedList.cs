using System;

namespace PL_Lab_08
{

public class SortedLinkedList1<TKey, TValue> : IMap<TKey,TValue> where TKey : IComparable<TKey>
    {
    private System.Collections.Generic.LinkedList<Pair<TKey, TValue>> list = new System.Collections.Generic.LinkedList<Pair<TKey, TValue>>();

    public int Count
        {
        get
            {
            return list.Count;
            }
        }

    public bool Add(TKey key,TValue value)
        {
        if ( Count == 0 || list.First.Value.Key.CompareTo(key)<0 )
            {
            list.AddFirst(new Pair<TKey, TValue>(key,value));
            return true;
            }
        int compVal=-1;
        var act=list.First.Next;
        for ( ; act!=null ; act = act.Next )
            {
            compVal = act.Value.Key.CompareTo(key);
            if ( compVal>=0 )
                break;
            }
        if ( compVal==0 )
            return false;
        if ( act==null )
            list.AddLast(new Pair<TKey, TValue>(key,value));
        else
            list.AddBefore(act,new Pair<TKey, TValue>(key,value));
        return true;
        }

    public Pair<TKey, TValue> Find(TKey key)
        {
        int compVal;
        for ( var act=list.First ; act!=null ; act = act.Next )
            {
            compVal = act.Value.Key.CompareTo(key);
            if ( compVal==0 )
                return act.Value;
            if ( compVal>0 )   // niestety bez tego tez działa
                break;
            }
        return null;
        }

    public Pair<TKey, TValue> PopFront()
        {
        if ( Count == 0 )
            return null;
        var result = list.First.Value;
        list.RemoveFirst();
        return result;
        }

    }


public class SortedLinkedList<TKey, TValue> : IMap<TKey, TValue> where TKey : IComparable<TKey>
    {

    private class SortedLinkedListNode
        {
        public Pair<TKey, TValue> Pair { get; }
        public SortedLinkedListNode Next;
        public SortedLinkedListNode(TKey key,TValue value, SortedLinkedListNode next=null)
            {
            Pair = new Pair<TKey,TValue>(key,value);
            Next = next;
            }
        }

    private SortedLinkedListNode head;

    public int Count { get; private set; } = 0;

    public bool Add(TKey key, TValue value)
        {
        if ( Add(key, value, ref head) )
            {
            Count += 1;
            return true;
            }
        return false;
        }

    public Pair<TKey, TValue> Find(TKey key)
        {
        int compVal;
        for ( var act=head ; act!=null ; act = act.Next )
            {
            compVal = act.Pair.Key.CompareTo(key);
            if ( compVal==0 )
                return act.Pair;
            if ( compVal>0 )   // niestety bez tego tez działa
                break;
            }
        return null;
        }

    public Pair<TKey, TValue> PopFront()
        {
        if (head == null)
            return null;
        var result = head.Pair;
        head = head.Next;
        Count -= 1;
        return result;
        }

    private static bool Add(TKey key, TValue value, ref SortedLinkedListNode act)
        {
        if ( act is null )
            {
            act = new SortedLinkedListNode(key,value);
            return true;
            }
        int compVal = act.Pair.Key.CompareTo(key);
        if ( compVal<0 )
            return Add(key,value,ref act.Next);
        if ( compVal>0 )
            {
            act = new SortedLinkedListNode(key,value,act);
            return true;
            }
        return false;
        }

    }

    //public static class SortedLinkedListExtender
    //{
    //    public static SortedLinkedList<TValue, TKey> InvertPairs<TKey, TValue>(this SortedLinkedList<TKey, TValue> list)
    //        where TKey : IComparable<TKey>
    //        where TValue : IComparable<TValue>
    //    {
    //        var tab = new Pair<TKey, TValue>[list.Count];
    //        int idx = 0;
    //        while (list.Count != 0)
    //            tab[idx++] = list.PopFront();
    //        var result = new SortedLinkedList<TValue, TKey>();
    //        foreach (var p in tab)
    //        {
    //            list.Add(p.Key, p.Value);
    //            result.Add(p.Value, p.Key);
    //        }
    //        return result;
    //    }
    //}

}
