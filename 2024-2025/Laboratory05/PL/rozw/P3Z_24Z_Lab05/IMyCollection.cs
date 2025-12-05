using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3Z_24Z_Lab05
{
    public interface IMyCollection<T> : IEnumerable<T>
    {
        public int Count { get; }
        public void Add(T item);
    }
}
