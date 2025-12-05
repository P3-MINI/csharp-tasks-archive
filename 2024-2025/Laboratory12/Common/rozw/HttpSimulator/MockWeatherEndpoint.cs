using System.Net;
using System.Text.Json;

namespace HttpSimulator;

public class MockWeatherEndpoint : IMockEndpoint
{
    private readonly HashSet<string> acceptedHosts = new HashSet<string> { "localhost", "127.0.0.1" };
    private readonly int port = 2137;

    private static Dictionary<string, double[]> tempData = new Dictionary<string, double[]>
    {
        { "London", new double[] { 18.9, 21.3, 19.4, 22.8, 20.1, 25.7, 23.5 } },
        { "Tokyo", new double[] { 5.6, 8.2, 10.5, 12.3, 9.9, 14.4, 11.2 } },
        { "Sydney", new double[] { 25.5, 28.1, 27.6, 27.0, 25.5, 30.2, 31.3, 29.8, 26.4 } },
        { "Berlin", new double[] { 1.4, -2.3, 3.6, -1.2, -4.5, 2.3 } },
        { "New York", new double[] { 12.3, 7.8, 14.5, -2.1, 6.9, 10.4, 8.7 } },
    };

    public async Task<HttpResponseMessage> AsyncRequest(HttpRequestMessage request)
    {
        if (request.RequestUri == null)
            return new HttpResponseMessage(HttpStatusCode.BadRequest);

        if (!acceptedHosts.Contains(request.RequestUri.Host))
            return new HttpResponseMessage(HttpStatusCode.BadGateway);
        if (request.RequestUri.Port != port)
            return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
        if (request.RequestUri.Scheme != "https")
            return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);

        switch (request.RequestUri.LocalPath)
        {
            case "/api/v13/forecast":
                return await ForecastEndpoint(request);
            default:
                return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }

    private async Task<HttpResponseMessage> ForecastEndpoint(HttpRequestMessage request)
    {
        if (request.RequestUri == null)
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);

        var queryParameters = HttpRequestHelpers.GetQueryParameters(request.RequestUri);

        if (!queryParameters.ContainsKey("city"))
            return new HttpResponseMessage(HttpStatusCode.BadRequest);

        var city = queryParameters["city"];

        var temp = tempData[city];

        // Simulates network delay and response calculation time
        // Thread.Sleep(Random.Shared.Next(500, 1500)); // TODO: Fix this bug!
        await Task.Delay(Random.Shared.Next(500, 1500));
        

        return new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(
                $"{{ \"Daily\": {{ \"Temperature\": {JsonSerializer.Serialize(temp)} }} }}"
                )
        };
    }
}
