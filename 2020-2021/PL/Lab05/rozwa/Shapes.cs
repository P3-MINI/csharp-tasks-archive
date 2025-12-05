using System;

namespace Lab05_pl
{
    abstract class Shape2D
    {
        public static int NumberOfCreatedObjects;
        protected readonly int ObjectNumber;
        
        protected Shape2D()
        {
            ++NumberOfCreatedObjects;
            ObjectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Shape2D({ObjectNumber}) created");
        }

        public abstract double CalculateArea();
        
        public virtual string PrintShape2D()
        {
            return "Shape(Shape2D)";
        }

        ~Shape2D()
        {
            Console.WriteLine($"Shape2D ({ObjectNumber}) destroyed");
        }
    }

    class Circle : Shape2D
    {
        private readonly double _radius;

        public Circle(double radius)
        {
            _radius = radius;
            Console.WriteLine($"Circle({ObjectNumber}) with radius={radius} created");
        }
        public override double CalculateArea()
        {
            return Math.PI * _radius * _radius;
        }

        public override string PrintShape2D()
        {
            return $"Circle(r={_radius})";
        }

        ~Circle()
        {
            Console.WriteLine($"Circle ({ObjectNumber}) destroyed");
        }
    }

    abstract class Shape3D
    {
        protected readonly Shape2D BaseShape;
        protected readonly double Height;
        public static int NumberOfCreatedObjects;
        private readonly int ObjectNumber;

        protected Shape3D(Shape2D baseShape, double height)
        {
            BaseShape = baseShape;
            Height = height;
            ObjectNumber = ++NumberOfCreatedObjects;
        }

        public abstract double CalculateCapacity();

        public virtual string PrintShape3D()
        {
            return $"Shape3D with base {BaseShape.PrintShape2D()} and height: {Height}";
        }
    }

    class Cone : Shape3D
    {
        public new static int NumberOfCreatedObjects;
        private readonly int ObjectNumber;

        public Cone(Circle baseShape, double height) : base(baseShape, height)
        {
            ObjectNumber = ++NumberOfCreatedObjects;
        }

        public override double CalculateCapacity()
        {
            return (BaseShape.CalculateArea() * Height) / 3.0;
        }

        public override string PrintShape3D()
        {
            return $"Cone(h={Height}) with base: {BaseShape.PrintShape2D()}";
        }
    }

    class Cylinder : Shape3D
    {
        public new static int NumberOfCreatedObjects;
        private readonly int ObjectNumber;

        public Cylinder(Shape2D baseShape, double height) : base(baseShape, height)
        {
            ObjectNumber = ++NumberOfCreatedObjects;
        }

        public override double CalculateCapacity()
        {
            return BaseShape.CalculateArea() * Height;
        }

        public new string PrintShape3D()
        {
            return $"Cylinder(h={Height}) with base: {BaseShape.PrintShape2D()}";
        }
    }
}
