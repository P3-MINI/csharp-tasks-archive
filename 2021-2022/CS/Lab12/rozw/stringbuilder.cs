
using System;
using System.Text;

class Test
    {

    public static void Main()
        {
        DateTime t1, t2;

        string s = "";
        t1 = DateTime.Now;
        for ( int i=0 ; i<200000 ; ++i ) // 200 thousand
            s += "a";
        t2 = DateTime.Now;
        Console.WriteLine("{0,7:0.000}",(t2-t1).TotalSeconds);

        StringBuilder sb = new StringBuilder();
        t1 = DateTime.Now;
        for ( int i=0 ; i<200000000 ; ++i ) // 200 million
            sb.Append("a");
        t2 = DateTime.Now;
        Console.WriteLine("{0,7:0.000}",(t2-t1).TotalSeconds);
        }

    }