using System.Buffers;
using System.Net.Sockets;
using System.Text;
using Quack.Messages;

namespace Quack.Client;

public class NetworkConnection
{
    private const int MaxPayloadSize = 64 * 1024;
    private TcpClient Client { get; }
    private NetworkStream Stream { get; }
    private bool _disconnecting;
  
    public bool IsConnected => Client.Connected;
    
    public event EventHandler<IMessage>? MessageReceived;
    public event EventHandler? Disconnected;

    public NetworkConnection(TcpClient client)
    {
        Client = client;
        Stream = Client.GetStream();
        Client.NoDelay = true;
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
                if (payloadLength > MaxPayloadSize) throw new IOException("Message length too large");

                MessageType type = (MessageType)headerBuffer[4];

                await Stream.ReadExactlyAsync(payloadBuffer, 0, payloadLength, token);

                string json = Encoding.UTF8.GetString(payloadBuffer, 0, payloadLength);
                IMessage? message = IJsonMessage.Deserialize(type, json);
                
                if (message != null)
                {
                    MessageReceived?.Invoke(this, message);
                }
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
    
    public async Task SendAsync(IBinaryMessage message, CancellationToken token = default)
    {
        if (!IsConnected || _disconnecting) return;
        byte[]? buffer = null;
        try
        {
            int payloadSize = message.GetSize();
            buffer = ArrayPool<byte>.Shared.Rent(payloadSize + 5);
            
            BitConverter.TryWriteBytes(buffer, payloadSize);
            buffer[4] = (byte)message.Type;
            message.Serialize(buffer.AsSpan(5));
            
            await Stream.WriteAsync(buffer, 0, payloadSize + 5, token);
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