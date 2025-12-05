
using System;

class Test
    {

    public struct Point2
        {
        private double x;
        private double y;
        public Point2(double px, double py) { x=px; y=py; }
        public void Deconstruct(out double px, out double py) { px=x; py=y; }
        }

    public static void Main()
        {

        // declaration of tuple nx (with fields n of type int and x of type double) and its initialization
        (int n, double x) nx = (1,2.5);

        // declaration of two separate variables n1 and x1 with explicitly specified types
        // (these variables exist and can be used in the next part of Main method)
        // and their initialization by tuple (1,2.5) deconstruction
        (int n1, double x1) = (1,2.5);

        // declaration of two separate variables n2 and x2 with explicitly specified types
        // and their initialization by tuple nx deconstruction
        (int n2, double x2) = nx;

        // declaration of two separate variables n3 and x3 with implicit types
        // and their initialization by tuple nx deconstruction
        (var n3, var x3) = nx;

        // simplified declaration of two separate variables n4 and x4 with implicit types
        // and their initialization by tuple nx deconstruction
        var (n4, x4) = nx;

        // "ordinary" declaration of variables n5 i x5
        int n5;
        double x5;
        // asignment to both variables n5 i x5 by tuple nx deconstruction
        (n5, x5) = nx;

        // declaration of two separate variables n6 and x6 (first with explicitly specified type, second with implicit type)
        // and their initialization by tuple nx deconstruction
        (int n6, var x6) = nx;

        // declaration of variable p2 of type Point2 (not a tuple type)
        Point2 p2 = new Point2(1.0, 2.5);
        
        // deconstruction of p2 object by implicit invocation of Deconstruct method
        var (xx, yy) = p2;
        }

    }

