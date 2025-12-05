using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    abstract class Figure
    {
        private Vector3 _position;
        protected Figure _parent;
        protected Figure[] _children;
        protected int _numberOfChildren;
        protected readonly int _id;
        static int _numberOfCreatedFigures = 0;

        public Figure(Vector3 position, int numberOfChildren = 2)
        {
            _position = position;
            _children = new Figure[numberOfChildren];
            _numberOfChildren = 0;
            _id = _numberOfCreatedFigures++;
        }

        public void AddChild(Figure child)
        {
            if(_numberOfChildren < _children.Length)
            {              
                _children[_numberOfChildren++] = child;
                child._parent = this;
            }
            else
            {
                Array.Resize(ref _children, _children.Length * 2);
                _children[_numberOfChildren++] = child;
                child._parent = this;
            }
        }

        public Vector3 GetPosition()
        { 
            return _position;
        }

        public int GetMaxNumberOfChildren()
        {
            return _children.Length;
        }

        public Vector3 GetCartesianSpaceCoordinates()
        {
            if (_parent != null)
                return Vector3.Add(_parent.GetCartesianSpaceCoordinates(), _position);
            return _position;
        }

        public abstract BoundingBox GetFigureBoundingBox();
        public BoundingBox CalculateBoundingBox()
        {
            var boundingBox = GetFigureBoundingBox();
            for(int i = 0; i < _numberOfChildren; i++)
                boundingBox = BoundingBox.MergeBoundingBoxes(boundingBox,_children[i].CalculateBoundingBox());
            return boundingBox;
        }

        public string GetTreeString(int indentation = 0)
        {
            string objectString = new string('\t', indentation) + this.ToString();
            for(int i = 0; i < _numberOfChildren; i++)
                objectString += _children[i].GetTreeString(indentation + 1);
            return objectString;
        }

        public void Move(Vector3 offset)
        {
            _position = Vector3.Add(_position, offset);
        }
    }

    class Sphere : Figure
    {
        public float Radius;

        public Sphere(Vector3 position, float radius) : base(position)
        {
            Radius = radius;
        }

        public override BoundingBox GetFigureBoundingBox()
        {
            var worldPosition = GetCartesianSpaceCoordinates();
            return new BoundingBox(Vector3.Add(worldPosition, new Vector3(-Radius)), Vector3.Add(worldPosition, new Vector3(Radius)));
        }

        public override string ToString()
        {
            return $"Sphere ID {_id}: Position in {GetCartesianSpaceCoordinates()}, Radius={Radius}\n";
        }
    }

    class Cylinder : Figure
    {
        public float Radius;
        public float Height;

        public Cylinder(Vector3 position, float radius, float height) : base(position)
        {
            Radius = radius;
            Height = height;
        }

        public override BoundingBox GetFigureBoundingBox()
        {
            var worldPosition = GetCartesianSpaceCoordinates();
            return new BoundingBox(Vector3.Add(worldPosition, new Vector3(-Radius, -Radius, 0)), Vector3.Add(worldPosition, new Vector3(Radius, Radius, Height)));
        }

        public override string ToString()
        {
            return $"Cylinder ID {_id}: Position in {GetCartesianSpaceCoordinates()}, Radius={Radius}, Height={Height}\n";
        }
    }
}
