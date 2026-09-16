namespace Quack.Messages;

public class DuckState
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float X { get; set; }
    public float Y { get; set; }
    public float Rotation { get; set; }
    public float Scale { get; set; }
    public long Timestamp { get; set; }
}

public class FoodState
{
    public int Id { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

public class FoodEvent
{
    public FoodEventType Type { get; set; }
    public int FoodId { get; set; }
    public float X { get; set; } 
    public float Y { get; set; }
}

public enum FoodEventType : byte
{
    Spawn,
    Consumed
}