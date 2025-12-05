namespace MiniTest;

[AttributeUsage(AttributeTargets.Method)]
public class PriorityAttribute(int priority) : Attribute
{
    public int Priority { get; } = priority;
}