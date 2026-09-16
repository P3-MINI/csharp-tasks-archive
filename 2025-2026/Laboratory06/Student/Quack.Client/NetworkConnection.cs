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
            while (!token.IsCancellationRequested && IsConnected)
            {
                // TODO Stage 3: NetworkConnection.StartReadingAsync
                await Task.Delay(100, token);
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
        try
        {
            // TODO Stage 4: NetworkConnection.SendAsync
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