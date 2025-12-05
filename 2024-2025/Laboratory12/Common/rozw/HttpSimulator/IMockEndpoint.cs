namespace HttpSimulator;

public interface IMockEndpoint
{
    Task<HttpResponseMessage> AsyncRequest(HttpRequestMessage request);
}
