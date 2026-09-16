using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Quack.Client;

public struct Input : IEquatable<Input>
{
    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Right { get; set; }
    public bool Left { get; set; }
    public bool Sprint { get; set; }

    public static Input Read(KeyboardState keyboardState)
    {
        return new Input
        {
            Up = keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up),
            Down = keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down),
            Right = keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right),
            Left = keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left),
            Sprint = keyboardState.IsKeyDown(Keys.LeftShift)
        };
    }

    public bool Equals(Input other)
    {
        return Up == other.Up && Down == other.Down && Right == other.Right && Left == other.Left && Sprint == other.Sprint;
    }

    public override bool Equals(object? obj)
    {
        return obj is Input other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Up, Down, Right, Left, Sprint);
    }

    public static bool operator ==(Input left, Input right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Input left, Input right)
    {
        return !left.Equals(right);
    }
}