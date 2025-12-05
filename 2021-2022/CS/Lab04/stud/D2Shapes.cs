using System;

namespace Lab04_eng
{
    // abstract class can mix non implemented(abstract) methods and also implemented
    abstract class D2Shape
    {
        public static int NumberOfCreatedObjects;
        
        // Protected type or member can be accessed only by code in the same class, or in a class that is derived from that class
        // Public type or member can be accessed by any other code in the same assembly or another assembly that references it
        // Internal type or member can be accessed by any code in the same assembly, but not from another assembly
        protected readonly int _objectNumber;

        protected D2Shape()
        {
            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor D2Shape ({_objectNumber}) called");
        }

        // not implemented (abstract) methods
        // protected method available only in inherited classes
        protected abstract D2Shape Clone();
        public abstract double CalculateArea();
        public abstract void Scale(double ratio);

        // implemented base method that can be inherited and overwritten
        public virtual string PrintD2Shape()
        {
            return "Shape(D2Shape)";
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

    class Circle : D2Shape
    {
        public new static int NumberOfCreatedObjects;
        private new readonly int _objectNumber;
        public double Radius;

        public Circle(double radius)
        {
            Radius = radius;

            ++NumberOfCreatedObjects;
            _objectNumber = NumberOfCreatedObjects;
            Console.WriteLine($"Constructor Circle ({_objectNumber}) called");
        }

        public double CalculateCircuit()
        {
            return 2 * Math.PI * Radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }

        protected override D2Shape Clone()
        {
            return new Circle(Radius);
        }

        public override void Scale(double ratio)
        {
            Radius *= ratio;
        }

        public override string PrintD2Shape()
        {
            return $"Circle(r={Radius})";
        }

        ~Circle()
        {
            Console.WriteLine($"Finalizer Circle ({_objectNumber}) called");
        }
    }

    //TODO: Implement task 1
    //class Rectangle : D2Shape
    //{
    //}
}
