namespace MiniTest;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class DescriptionAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}