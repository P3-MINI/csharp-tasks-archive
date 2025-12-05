using System;

namespace Lab05_pl
{
    abstract class Shape3D
    {
        public static int NumberOfCreatedObjects;
        protected readonly int _objectNumber;

        protected readonly Shape2D BaseShape2D;
        protected readonly int _height;

        // TODO: Implement constructor to meet task conditions
        protected Shape3D(Shape2D baseShape2D, int height)
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor Shape3D ({_objectNumber})");
            BaseShape2D = baseShape2D;
            _height = height;
        }

        //TODO: Implement missing methods
        // Code below should be commented
        public abstract double CalculateCapacity();

        public abstract double CalculateArea();

        public virtual string PrintShape3D()
        {
            return $"Shape3D with base {BaseShape2D.PrintShape2D()} and height: {_height}";
        }

        ~Shape3D()
        {
            Console.WriteLine($"Finalizer Shape3D ({_objectNumber})");
        }
    }

    class Cylinder : Shape3D
    {
        public new static int NumberOfCreatedObjects;
        private new readonly int _objectNumber;

        public Cylinder(Circle baseShape, int height) : base(baseShape, height)
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor Cylinder ({_objectNumber})");
        }

        public override double CalculateCapacity()
        {
            //TODO: Implement method body
            // Pi * r^2 * h
            return BaseShape2D.CalculateArea() * _height;
        }

        public override double CalculateArea()
        {
            //TODO: Implement method body
            //To do so add extra method to `Circle` class `CalculateCircuit`
            // And use it in implementation of this method
            // 2*Pi*r^2*h + 2*Pi*r*h
            return 2 * BaseShape2D.CalculateArea() + ((Circle)BaseShape2D).CalculateCircuit() * _height;
        }

        public override string PrintShape3D()
        {
            //TODO: Implement method body
            //If possible reuses Shape2D printout
            return $"Cylinder(h={_height}) and base: {BaseShape2D.PrintShape2D()}";
        }

        ~Cylinder()
        {
            Console.WriteLine($"Finalizer Cylinder ({_objectNumber})");
        }
    }

    class Cone : Shape3D
    {
        public new static int NumberOfCreatedObjects;
        private new readonly int _objectNumber;

        public Cone(Circle circle, int height) : base(circle, height)
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;

            Console.WriteLine($"Constructor Cone ({_objectNumber})");
        }

        public override double CalculateCapacity()
        {

            return ((Circle)BaseShape2D).CalculateArea() * _height / 3;
        }

        public override double CalculateArea()
        {

            var r = ((Circle)BaseShape2D).GetRadius();
            var s = Math.Sqrt(r * r + _height * _height);
            return Math.PI * r * s + BaseShape2D.CalculateArea();
        }

        // New keyword hide base method
        public new string PrintShape3D()
        {
            return $"Cone(h={_height}) and base: {BaseShape2D.PrintShape2D()}";
        }

        ~Cone()
        {
            Console.WriteLine($"Finalizer Cone ({_objectNumber})");
        }
    }

    //As students to implement Cuboid
    //class Cuboid : Shape3D
    //{
    //    public new static int NumberOfCreatedObjects;
    //    private new readonly int _objectNumber;

    //    public Cuboid(Rectangle baseShape, int height) : base(baseShape, height)
    //    {
    //        ++NumberOfCreatedObjects;
    //        _objectNumber = NumberOfCreatedObjects;

    //        Console.WriteLine($"Constructor Cuboid ({NumberOfCreatedObjects}) called");
    //    }

    //    public override double CalculateCapacity()
    //    {
    //        return BaseShape2D.CalculateArea() * _height;
    //    }

    //    public override double CalculateArea()
    //    {
    //        var rectangle = (Rectangle)BaseShape2D;
    //        return 2 * (rectangle.XSide * _height + rectangle.YSide * _height + rectangle.CalculateArea());
    //    }

    //    // Broken inheritance. This methods hides base method
    //    public new string PrintShape3D()
    //    {
    //        var rectangle = (Rectangle)BaseShape2D;
    //        return $"Cuboid({rectangle.XSide}, {rectangle.YSide}, {_height})";
    //    }

    //    ~Cuboid()
    //    {
    //        Console.WriteLine($"Finalizer Cuboid ({_objectNumber}) called");
    //    }
    //}
}
