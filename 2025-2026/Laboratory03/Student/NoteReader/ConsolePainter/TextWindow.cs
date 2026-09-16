using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePainter
{
    internal class TextWindow : CoolWindow
    {
        private IDataSource<string> source;
        private int lineCount;
        public TextWindow((int x, int y) start, (int x, int y) end, IDataSource<string> source) : base(start, end)
        {
            Clear();
            SetSource(source);
        }
        public void SetSource(IDataSource<string> source)
        {
            if (this.source != null)
            {
                source.DataChanged -= OnSourceChanged;
            }
            this.source = source;
            source.DataChanged += OnSourceChanged;
            OnSourceChanged(null, EventArgs.Empty);
        }
        private void OnSourceChanged(object? sender, EventArgs e)
        {
            lineCount = source.Count;
            scroll = Math.Min(scroll, lineCount - EndContent.y + StartContent.y - 1);
            scroll = Math.Max(scroll, 0);
            DrawContent();
        }

        public void Clear()
        {
            Guard.Begin();
            for (int y = start.y; y <= end.y; y++)
            {
                Console.SetCursorPosition(start.x, y);
                Console.Write(new string(' ', end.x - start.x + 1));
            }
            Guard.End();
        }
        protected override void DrawContent()
        {
            int len = EndContent.x - StartContent.x + 1;
            Guard.Begin();
            int i = 0;
            foreach (var line in source.Data)
            {
                if (i < scroll)
                {
                    i++;
                    continue;
                }
                if (i >= EndContent.y - StartContent.y + 1 + scroll)
                    break;
                Console.SetCursorPosition(StartContent.x, StartContent.y + i - scroll);
                Console.Write(String.Concat(line.PadRight(len).Take(len)));
                i++;
            }
            for (; i < EndContent.y - StartContent.y + 1 + scroll; i++)
            {
                Console.SetCursorPosition(StartContent.x, StartContent.y + i);
                Console.Write(String.Concat("".PadRight(len).Take(len)));
            }
            Guard.End();
        }
        public void ScrollUp()
        {
            if (scroll - 1 >= 0)
                scroll--;
            DrawContent();
        }
        public void ScrollDown()
        {
            if (scroll + 1 < lineCount - EndContent.y + StartContent.y)
                scroll++;
            DrawContent();
        }
        private int scroll = 0;
    }
}
