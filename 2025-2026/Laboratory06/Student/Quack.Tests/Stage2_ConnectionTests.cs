using System.Net;
using System.Net.Sockets;
using Quack.Client;

namespace Quack.Tests;

[TestClass]
public class Stage2_ConnectionTests
{
    private TcpListener _server = null!;
    private GameClient _client = null!;

    [TestInitialize]
    public void Setup()
    {
        // Port 0 ensures each parallel test gets its own unique port.
        _server = new TcpListener(IPAddress.Loopback, 0);
        _server.Start();
        _client = new GameClient();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _client.Dispose();
        _server.Stop();
    }

    [TestMethod]
    [Timeout(5000, CooperativeCancellation = true)]
    [DataRow("127.0.0.1")]
    [DataRow("localhost")]
    public async Task ConnectAsync_EstablishesTcpConnection(string host)
    {
        // Arrange
        int port = ((IPEndPoint)_server.LocalEndpoint).Port;

        // Act
        // Connect and Accept must happen concurrently
        try 
        {
            Task connectTask = _client.ConnectAsync(host, port);
            using TcpClient serverSideSocket = await _server.AcceptTcpClientAsync().WaitAsync(TimeSpan.FromSeconds(2));
            await connectTask.WaitAsync(TimeSpan.FromSeconds(2));

            // Assert
            Assert.IsTrue(_client.IsConnected, $"Failed to connect using host '{host}'. Client.IsConnected should be true.");
            Assert.IsTrue(serverSideSocket.Connected, "Server side socket should be active, meaning the handshake completed successfully.");
        }
        catch (TimeoutException)
        {
            Assert.Fail($"Timed out waiting for connection to '{host}'. Is ConnectAsync attempting to connect?");
        }
    }
}
