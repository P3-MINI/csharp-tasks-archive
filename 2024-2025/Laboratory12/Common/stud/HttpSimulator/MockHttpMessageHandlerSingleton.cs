namespace HttpSimulator;

public static class MockHttpMessageHandlerSingleton
{
    private static readonly Lazy<MockHttpMessageHandler> _instance = new Lazy<MockHttpMessageHandler>(() =>
    {
        var mockHandler = new MockHttpMessageHandler(new MockWeatherEndpoint());

        return mockHandler;
    });

    public static MockHttpMessageHandler Instance => _instance.Value;
}
