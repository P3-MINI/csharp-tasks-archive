using System;

namespace Lab03_eng
{
    // abstract class can mix non implemented(abstract) methods and also implemented
    abstract class D2Shape
    {
        public static int NumberOfCreatedObjects;
        
        // Protected type or member can be accessed only by code in the same class, or in a class that is derived from that class
        protected int _objectNumber;
        public string ExampleOfPublicField = "Public type or member can be accessed by any other code in the same assembly or another assembly that references it";
        internal string ExampleOfInternalField = "Internal type or member can be accessed by any code in the same assembly, but not from another assembly";
        
        protected D2Shape()
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor D2Shape ({NumberOfCreatedObjects}) called");
        }

        // not implemented (abstract) methods
        // protected method available only in inherited classes
        protected abstract double CalculateArea();
        public abstract D2Shape Clone();
        public abstract void Scale(double ratio);

        // implemented base method that can be inherited and overwritten
        public virtual string PrintShape()
        {
            return $"Shape(D2Shape) area = {CalculateArea()}";
        }

        // Static method that creates concrete object but declares return base type
        // Factory method pattern
        public static D2Shape ScaleD2Shape(D2Shape d2Shape, double ratio)
        {
            D2Shape scaledShape = d2Shape.Clone();
            scaledShape.Scale(ratio);
            return scaledShape;
        }

        // Why do we can override if it is base class there is no inheritance?
        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        // Finalizers of the object
        ~D2Shape()
        {
            Console.WriteLine($"Finalizer D2Shape ({_objectNumber}) called");
        }
    }

    class RectangularTriangle : D2Shape
    {
        private double _a;
        private double _h;

        public RectangularTriangle(double a, double h)
        {
            _a = a;
            _h = h;
            // here we accessing protected field from base class
            Console.WriteLine($"Constructor RectangularTriangle ({NumberOfCreatedObjects}) called");
        }

        protected override double CalculateArea()
        {
            return 0.5 * _a * _h;
        }

        public override D2Shape Clone()
        {
            return new RectangularTriangle(_a, _h);
        }

        public override void Scale(double ratio)
        {
            _a *= ratio;
            _h *= ratio;
        }
        
        public override string PrintShape()
        {
            return $"Area of RectangularTriangle ({_a},{_h}) is = {CalculateArea()}";
        }

        ~RectangularTriangle()
        {
            Console.WriteLine($"Finalizer RectangularTriangle ({_objectNumber}) called");
        }
    }

    class Rectangle : D2Shape
    {
        protected double _xSide;
        protected double _ySide;

        // This is private constructor, not accessed externally
        // To avoid repetition of code when multiple constructors exists
        private Rectangle()
        {
            // here we accessing protected field from base class
            Console.WriteLine($"Constructor Rectangle ({NumberOfCreatedObjects}) called");
        }

        // You need to use keyword -> this <- to call other constructor in the same class
        public Rectangle(double x) : this(x, x)
        {
            Console.WriteLine($"Call Rectangle constructor with single parameter: {x}");
        }

        // You need to use keyword -> this <- to call other constructor in the same class
        public Rectangle(double x, double y) : this()
        {
            Console.WriteLine($"Call Rectangle constructor with two parameters: ({x}, {y})");
            _xSide = x;
            _ySide = y;
        }

        protected override double CalculateArea()
        {
            return _xSide * _ySide;
        }

        public override D2Shape Clone()
        {
            return new Rectangle(_xSide, _ySide);
        }

        public override void Scale(double ratio)
        {
            _xSide *= ratio;
            _ySide *= ratio;
        }

        public override string PrintShape()
        {
            return $"Area of Rectangle ({_xSide},{_ySide}) is = {CalculateArea()}";
        }

        ~Rectangle()
        {
            Console.WriteLine($"Finalizer Rectangle ({_objectNumber}) called");
        }
    }


    // Ask students to write Circle class -> Implementation below
    // Let's extend it adding method to calculate Circuit
    class Circle : D2Shape
    {
        private double _radius;

        public Circle(double radius)
        {
            _radius = radius;
            
            Console.WriteLine($"Constructor Circle ({NumberOfCreatedObjects}) called");
        }

        public double CalculateCircuit()
        {
            return 2 * Math.PI * _radius;
        }

        protected override double CalculateArea()
        {
            return Math.PI * _radius * _radius;
        }

        public override D2Shape Clone()
        {
            return new Circle(_radius);
        }

        public override void Scale(double ratio)
        {
            _radius *= ratio;
        }

        public override string PrintShape()
        {
            return $"Circle area ({_radius}) is ={CalculateArea()}";
        }

        ~Circle()
        {
            Console.WriteLine($"Finalizer Circle ({_objectNumber}) called");
        }
    }
}
