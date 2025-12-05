namespace HttpSimulator;

public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly IMockEndpoint _mockEndpoint;

    public MockHttpMessageHandler(IMockEndpoint mockEndpoint)
    {
        _mockEndpoint = mockEndpoint ?? throw new ArgumentNullException(nameof(mockEndpoint));
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _mockEndpoint.AsyncRequest(request);
    }
}
