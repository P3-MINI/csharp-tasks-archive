using System;

namespace Lab12
{
    public interface ILoggerSink
    {
        void OnLogMessage(string message, Severity severity);
        void OnAttach();
        void OnDetach();
    }

    internal class ColorLoggerSink : ILoggerSink
    {
        public void OnLogMessage(string message, Severity severity)
        {
            Console.ForegroundColor = severity switch
            {
                Severity.Debug => ConsoleColor.Blue,
                Severity.Normal => ConsoleColor.White,
                Severity.High => ConsoleColor.Yellow,
                Severity.Critical => ConsoleColor.Red,
                _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null),
            };
            Console.WriteLine($"{DateTime.Now}, Severity: {severity}, Message: {message}");
            Console.ResetColor();
        }

        public void OnAttach()
        {
            Console.WriteLine($"{DateTime.Now}, Color logger attaching");
        }

        public void OnDetach()
        {
            Console.WriteLine($"{DateTime.Now}, Color logger detaching");
        }
    }

    internal class ConditionalLoggerSink : ILoggerSink
    {
        private Func<string, Severity, bool> _condition;

        public ConditionalLoggerSink(Func<string, Severity, bool> condition)
        {
            this._condition = condition;
        }

        public void OnLogMessage(string message, Severity severity)
        {
            if (_condition.Invoke(message, severity))
            {
                Console.WriteLine("--------------");
                Console.WriteLine($"Sample log: {message}");
                Console.WriteLine("--------------");
            }
        }

        public void OnAttach()
        {
            Console.WriteLine($"{DateTime.Now}, Conditional logger attaching");
        }

        public void OnDetach()
        {
            Console.WriteLine($"{DateTime.Now}, Conditional logger detaching");
        }
    }
}