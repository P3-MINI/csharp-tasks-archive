namespace MiniTest;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class DataRowAttribute(params object?[]? data) : Attribute
{
    public object?[] Data { get; } = data ?? [null];
    public string? Description { get; set; }
}