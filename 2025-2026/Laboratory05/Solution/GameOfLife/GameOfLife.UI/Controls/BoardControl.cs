using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace GameOfLife.UI.Controls;

public class BoardControl : Control
{
    public static readonly StyledProperty<bool[,]?> BoardStateProperty =
        AvaloniaProperty.Register<BoardControl, bool[,]?>(nameof(BoardState));

    public bool[,]? BoardState
    {
        get => GetValue(BoardStateProperty);
        set => SetValue(BoardStateProperty, value);
    }

    private static readonly IBrush LiveCellBrush = Brushes.Black;

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == BoardStateProperty)
        {
            InvalidateVisual();
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var state = BoardState;
        if (state == null)
        {
            return;
        }

        var rows = state.GetLength(0);
        var cols = state.GetLength(1);

        if (rows == 0 || cols == 0)
        {
            return;
        }

        var cellWidth = Bounds.Width / cols;
        var cellHeight = Bounds.Height / rows;

        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < cols; x++)
            {
                if (state[y, x])
                {
                    var rect = new Rect(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    context.FillRectangle(LiveCellBrush, rect);
                }
            }
        }
    }
}