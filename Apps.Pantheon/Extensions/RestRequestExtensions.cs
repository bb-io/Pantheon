using RestSharp;

namespace Apps.Pantheon.Extensions;

public static class RestRequestExtensions
{
    public static void AddQueryParameterIfNotEmpty(this RestRequest request, string paramName, string? queryParam)
    {
        if (!string.IsNullOrEmpty(queryParam))
            request.AddQueryParameter(paramName, queryParam);
    }
}
