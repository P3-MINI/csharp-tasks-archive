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
        int count = Encoding.UTF8.GetBytes(Name, buffer[4..]);
        BitConverter.TryWriteBytes(buffer[..4], count);
    }

    public static IBinaryMessage Deserialize(ReadOnlySpan<byte> buffer)
    {
        int len = BitConverter.ToInt32(buffer[..4]);
        return new JoinMessage { Name = Encoding.UTF8.GetString(buffer.Slice(4, len)) };
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
        byte flags = 0;
        if (Up)     flags |= 0x01;
        if (Down)   flags |= 0x02;
        if (Left)   flags |= 0x04;
        if (Right)  flags |= 0x08;
        if (Sprint) flags |= 0x10;
        buffer[0] = flags;
    }

    public static IBinaryMessage Deserialize(ReadOnlySpan<byte> buffer)
    {
        byte flags = buffer[0];
        return new InputMessage
        {
            Up =     (flags & 0x01) != 0,
            Down =   (flags & 0x02) != 0,
            Left =   (flags & 0x04) != 0,
            Right =  (flags & 0x08) != 0,
            Sprint = (flags & 0x10) != 0
        };
    }
}