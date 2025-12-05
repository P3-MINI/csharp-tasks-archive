
using System;

class BitArray
    {
    private int[] bits;
    private int length;

    public BitArray(int length)
        {
        this.length = length;
        bits = new int[((length-1)>>5)+1];
        return;
        }

    public int Lenght
        {
        get { return length; }
        }

    public bool this[int i]
        {
        // Note: if the first argument of << operator is an integer number then the second argument of this operator
        //       is computed modulo size (in bits) of the first operator's argument
        get {
            return ( bits[i>>5] & (1<<i) ) != 0 ;
            }
        set {
            if ( value )
                bits[i>>5] |= (1<<i);
            else
                bits[i>>5] &= ~(1<<i);
            }
         }

    }  //  class BitArray


class BitArray8
    {
    private int[] bits;
    private int length;

    public BitArray8(int length)
        {
        this.length = length;
        bits = new int[((length-1)>>5)+1];
        return;
        }

    public int Lenght
        {
        get { return length; }
        }

    public bool this[Index idx]
        {
        // Note: if the first argument of << operator is an integer number then the second argument of this operator
        //       is computed modulo size (in bits) of the first operator's argument
        get {
            int i = idx.IsFromEnd ? length-idx.Value : idx.Value ;
            return ( bits[i>>5] & (1<<i) ) != 0 ;
            }
        set {
            int i = idx.IsFromEnd ? length-idx.Value : idx.Value ;
            if ( value )
                bits[i>>5] |= (1<<i);
            else
                bits[i>>5] &= ~(1<<i);
            }
         }

    public BitArray8 this[Range r]
        {
        get {
            int b = r.Start.IsFromEnd ? length-r.Start.Value : r.Start.Value ;
            int e = r.End.IsFromEnd ? length-r.End.Value : r.End.Value ;
            BitArray8 res = new BitArray8(e-b);
            for ( int i=0 ; i<res.Lenght ; ++i )
                res[i] = this[b+i];
            return res;
            }
         }

    }  //  class BitArray


class Chessboard
    {
    private sbyte[,] board = new sbyte[8,8];

    public sbyte this[char c, int r]
        {
        get { return board[r-1,c-'a']; }
        set { board[r-1,c-'a']=value; }
        }
 
    public sbyte this[string p]
        {
        get { return board[p[1]-'1',p[0]-'a']; }
        set { board[p[1]-'1',p[0]-'a'] = value; }
        }
 
    }  //  class Chess


struct Complex
    {
    private double re;
    private double im;

    public double Re
        {
        get { return re; }
        set { re = value; }
        }

    public double Im
        {
        get { return im; }
        set { im = value; }
        }

    public double this[int i]
        {
        get {
            switch ( i )
                {
                case 1: return re;
                case 2: return im;
                default: throw new IndexOutOfRangeException();
                }
            }   
        set {
            switch ( i )
                {
                case 1: re = value; return;
                case 2: im = value; return;
                default: throw new IndexOutOfRangeException();
                }
            }
        }

    }  //  struct Complex


class Test
    {

    static void Main()
        {
        BitArray myBits = new BitArray(40);
        myBits[33] = true;
        Console.WriteLine("{0}",myBits[22]);  //  false
        Console.WriteLine("{0}",myBits[33]);  //  true
        Console.WriteLine();

        BitArray8 myBits8 = new BitArray8(40);
        myBits8[33] = true;
        myBits8[^5] = true;
        Console.WriteLine("{0}",myBits8[33]);  //  true
        Console.WriteLine("{0}",myBits8[34]);  //  false
        Console.WriteLine("{0}",myBits8[35]);  //  true
        Console.WriteLine();
        Console.WriteLine("{0}",myBits8[^7]);  //  true    - the same as for index 33
        Console.WriteLine("{0}",myBits8[^6]);  //  false   - the same as for index 34
        Console.WriteLine("{0}",myBits8[^5]);  //  true    - the same as for index 35
        Console.WriteLine();
        BitArray8 myBits8r;
        myBits8r = myBits8[33..^4];
        Console.WriteLine("{0}",myBits8r[0]);  //  true
        Console.WriteLine("{0}",myBits8r[1]);  //  false
        Console.WriteLine("{0}",myBits8r[2]);  //  true
        Console.WriteLine();

        myBits8r = myBits8[11..];    // notational shortcuts
        myBits8r = myBits8[..^23];   // automatically
        myBits8r = myBits8[..];      // can be used

        Chessboard chessboard = new Chessboard();
        chessboard['e',4] = 1;
        Console.WriteLine("{0}",chessboard["e4"]);  //  1

        Complex c = new Complex();
        c.Re = 5.0;
        c[2] = 10.0;
        Console.WriteLine("{0}  {1}", c[1], c.Im);
        }

    }  //  class Test
