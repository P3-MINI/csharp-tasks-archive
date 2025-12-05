using System;

namespace Lab05_pl
{
    abstract class Shape2D
    {
        protected readonly int ObjectNumber;
        public static int NumberOfCreatedObjects;

        protected Shape2D()
        {
            ObjectNumber = ++NumberOfCreatedObjects;
            Console.WriteLine($"Shape2D ({ObjectNumber}) created");
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

    class Rectangle : Shape2D
    {
        private readonly double _x;
        private readonly double _y;

        public Rectangle(double x, double y)
        {
            _x = x;
            _y = y;
            Console.WriteLine($"Rectangle ({ObjectNumber}) with x={x}, y={y} created");
        }

        public override double CalculateArea()
        {
            return _x * _y;
        }

        public override string PrintShape2D()
        {
            return $"Rectangle(x={_x},y={_y})";
        }

        ~Rectangle()
        {
            Console.WriteLine($"Rectangle ({ObjectNumber}) destroyed");
        }
    }

    abstract class Shape3D
    {
        protected readonly Shape2D BaseShape;
        protected readonly double Height;
        private readonly int ObjectNumber;
        public static int NumberOfCreatedObjects;

        protected Shape3D(Shape2D baseShape, double height)
        {
            ObjectNumber = ++NumberOfCreatedObjects;
            BaseShape = baseShape;
            Height = height;
        }

        public abstract double CalculateCapacity();

        public virtual string PrintShape3D()
        {
            return $"Shape3D with base {BaseShape.PrintShape2D()} and height: {Height}";
        }
    }

    class Cuboid : Shape3D
    {
        private readonly int ObjectNumber;
        public new static int NumberOfCreatedObjects;

        public Cuboid(Rectangle baseShape, double height) : base(baseShape, height)
        {
            ObjectNumber = ++NumberOfCreatedObjects;
        }

        public override double CalculateCapacity()
        {
            return BaseShape.CalculateArea() * Height;
        }

        public override string PrintShape3D()
        {
            return $"Cuboid(h={Height}) with base: {BaseShape.PrintShape2D()}";
        }
    }

    class Pyramid : Shape3D
    {
        private readonly int ObjectNumber;
        public new static int NumberOfCreatedObjects;

        public Pyramid(Rectangle baseShape, double height) : base(baseShape, height)
        {
            ObjectNumber = ++NumberOfCreatedObjects;
        }

        public override double CalculateCapacity()
        {
            return (BaseShape.CalculateArea() * Height)/3.0;
        }

        public new string PrintShape3D()
        {
            return $"Pyramid(h={Height}) with base: {BaseShape.PrintShape2D()}";
        }
    }
}
