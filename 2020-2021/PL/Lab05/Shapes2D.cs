using System;

namespace Lab05_pl
{
    abstract class Shape2D
    {
        public static int NumberOfCreatedObjects;
        protected readonly int _objectNumber;
        
        protected Shape2D()
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor Shape2D ({_objectNumber})");
        }

        protected abstract Shape2D Clone();
        public abstract double CalculateArea();
        public abstract void Scale(double ratio);
        
        public virtual string PrintShape2D()
        {
            return "Shape(Shape2D)";
        }

        public static Shape2D ScaleShape2D(Shape2D shape2D, double ratio)
        {
            // TODO: Implement method body
            Shape2D scaledShape2D = shape2D.Clone();
            scaledShape2D.Scale(ratio);
            return scaledShape2D;
            throw new NotImplementedException();
        }

        ~Shape2D()
        {
            Console.WriteLine($"Finalizer Shape2D ({_objectNumber})");
        }
    }

    class Circle : Shape2D
    {
        private double _radius;
        public Circle(double radius)
        {
            _radius = radius;

            Console.WriteLine($"Constructor Circle ({_objectNumber})");
        }

        //TODO: 
        // Stage 3
        public double CalculateCircuit()
        {
            return 2 * Math.PI * _radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * _radius * _radius;
        }

        protected override Shape2D Clone()
        {
            return new Circle(_radius);
        }

        public override void Scale(double ratio)
        {
            _radius *= ratio;
        }

        // TODO: 
        // Stage 3
        public double GetRadius() => _radius;

        public override string PrintShape2D()
        {
            return $"Circle(r={_radius})";
        }

        ~Circle()
        {
            Console.WriteLine($"Finalizer Circle ({_objectNumber})");
        }
    }


    ////Ask students to write Rectangle
    //class Rectangle : Shape2D
    //{
    //    public new static int NumberOfCreatedObjects;
    //    private new readonly int _objectNumber;

    //    public double XSide;
    //    public double YSide;

    //    // This is private constructor, not accessed externally
    //    // To avoid repetition of code when multiple constructors exists
    //    private Rectangle()
    //    {
    //        ++NumberOfCreatedObjects;
    //        _objectNumber = NumberOfCreatedObjects;

    //        // here we accessing protected field from base class
    //        Console.WriteLine($"Constructor Rectangle ({_objectNumber}) called");
    //    }

    //    // You need to use keyword -> this <- to call other constructor in the same class
    //    public Rectangle(double x, double y) : this()
    //    {
    //        Console.WriteLine($"Call Rectangle constructor with two parameters: ({x}, {y})");
    //        XSide = x;
    //        YSide = y;
    //    }

    //    public override double CalculateArea()
    //    {
    //        return XSide * YSide;
    //    }

    //    protected override Shape2D Clone()
    //    {
    //        return new Rectangle(XSide, YSide);
    //    }

    //    public override void Scale(double ratio)
    //    {
    //        XSide *= ratio;
    //        YSide *= ratio;
    //    }

    //    public override string PrintShape2D()
    //    {
    //        return $"Rectangle(a={XSide},b={YSide})";
    //    }

    //    ~Rectangle()
    //    {
    //        Console.WriteLine($"Finalizer Rectangle ({_objectNumber}) called");
    //    }
    //}
}
