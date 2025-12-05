using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab12
{
    public interface IInputSource
    {
        bool GetLine(Logger.GetLineEventArgs getLineEventArgs);
        void OnDetach();
        void OnAttach();
    }
}
