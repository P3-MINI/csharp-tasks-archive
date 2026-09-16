using System.Net;
using System.Net.Sockets;
using Quack.Client;
using Quack.Messages;

namespace Quack.Tests;

[TestClass]
public class Stage4_NetworkSendTests
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
    public async Task SendAsync_WritesCorrectHeaderAndPayload()
    {
        // Arrange
        var msg = new JoinMessage { Name = "B" }; 
        
        // Act
        await _connection.SendAsync(msg);

        // Assert - Read from server side
        var stream = _serverAcceptedSocket.GetStream();
        byte[] header = new byte[5];
        
        using var readCts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        try
        {
            await stream.ReadExactlyAsync(header, 0, 5, readCts.Token);
        }
        catch (OperationCanceledException)
        {
            Assert.Fail("Timed out waiting for the header. Did SendAsync write the 5-byte header?");
        }

        int payloadLen = BitConverter.ToInt32(header, 0);
        Assert.AreEqual(msg.GetSize(), payloadLen, "The first 4 bytes of the header (Payload Length) do not match the expected message size.");
        Assert.AreEqual((byte)MessageType.Join, header[4], "The 5th byte of the header (Message Type) is incorrect.");

        byte[] payload = new byte[payloadLen];
        try
        {
            await stream.ReadExactlyAsync(payload, 0, payloadLen, readCts.Token);
        }
        catch (OperationCanceledException)
        {
            Assert.Fail($"Timed out waiting for the payload ({payloadLen} bytes). Did SendAsync write the full payload?");
        }
        
        var receivedMsg = (JoinMessage)JoinMessage.Deserialize(payload);
        Assert.AreEqual("B", receivedMsg.Name, "The deserialized payload content (JoinMessage.Name) does not match what was sent.");
    }
}
