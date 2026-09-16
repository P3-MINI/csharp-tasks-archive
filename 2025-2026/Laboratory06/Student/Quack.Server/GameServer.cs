using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Quack.Messages;
using Quack.Engine.Extra;

namespace Quack.Server;

public class GameServer : IDisposable
{
    private const int Port = 6700;
    private TcpListener Listener { get; } = new(IPAddress.Any, Port);
    private CancellationTokenSource Cancellation { get; } = new();
    private int NextPlayerId { get; set; } = 1;
    private int NextFoodId { get; set; } = 1;
    private const int MaxFoodCount = 50;
    
    private Dictionary<int, Task> ClientTasks { get; } = [];
    private Dictionary<int, ClientConnection> ClientConnections { get; } = [];
    private List<DuckState> DuckStates { get; } = [];
    private List<FoodState> FoodStates { get; } = [];
    private List<FoodEvent> PendingFoodEvents { get; } = [];

    public Scene Scene { get; } = new();
    private PhysicsSystem Physics { get; } = new();

    public GameServer()
    {
        Physics.FoodConsumed += OnFoodConsumed;
        Physics.DuckEaten += OnDuckEaten;
        // Spawn initial food
        for (int i = 0; i < MaxFoodCount; i++)
        {
            SpawnFood(notify: false);
        }
    }

    private void OnDuckEaten(object? sender, PhysicsSystem.DuckEatenEventArgs e)
    {
        if (ClientConnections.TryGetValue(e.PreyId, out var connection))
        {
            Console.WriteLine($"Duck {e.PreyId} was eaten by {e.PredatorId}!");
            if (Scene.Ducks.TryGetValue(e.PredatorId, out var predator))
            {
                if (Scene.Ducks.TryGetValue(e.PreyId, out var pray))
                {
                    predator.Food += pray.Scale * pray.Scale;
                }
            }
            connection.Disconnect();
        }
    }

    private void SpawnFood(bool notify)
    {
        int id = NextFoodId++;
        float x = Random.Shared.NextSingle(-90.0f, 90.0f);
        float y = Random.Shared.NextSingle(-90.0f, 90.0f);

        var body = Physics.CreateFoodBody(id, x, y);
        var state = new FoodState { Id = id, X = x, Y = y };
        var food = new Food(state, body);

        Scene.Food[id] = food;
        FoodStates.Add(state);

        if (notify)
        {
            PendingFoodEvents.Add(new FoodEvent { Type = FoodEventType.Spawn, FoodId = id, X = x, Y = y });
        }
    }

    private void OnFoodConsumed(object? sender, PhysicsSystem.FoodConsumedEventArgs e)
    {
        if (Scene.Food.Remove(e.FoodId, out var food))
        {
            Physics.DestroyBody(food.PhysicsBody);
            
            int index = FoodStates.FindIndex(f => f.Id == e.FoodId);
            if (index != -1) FoodStates.RemoveAt(index);

            PendingFoodEvents.Add(new FoodEvent { Type = FoodEventType.Consumed, FoodId = e.FoodId });

            if (Scene.Ducks.TryGetValue(e.DuckId, out var duck))
            {
                duck.Food += 1.0f;
            }

            SpawnFood(notify: true);
        }
    }

