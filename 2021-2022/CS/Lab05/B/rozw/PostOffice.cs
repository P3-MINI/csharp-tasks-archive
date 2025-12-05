using System;

namespace Lab5BEn
{
    public class PostOffice
    {
        private int _numOfPostBoxes;
        private readonly PostBox[] _postBoxes;

        public PostOffice(int size)
        {
            _postBoxes = new PostBox[size];
        }

        public bool AddPostBox(PostBox postBox)
        {
            if (_numOfPostBoxes >= _postBoxes.Length)
                return false;
            _postBoxes[_numOfPostBoxes++] = postBox;
            return true;
        }

        public int GetPostBoxesCount()
        {
            return _numOfPostBoxes;
        }

        public void Print()
        {
            Console.WriteLine($"Post office with {_numOfPostBoxes} post boxes\n\nPost boxes: ");
            for (var i = 0; i < _numOfPostBoxes; i++)
            {
                Console.WriteLine(i);
                _postBoxes[i].Print();
                Console.WriteLine();
            }
        }
    }
}