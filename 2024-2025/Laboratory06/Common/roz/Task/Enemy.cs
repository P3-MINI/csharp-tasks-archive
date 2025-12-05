using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Enemy
    {
        public Point Position {  get; private set; }
        //begin task 4
        Queue<Point> path;

        public Enemy(IEnumerable<Point> points)
        {
            path = new Queue<Point>(points);
            MoveNext();
        }

        public void MoveNext()
        {
            Position = path.Dequeue();
            path.Enqueue(Position);
        }
        //end task 4
    }
}
