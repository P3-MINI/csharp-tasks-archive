using System;

namespace Lab04_eng
{
    abstract class D3Shape
    {
        // Hold number of all created D3Shapes
        public static int NumberOfCreatedObjects;
        protected readonly int _objectNumber;
        
        // Readonly fields can't be changed after initialization
        protected readonly D2Shape _baseShape;
        protected int _height;

        private D3Shape()
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor D3Shape ({_objectNumber}) called");
        }

        // calling another constructor from the same class by `this`
        protected D3Shape(D2Shape baseShape, int height) : this()
        {
            _baseShape = baseShape;
            _height = height;
        }

        public abstract double CalculateCapacity();

        public abstract double CalculateArea();

        // Can't override it because it is not marked as virtual
        public void ChangeHeight(int newHeight)
        {
            _height = newHeight;
        }

        public virtual string PrintD3Shape()
        {
            return $"{_baseShape.PrintD2Shape()} with height {_height}";
        }

        ~D3Shape()
        {
            Console.WriteLine($"Finalizer D3Shape ({_objectNumber}) called");
        }
    }

    class Cylinder : D3Shape
    {
        // Overriden static field
        // Calculate number of all created Cylinders
        public new static int NumberOfCreatedObjects;
        // Hold number of created objects Cylinder
        private new readonly int _objectNumber;

        public Cylinder(Circle baseShape, int height): base(baseShape, height)
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor Cylinder ({_objectNumber}) called");
        }

        // sealed used here prevents derived class from overriding
        public sealed override double CalculateCapacity()
        {
            return _baseShape.CalculateArea() * _height;
        }

        // The same as if it has `new` keyword - hides base method
        // But right now compiler will complain to add it
        //public void ChangeHeight(int height)
        //{
        //}

        public override double CalculateArea()
        {
            return 2 * _baseShape.CalculateArea() + ((Circle) _baseShape).CalculateCircuit() * _height;
        }

        public override string PrintD3Shape()
        {
            return $"Cylinder with height= {_height} and base: {_baseShape.PrintD2Shape()}";
        }

        ~Cylinder()
        {
            Console.WriteLine($"Finalizer Cylinder ({_objectNumber}) called");
        }
    }

    class Cone : D3Shape
    {
        public new static int NumberOfCreatedObjects;
        private new readonly int _objectNumber;

        public Cone(Circle circle, int height) : base(circle, height)
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;

            Console.WriteLine($"Constructor Cone ({_objectNumber}) called");
        }

        public override double CalculateCapacity()
        {
            return ((Circle) _baseShape).CalculateArea() * _height / 3;
        }

        public override double CalculateArea()
        {
            var r = ((Circle) _baseShape).Radius;
            var s = Math.Sqrt(r * r + _height * _height);
            return Math.PI * r * s + _baseShape.CalculateArea();
        }

        // New keyword hide base method
        public new string PrintD3Shape()
        {
            return $"Cone with height= {_height} and base: {_baseShape.PrintD2Shape()}";
        }

        ~Cone()
        {
            Console.WriteLine($"Finalizer Cone ({_objectNumber}) called");
        }
    }

    //As students to implement Cuboid
    class Cuboid : D3Shape
    {
        public new static int NumberOfCreatedObjects;
        private new readonly int _objectNumber;

        public Cuboid(Rectangle baseShape, int height) : base(baseShape, height)
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;

            Console.WriteLine($"Constructor Cuboid ({NumberOfCreatedObjects}) called");
        }

        public override double CalculateCapacity()
        {
            return _baseShape.CalculateArea() * _height;
        }

        public override double CalculateArea()
        {
            var rectangle = (Rectangle)_baseShape;
            return 2 * (rectangle.XSide * _height + rectangle.YSide * _height + rectangle.CalculateArea());
        }

        // Broken inheritance. This methods hides base method
        public new string PrintD3Shape()
        {
            var rectangle = (Rectangle)_baseShape;
            return $"Cuboid({rectangle.XSide}, {rectangle.YSide}, {_height})";
        }

        ~Cuboid()
        {
            Console.WriteLine($"Finalizer Cuboid ({_objectNumber}) called");
        }
    }
}
