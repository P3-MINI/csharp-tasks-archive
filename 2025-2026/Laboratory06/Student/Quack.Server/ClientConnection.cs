using System.Buffers;
using System.Net.Sockets;
using System.Text;
using Quack.Messages;

namespace Quack.Server;

public class ClientConnection
{
    private const int MaxPayloadSize = 64 * 1024;
    private TcpClient Client { get; }
    private NetworkStream Stream { get; }
    private bool _disconnecting;
    private SemaphoreSlim SendLock { get; } = new(1, 1);
  
    public bool IsConnected => Client.Connected;
    public int Id { get; init; }
    
    public event EventHandler<IMessage>? MessageReceived;
    public event EventHandler? Disconnected;

    public ClientConnection(TcpClient client, int id)
    {
        Client = client;
        Stream = Client.GetStream();
        Client.NoDelay = true;
        Id = id;
    }

    public async Task StartReadingAsync(CancellationToken token = default)
    {
        try
        {
            byte[] headerBuffer = new byte[5];
            byte[] payloadBuffer = new byte[MaxPayloadSize];
            while (!token.IsCancellationRequested && IsConnected)
            {
                await Stream.ReadExactlyAsync(headerBuffer, 0, 5, token);

                int payloadLength = BitConverter.ToInt32(headerBuffer, 0);
                if (payloadLength > MaxPayloadSize) throw new IOException("Message payload too large");
                
                MessageType type = (MessageType)headerBuffer[4];

                await Stream.ReadExactlyAsync(payloadBuffer, 0, payloadLength, token);

                if (type != MessageType.Join && type != MessageType.ClientInput)
                {
                    Console.WriteLine($"Server received unexpected message type: {type}");
                    break;
                }

                IMessage message = IBinaryMessage.Deserialize(type, payloadBuffer);
                MessageReceived?.Invoke(this, message);
            }
        }
        catch (IOException ioEx) when (ioEx.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionReset })
        {
            Console.WriteLine($"Client connection reset: {ioEx.Message}");
        }
        catch (Exception ex) when (ex is EndOfStreamException or OperationCanceledException or ObjectDisposedException)
        {
            // Expected disconnection or cancellation
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Network read error: {ex.GetType().Name}: {ex.Message}");
        }
        finally
        {
            Disconnect();
        }
    }
    
    public async Task SendSerializedAsync(MessageType type, string json, CancellationToken token = default)
    {
        if (!IsConnected || _disconnecting) return;
        byte[]? buffer = null;
        try
        {
            int payloadSize = Encoding.UTF8.GetByteCount(json);
            buffer = ArrayPool<byte>.Shared.Rent(payloadSize + 5);
            BitConverter.TryWriteBytes(buffer, payloadSize);
            buffer[4] = (byte)type;
            int bytes = Encoding.UTF8.GetBytes(json, buffer.AsSpan(5));
            int messageLength = 5 + bytes;

            await SendLock.WaitAsync(token);
            try
            {
                await Stream.WriteAsync(buffer, 0, messageLength, token);
            }
            finally
            {
                SendLock.Release();
            }
        }
        catch (IOException ioEx) when (ioEx.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionReset })
        {
            Console.WriteLine($"Network send failed (connection reset): {ioEx}");
            Disconnect();
        }
        catch (ObjectDisposedException)
        {
            Console.WriteLine("Network send failed (object disposed).");
            Disconnect();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Network send error: {ex}");
            Disconnect();
        }
        finally
        {
            if (buffer != null) ArrayPool<byte>.Shared.Return(buffer);
        }
    }
    
    public void Disconnect()
    {
        if (Interlocked.Exchange(ref _disconnecting, true)) return;

        try
        {
            if (Client.Connected)
            {
                Client.Client.Shutdown(SocketShutdown.Both);
                Client.Close();
            }
            Disconnected?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during disconnect: {ex.Message}");
        }
        finally
        {
            Client.Dispose();
        }
    }
}