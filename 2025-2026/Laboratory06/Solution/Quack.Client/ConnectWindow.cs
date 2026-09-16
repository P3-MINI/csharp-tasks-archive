using ImGuiNET;
using System.Numerics;

namespace Quack.Client;

public class ConnectWindow
{
    private readonly GameClient _client;
    
    private string _host = "localhost";
    private int _port = 6700;
    private string _name = Environment.UserName;
    private string _status = "";
    private bool _isConnecting = false;

    public ConnectWindow(GameClient client)
    {
        _client = client;
    }

    public void Render()
    {
        if (_client.IsConnected) return;

        // Center the window
        var viewport = ImGui.GetMainViewport();
        ImGui.SetNextWindowPos(viewport.GetCenter(), ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));
        
        if (ImGui.Begin("Join Game", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize))
        {
            ImGui.InputText("Host", ref _host, 64);
            ImGui.InputInt("Port", ref _port);
            ImGui.InputText("Name", ref _name, 32);
            
            ImGui.Dummy(new Vector2(0, 10)); // Spacer

            if (_isConnecting)
            {
                ImGui.TextDisabled("Connecting...");
            }
            else
            {
                if (ImGui.Button("Connect", new Vector2(-1, 0)) || ImGui.IsKeyPressed(ImGuiKey.Enter))
                {
                    Connect();
                }
            }

            if (!string.IsNullOrEmpty(_status))
            {
                ImGui.Dummy(new Vector2(0, 5));
                ImGui.TextColored(new Vector4(1, 0.4f, 0.4f, 1), _status);
            }
            
            ImGui.End();
        }
    }

    private async void Connect()
    {
        _isConnecting = true;
        _status = "";
        try
        {
            await _client.ConnectAsync(_host, _port);
            await _client.JoinAsync(_name);
        }
        catch (Exception ex)
        {
            _status = ex.Message;
            // Ensure we disconnect cleanly if Join failed partway
            // _client.Disconnect()? Client logic handles cleanup usually.
        }
        finally
        {
            _isConnecting = false;
        }
    }
}