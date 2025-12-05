//#define STAGE_1
//#define STAGE_2
#define STAGE_3

namespace Lab05
{
    abstract class Figure
    {
        private Vector2 _position;
        protected Figure _parent;
        protected Figure[] _children;
        protected int _numberOfChildren;
        protected readonly int _id;
        static int _numberOfCreatedFigures = 0;

        public Figure(Vector2 position, int numberOfChildren = 2)
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

        public Vector2 GetRelativePosition()
        { 
            return _position;
        }

        public int GetMaxNumberOfChildren()
        {
            return _children.Length;
        }

        public Vector2 GetGlobalPosition()
        {
            if (_parent != null)
                return Vector2.Add(_parent.GetGlobalPosition(), _position);
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

        public void Move(Vector2 offset)
        {
            _position = Vector2.Add(_position, offset);
        }
    }

    class Circle : Figure
    {
        public float Radius;

        public Circle(Vector2 position, float radius) : base(position)
        {
            Radius = radius;
        }

        public override BoundingBox GetFigureBoundingBox()
        {
            var worldPosition = GetGlobalPosition();
            return new BoundingBox(Vector2.Add(worldPosition, new Vector2(-Radius)), Vector2.Add(worldPosition, new Vector2(Radius)));
        }

        public override string ToString()
        {
            var position = GetRelativePosition();
            #if STAGE_3
            position = GetGlobalPosition();
            #endif
            return $"Circle ID {_id}: Position in {position}, Radius={Radius}\n";
        }
    }

    class Triangle : Figure
    {
        public Vector2 V1;
        public Vector2 V2;
        public Vector2 V3;

        public Triangle(Vector2 position, Vector2 v1, Vector2 v2, Vector2 v3) : base(position)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public override BoundingBox GetFigureBoundingBox()
        {
            var worldPosition = GetGlobalPosition();
            return new BoundingBox(Vector2.Add(worldPosition,Vector2.Min(Vector2.Min(V1, V2), V3)),
                Vector2.Add(worldPosition, Vector2.Max(Vector2.Max(V1, V2), V3)));
        }

        public override string ToString()
        {
            var position = GetRelativePosition();
            #if STAGE_3
            position = GetGlobalPosition();
            #endif
            return $"Triangle ID {_id}: Position in {position}, Vertices in {V1}, {V2}, {V3}\n";
        }
    }
}
