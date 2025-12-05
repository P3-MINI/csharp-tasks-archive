using System;

namespace Lab12
{
    public interface ILoggerSink
    {
        void OnLogMessage(string message, Severity severity);
        void OnAttach();
        void OnDetach();
    }
}