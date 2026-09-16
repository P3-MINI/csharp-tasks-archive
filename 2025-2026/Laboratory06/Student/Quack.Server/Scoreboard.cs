using ImGuiNET;
using Vector2 = System.Numerics.Vector2;

namespace Quack.Server;

public class Scoreboard
{
    private List<(float Scale, string Name)> _list = [];
    
    public void Update(Scene scene)
    {
        _list.Clear();
        foreach (var duck in scene.Ducks.Values)
        {
            _list.Add((duck.Scale, duck.State.Name));
        }
        _list.Sort((a, b) => b.Scale.CompareTo(a.Scale));
    }

    public void Render()
    {
        ImGui.SetNextWindowSize(new Vector2(960, 600), ImGuiCond.Once);
        ImGui.SetNextWindowPos(new Vector2(160, 100), ImGuiCond.Once);
        ImGui.SetNextWindowCollapsed(false, ImGuiCond.Once);
        ImGui.Begin("Scoreboard");
        
        if (ImGui.BeginTable("ScoreboardTable", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
        {
            ImGui.TableSetupColumn("Rank", ImGuiTableColumnFlags.WidthFixed, 90.0f);
            ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableSetupColumn("Score", ImGuiTableColumnFlags.WidthFixed, 120.0f);
            ImGui.TableHeadersRow();

            for (int i = 0; i < _list.Count; i++)
            {
                var item = _list[i];
                ImGui.TableNextRow();
                
                ImGui.TableNextColumn();
                ImGui.Text($"#{i + 1}");
                
                ImGui.TableNextColumn();
                ImGui.Text(item.Name);
                
                ImGui.TableNextColumn();
                ImGui.Text($"{(int)(item.Scale * 100)}");
            }
            ImGui.EndTable();
        }
        
        ImGui.End();
    }
}