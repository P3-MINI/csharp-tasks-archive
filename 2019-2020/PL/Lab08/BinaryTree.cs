using System;

namespace PL_Lab_08
{

public class BinaryTree<TKey, TValue> : IMap<TKey, TValue> where TKey : IComparable<TKey>
    {

    private class BinaryTreeNode
        {
        public Pair<TKey, TValue> Pair { get; }
        public BinaryTreeNode leftNode;
        public BinaryTreeNode rightNode;
        public BinaryTreeNode(TKey key, TValue value)
            {
            Pair = new Pair<TKey, TValue>(key, value);
            }
        }

    private BinaryTreeNode root;

    public int Count { get; private set; } = 0;

    public bool Add(TKey key, TValue value)
        {
        if ( Add(key, value, ref root) )
            {
            Count += 1;
            return true;
            }
        return false;
        }

    public Pair<TKey, TValue> Find(TKey key)
        {
        int compVal;
        for ( var act=root ; act!=null ; act = compVal<0 ? act.leftNode : act.rightNode )
            {
            compVal = act.Pair.Key.CompareTo(key);
            if ( compVal==0 )
                return act.Pair;
            }
        return null;
        }

    private static bool Add(TKey key, TValue value, ref BinaryTreeNode act)
        {
        if ( act==null )
            {
            act = new BinaryTreeNode(key,value);
            return true;
            }
        int compVal = act.Pair.Key.CompareTo(key);
        if ( compVal==0 )
            return false;
        return compVal<0 ? Add(key,value, ref act.leftNode) : Add(key,value, ref act.rightNode) ;
        }

    }
}
