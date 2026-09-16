using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePainter
{
    internal interface IDataSource<T>
    {
        string Name { get; }
        IEnumerable<T> Data { get; }
        int Count { get; }
        event EventHandler? DataChanged;
    }
    internal class EmptySource<T> : IDataSource<T>
    {
        private EmptySource(){}
        public static EmptySource<T> Empty { get; } = new EmptySource<T>(); 
        public string Name => "";

        public IEnumerable<T> Data => [];

        public int Count => 0;

        public event EventHandler? DataChanged
        {
            add { }
            remove { }
        }
    }
}
