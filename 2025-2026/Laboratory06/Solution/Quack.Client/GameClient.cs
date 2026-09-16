using System.Net;
using System.Net.Sockets;
using Quack.Messages;

namespace Quack.Client;

public class GameClient : IDisposable
{
    private NetworkConnection? Connection { get; set; }
    private CancellationTokenSource Cancellation { get; set; } = new();

    public int MyId { get; private set; }

    public Scene? Scene { get; set; }

    public event EventHandler? Connected;
    public event EventHandler? Disconnected;

    public bool IsConnected => Connection?.IsConnected ?? false;
    
    public async Task ConnectAsync(string host, int port)
    {
        try
        {
            var client = new TcpClient();

            if (!IPAddress.TryParse(host, out var address))
            {
                var hostEntry = await Dns.GetHostEntryAsync(host);
                address = hostEntry.AddressList.FirstOrDefault(addr => addr.AddressFamily == AddressFamily.InterNetwork);
            }

            if (address == null)
            {
                throw new Exception($"Could not resolve host '{host}' to an IPv4 address.");
            }

            Console.WriteLine($"Attempting to connect to {address}:{port}...");
            await client.ConnectAsync(address, port, Cancellation.Token);
            Console.WriteLine($"Successfully connected to {address}:{port}.");
            
            Connection = new NetworkConnection(client);
            Connection.MessageReceived += ConnectionOnMessageReceived;
            Connection.Disconnected += ConnectionOnDisconnected;

            _ = Connection.StartReadingAsync(Cancellation.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
            throw;
        }
    }
    
    public async Task JoinAsync(string name)
    {
        if (!IsConnected) return;
        if (Connection is not null)
        {
            await Connection.SendAsync(new JoinMessage { Name = name });
        }
    }
    
    public async Task SendInput(Input input)
    {
        if (!IsConnected) return;
        if (Connection is not null)
        {
            await Connection.SendAsync(new InputMessage
            {
                Up = input.Up,
                Down = input.Down,
                Left = input.Left,
                Right = input.Right,
                Sprint = input.Sprint
            });
        }
    }

    private void ConnectionOnDisconnected(object? sender, EventArgs e)
    {
        Disconnected?.Invoke(this, EventArgs.Empty);
    }

    private void ConnectionOnMessageReceived(object? sender, IMessage message)
    {
        switch (message.Type)
        {
            case MessageType.Welcome:
                if (message is WelcomeMessage welcome)
                {
                    MyId = welcome.PlayerId;
                    Scene?.InitState(welcome);
                    OnConnected();
                }
                break;

            case MessageType.UpdateState:
                if (message is UpdateStateMessage updateState)
                {
                    Scene?.UpdateState(updateState);
                }
                break;

            case MessageType.Disconnected:
                if (message is DisconnectedMessage disconnected)
                {
                    Scene?.RemoveDuck(disconnected.PlayerId);
                }
                break;
        }
    }

    protected virtual void OnConnected()
    {
        Connected?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        Cancellation.Dispose();
        Connection?.Disconnect();
        Scene?.Dispose();
    }
}