    public async Task StartAsync()
    {
        Listener.Start(backlog: 10);
        try
        {
            CancellationToken token = Cancellation.Token;
            while (!token.IsCancellationRequested)
            {
                TcpClient client = await Listener.AcceptTcpClientAsync(token);
                int id = NextPlayerId++;
                ClientTasks[id] = HandleClientAsync(client, id, token);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public void Update(float dt)
    {
        Physics.Step(dt);
        Scene.Update(dt);
    }
    
    private async Task HandleClientAsync(TcpClient client, int id, CancellationToken token = default)
    {
        ClientConnection connection = new ClientConnection(client, id);
        ClientConnections[id] = connection;
        connection.Disconnected += ConnectionOnDisconnected;
        connection.MessageReceived += ConnectionOnMessageReceived;

        try
        {
            await connection.StartReadingAsync(token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling client {id}: {ex.Message}");
        }
        finally
        {
            connection.Disconnect();
        }
    }
    
    public async Task BroadcastUpdateStateAsync()
    {
        var eventsToSend = new List<FoodEvent>(PendingFoodEvents);
        PendingFoodEvents.Clear();

        var worldState = new UpdateStateMessage
        {
            Ducks = DuckStates,
            FoodEvents = eventsToSend,
            GameTime = Scene.GameTime
        };

        try
        {
            await BroadcastMessage(worldState);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task BroadcastMessage(IJsonMessage message, int excludePlayerId = -1)
    {
        List<Task> sendTasks = new List<Task>(ClientConnections.Count);
        string json = message.Serialize();
        foreach (var connection in ClientConnections.Values)
        {
            if (connection.Id == excludePlayerId) continue;
            if (connection.IsConnected)
            {
                sendTasks.Add(connection.SendSerializedAsync(message.Type, json, Cancellation.Token));
            }
        }

        await Task.WhenAll(sendTasks);
    }

    private async void ConnectionOnMessageReceived(object? sender, IMessage message)
    {
        if (sender is ClientConnection connection)
        {
            try
            {
                switch (message.Type)
                {
                    case MessageType.Join:
                        if (message is JoinMessage join)
                        {
                            float startX = Random.Shared.NextSingle(-90.0f, 90.0f);
                            float startY = Random.Shared.NextSingle(-90.0f, 90.0f);
                            float startRot = Random.Shared.NextSingle(0.0f, 2 * float.Pi);

                            var body = Physics.CreateDuckBody(
                                connection.Id,
                                startX,
                                startY,
                                startRot,
                                1.0f
                            );

                            DuckState state = new DuckState
                            {
                                Id = connection.Id,
                                Name = join.Name,
                                Scale = 1.0f,
                                X = startX,
                                Y = startY,
                                Rotation = startRot
                            };
                        
                            Duck duck = new Duck(state, body);
                            Scene.Ducks[connection.Id] = duck;
                            DuckStates.Add(state);
                        
                            WelcomeMessage welcome = new WelcomeMessage
                            {
                                Ducks = DuckStates,
                                Food = FoodStates,
                                PlayerId = connection.Id,
                                GameTime = Scene.GameTime
                            };
                            await connection.SendSerializedAsync(welcome.Type, welcome.Serialize(), Cancellation.Token);
                        }
                        break;
                    case MessageType.Welcome:
                        break;
                    case MessageType.ClientInput:
                        if (message is InputMessage input)
                        {
                            if (Scene.Ducks.TryGetValue(connection.Id, out var duck))
                            {
                                duck.ReadInput(input);
                            }
                        }
                        break;
                    case MessageType.UpdateState:
                        break;
                    case MessageType.Disconnected:
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message from {connection.Id}: {ex}");
                connection.Disconnect();
            }
        }
    }

    private async void ConnectionOnDisconnected(object? sender, EventArgs e)
    {
        if (sender is ClientConnection connection)
        {
            try
            {
                ClientConnections.Remove(connection.Id);
            
                if (Scene.Ducks.Remove(connection.Id, out var duck))
                {
                    Physics.DestroyBody(duck.PhysicsBody);
                }

                for (int i = 0; i < DuckStates.Count; i++)
                {
                    if (DuckStates[i].Id == connection.Id)
                    {
                        DuckStates.RemoveAt(i);
                        break;
                    }
                }
                if (ClientTasks.Remove(connection.Id, out var task))
                    await task;
                await BroadcastMessage(new DisconnectedMessage { PlayerId = connection.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling disconnection for {connection.Id}: {ex}");
            }
        }
    }

    public IEnumerable<string> GetServerIPs()
    {
        var ips = new List<string>();
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip.Address) && !ip.Address.ToString().StartsWith("169.254"))
                    {
                        ips.Add($"{ip.Address}:{Port}");
                    }
                    else if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6 && !IPAddress.IsLoopback(ip.Address) && !ip.Address.IsIPv6LinkLocal)
                    {
                        ips.Add($"[{ip.Address}]:{Port}");
                    }
                }
            }
        }
        if (ips.Count == 0)
        {
            ips.Add($"127.0.0.1:{Port}");
        }
        return ips;
    }

    public void Dispose()
    {
        Cancellation.Cancel();
        Listener.Stop();
        
        foreach (var client in ClientConnections.Values)
        {
            client.Disconnect();
        }
        ClientConnections.Clear();
        
        Physics.Dispose();
        Scene.Dispose();
    }
}