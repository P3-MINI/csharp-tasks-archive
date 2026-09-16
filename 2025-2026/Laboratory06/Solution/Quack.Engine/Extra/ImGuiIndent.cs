using ImGuiNET;

namespace Quack.Engine.Extra;

public readonly struct ImGuiIndent : IDisposable
{
    private float Value { get; }

    public ImGuiIndent(float value)
    {
        Value = value;
        ImGui.Indent(Value);
    }

    public void Dispose()
    {
        ImGui.Unindent(Value);
    }
}