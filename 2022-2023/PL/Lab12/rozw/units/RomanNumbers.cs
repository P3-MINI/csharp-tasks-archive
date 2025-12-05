
using System;
using System.Collections.Generic;
using System.Text;

public struct RomanNumber
    {

    private static List<(int arabic, string roman,int next)> conversionTable = new List<(int arabic, string roman, int next)>()
        { (1000,"M",1000), (900,"CM",90), (500,"D",100), (400,"CD",90), (100,"C",100), (90,"XC",9), (50,"L",10), (40,"XL",9), (10,"X",100), (9,"IX",0), (5,"V",1), (4,"IV",0), (1,"I",1) };

    private static Dictionary<int,int> start = new Dictionary<int, int>();

    private int val;

    static RomanNumber()
        {
        for ( int i=0 ; i<conversionTable.Count ; ++i )
            start[conversionTable[i].arabic] = i;
        start[0] = conversionTable.Count;
        }

    public RomanNumber(int n)
        {
        if ( n<1 || n> 3999 ) throw new System.OverflowException();
        val = n;
        }

    public RomanNumber(string s)
        {
        val = 0;
        int count = 0;
        int i, ii;
        if ( s==null )
            throw new System.ArgumentException();
        s = s.Trim();
        if ( s==string.Empty )
            throw new System.ArgumentException();
        ii = i = 0;
        while ( s!=string.Empty && i<conversionTable.Count )
            {
            if ( s.StartsWith(conversionTable[i].roman) )
                {
                val += conversionTable[i].arabic;
                s = s.Substring(conversionTable[i].roman.Length);
                if ( i!=start[conversionTable[i].next] )
                    ii = start[conversionTable[i].next];
                else 
                    if ( ++count==3 )
                        ii = i+1;
                }
            else
                ii = i+1;
            if ( ii>i )
                {
                i = ii;
                count = 0;
                }
            }
        if ( s!=string.Empty )
            throw new System.ArgumentException();
        }

    public static implicit operator int(RomanNumber x) => x.val;

    public static RomanNumber operator+(RomanNumber x, RomanNumber y) => new RomanNumber(x.val+y.val);

    public static RomanNumber operator-(RomanNumber x, RomanNumber y) => new RomanNumber(x.val-y.val);

    public static RomanNumber operator*(RomanNumber x, RomanNumber y) => new RomanNumber(x.val*y.val);

    public static RomanNumber operator/(RomanNumber x, RomanNumber y) 
        {
        if ( x.val%y.val!=0 )
            throw new System.ArithmeticException();
        return new RomanNumber(x.val/y.val);
        }

    public static bool operator==(RomanNumber x, RomanNumber y) => x.val==y.val;

    public static bool operator!=(RomanNumber x, RomanNumber y) => x.val!=y.val;

    public static bool operator>(RomanNumber x, RomanNumber y) => x.val>y.val;

    public static bool operator<(RomanNumber x, RomanNumber y) => x.val<y.val;

    public static bool operator>=(RomanNumber x, RomanNumber y) => x.val>=y.val;

    public static bool operator<=(RomanNumber x, RomanNumber y) => x.val<=y.val;

    public override bool Equals(object obj) => this==(RomanNumber)obj;

    public override int GetHashCode() => val.GetHashCode();

    public override string ToString()
        {
        int v = val;
        StringBuilder sb = new StringBuilder();
        for ( int i=0 ; i<conversionTable.Count ; ++i )
            while ( v>=conversionTable[i].arabic && v>0 )
                {
                v -= conversionTable[i].arabic;
                sb.Append(conversionTable[i].roman);
                }
        return sb.ToString();
        }

    }