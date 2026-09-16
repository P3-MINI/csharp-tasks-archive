using System.Net;
using System.Net.Sockets;
using Quack.Client;
using Quack.Messages;

namespace Quack.Tests;

[TestClass]
public class Stage3_NetworkReadTests
{
    private TcpListener _server = null!;
    private NetworkConnection _connection = null!;
    private TcpClient _clientSocket = null!;
    private TcpClient _serverAcceptedSocket = null!;
    private Task? _readTask;
    private CancellationTokenSource _cts = null!;

    [TestInitialize]
    public async Task Setup()
    {
        _cts = new CancellationTokenSource();
        _server = new TcpListener(IPAddress.Loopback, 0);
        _server.Start();
        int port = ((IPEndPoint)_server.LocalEndpoint).Port;

        _clientSocket = new TcpClient();
        await _clientSocket.ConnectAsync(IPAddress.Loopback, port);
        _serverAcceptedSocket = await _server.AcceptTcpClientAsync();
        
        _connection = new NetworkConnection(_clientSocket);
        _readTask = _connection.StartReadingAsync(_cts.Token);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _cts.Cancel();
        _connection.Disconnect();
        _serverAcceptedSocket.Close();
        _server.Stop();
    }

    [TestMethod]
    [Timeout(5000, CooperativeCancellation = true)]
    public async Task StartReadingAsync_ParsesIncomingData_AndRaisesEvent()
    {
        // Arrange
        var tcs = new TaskCompletionSource<IMessage>();
        _connection.MessageReceived += (s, m) => tcs.TrySetResult(m);

        var welcome = new WelcomeMessage { PlayerId = 777 };
        string json = welcome.Serialize();
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(json);
        
        // Construct frame: [Length (4)] [Type (1)] [Payload (N)]
        byte[] frame = new byte[5 + payload.Length];
        BitConverter.TryWriteBytes(frame.AsSpan(0, 4), payload.Length);
        frame[4] = (byte)MessageType.Welcome;
        payload.CopyTo(frame, 5);

        // Act
        await _serverAcceptedSocket.GetStream().WriteAsync(frame);

        // Assert
        try
        {
            var receivedMsg = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(2));
            Assert.IsInstanceOfType<WelcomeMessage>(receivedMsg, "The received message should be deserialized as a WelcomeMessage.");
            Assert.AreEqual(777, ((WelcomeMessage)receivedMsg).PlayerId, "The 'PlayerId' in the received message does not match the value sent by the server.");
        }
        catch (TimeoutException)
        {
            Assert.Fail("Timed out waiting for MessageReceived event. Is StartReadingAsync reading from the stream and firing the event?");
        }
    }
}
