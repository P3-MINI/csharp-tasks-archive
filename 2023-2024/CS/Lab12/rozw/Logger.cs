using System;
using System.Collections.Generic;

namespace Lab12
{
    public class Logger
    {
        #region STAGE_1
        public delegate void LogEvent(string message, Severity severity = Severity.Normal);
        private event LogEvent _logEventHandler;


        public void AttachSink(ILoggerSink loggerink)
        {
            _logEventHandler += loggerink.OnLogMessage;
            loggerink.OnAttach();
        }

        public void DetachSink(ILoggerSink loggerSink)
        {
            _logEventHandler -= loggerSink.OnLogMessage;
            loggerSink.OnDetach();
        }
        public void HandleMessage(string message, Severity severity)
        {
            Console.WriteLine($"Severity: {severity}, message: {message}");
            _logEventHandler?.Invoke(message, severity);
        }
        #endregion

        #region STAGE_2
        public delegate bool GetLine(GetLineEventArgs e);
        private event GetLine _getLineHandler;

        public void AttachInputSource(IInputSource inputSource)
        {
            _getLineHandler += inputSource.GetLine;
            inputSource.OnAttach();
        }

        public void DetachInputSource(IInputSource inputSource)
        {
            _getLineHandler -= inputSource.GetLine;
            inputSource.OnDetach();
        }

        public void ProcessNextLines(int linesCount)
        {
            GetLineEventArgs e = new GetLineEventArgs();
            for (int i = 0; i < linesCount; i++)
            {
                _getLineHandler?.Invoke(e);
            }

            foreach (var line in e.lines)
            {
                HandleMessage(line, line.Length < 100 ? Severity.Normal : Severity.Debug);
            }
        }

        public class GetLineEventArgs
        {
            public bool IsEmpty
            {
                get;
                private set;
            } = true;

            internal List<string> lines = new List<string>();
            public void AddLine(string line)
            {
                if (line != null && line.Length > 0)
                {
                    lines.Add(line);
                    IsEmpty = false;
                }
            }
        }

        #endregion
        
    }
}