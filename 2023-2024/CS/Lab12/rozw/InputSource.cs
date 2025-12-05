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

    public class FileSource : IInputSource
    {
        private int currentLine = 0;
        private List<string> _lines;
        private string _filePath;
        public FileSource(string filePath)
        {
            _filePath = filePath;
            _lines = new List<string>(File.ReadAllLines(_filePath));
        }
        public bool GetLine(Logger.GetLineEventArgs getLineEventArgs)
        {
            if (currentLine < _lines.Count)
            {
                getLineEventArgs.AddLine(_lines[currentLine]);
                currentLine++;
                return true;
            }
            return false;
        }

        public void OnDetach()
        {
            Console.WriteLine("Attaching file source");
            Console.WriteLine($"Path: {_filePath}");
            Console.WriteLine("Attached");
        }

        public void OnAttach()
        {
            Console.WriteLine("Detaching file source");
            Console.WriteLine($"Path: {_filePath}");
            Console.WriteLine("Detached");
        }
    }

    public class RandomSource : IInputSource
    {
        private StringBuilder builder = new StringBuilder();
        private Random _random;
        private int _minLength;
        private int _maxLength;

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public RandomSource(int minLength, int maxLength)
        {
            if (minLength < 0 || minLength >= maxLength)
            {
                throw new ArgumentException("Min length must be >= 0 and < than max length");
            }
            _maxLength = maxLength;
            _minLength = minLength;
            _random = new Random();
        }

        public bool GetLine(Logger.GetLineEventArgs getLineEventArgs)
        {
            var len = _random.Next(_minLength, _maxLength);
            for (int i = 0; i < len; i++)
            {
                builder.Append(chars[_random.Next(chars.Length)]);
            }
            builder.Append("\n");
            getLineEventArgs.AddLine(builder.ToString());
            builder.Clear();
            return true;
        }

        public void OnAttach()
        {
            Console.WriteLine("Attaching random source");
            Console.WriteLine($"Chars array: {chars}");
            Console.WriteLine($"Attached");
        }

        public void OnDetach()
        {
            Console.WriteLine("Detaching random source");
            Console.WriteLine("Detached");
        }
    }
}
