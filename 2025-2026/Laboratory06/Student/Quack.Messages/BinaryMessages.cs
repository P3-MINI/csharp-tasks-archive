using System.Text;

namespace Quack.Messages;

public interface IBinaryMessage : IMessage
{
    int GetSize();
    void Serialize(Span<byte> buffer);
    static abstract IBinaryMessage Deserialize(ReadOnlySpan<byte> buffer);
    
    public static IBinaryMessage Deserialize(MessageType type, ReadOnlySpan<byte> buffer) => type switch
    {
        MessageType.Join => JoinMessage.Deserialize(buffer),
        MessageType.ClientInput => InputMessage.Deserialize(buffer),
        _ => throw new ArgumentOutOfRangeException(nameof(type), "Message type out of range")
    };
}

public class JoinMessage : IBinaryMessage
{
    public MessageType Type => MessageType.Join;
    public string Name { get; set; } = string.Empty;

    public int GetSize() => 4 + Encoding.UTF8.GetByteCount(Name);

    public void Serialize(Span<byte> buffer)
    {
        // TODO Stage 1: JoinMessage.Serialize
    }

    public static IBinaryMessage Deserialize(ReadOnlySpan<byte> buffer)
    {
        // TODO Stage 1: JoinMessage.Deserialize
        return new JoinMessage();
    }
}

public class InputMessage : IBinaryMessage
{
    public MessageType Type => MessageType.ClientInput;
    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Sprint { get; set; }

    public int GetSize() => 1;

    public void Serialize(Span<byte> buffer)
    {
        // TODO Stage 1: InputMessage.Serialize
    }

    public static IBinaryMessage Deserialize(ReadOnlySpan<byte> buffer)
    {
        // TODO Stage 1: InputMessage.Deserialize
        return new InputMessage();
    }
}