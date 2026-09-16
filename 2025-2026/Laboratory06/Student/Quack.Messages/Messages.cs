namespace Quack.Messages;

public enum MessageType : byte
{
    Empty,
    Join,
    Welcome,
    ClientInput,
    UpdateState,
    Disconnected
}

public interface IMessage
{
    MessageType Type { get; }
}