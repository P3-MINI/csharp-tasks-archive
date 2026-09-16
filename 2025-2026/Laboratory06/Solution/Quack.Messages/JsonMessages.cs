using System.Text.Json;

namespace Quack.Messages;

public interface IJsonMessage : IMessage
{
    string Serialize();
    static abstract IJsonMessage? Deserialize(string json);
    
    public static IJsonMessage? Deserialize(MessageType type, string json) => type switch
    {
        MessageType.Welcome => JsonMessage<WelcomeMessage>.Deserialize(json),
        MessageType.UpdateState => JsonMessage<UpdateStateMessage>.Deserialize(json),
        MessageType.Disconnected => JsonMessage<DisconnectedMessage>.Deserialize(json),
        _ => throw new ArgumentOutOfRangeException(nameof(type), "Message type out of range")
    };
}

public abstract class JsonMessage<TSelf> : IJsonMessage where TSelf : JsonMessage<TSelf>
{
    public abstract MessageType Type { get; }
    
    public string Serialize()
    {
        return JsonSerializer.Serialize(this, typeof(TSelf));
    }

    public static IJsonMessage? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<TSelf>(json);
    }
}

public class WelcomeMessage : JsonMessage<WelcomeMessage>
{
    public override MessageType Type => MessageType.Welcome;
    public int PlayerId { get; set; }
    public List<DuckState> Ducks { get; set; } = [];
    public List<FoodState> Food { get; set; } = [];
    public DateTime GameTime { get; set; }
}

public class UpdateStateMessage : JsonMessage<UpdateStateMessage>
{
    public override MessageType Type => MessageType.UpdateState;
    public List<DuckState> Ducks { get; set; } = [];
    public List<FoodEvent> FoodEvents { get; set; } = [];
    public DateTime GameTime { get; set; }
}

public class DisconnectedMessage : JsonMessage<DisconnectedMessage>
{
    public override MessageType Type => MessageType.Disconnected;
    public int PlayerId { get; set; }
}
