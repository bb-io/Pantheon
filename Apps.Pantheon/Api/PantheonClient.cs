using Apps.Pantheon.Constants;
using Apps.Pantheon.Models.Response.Error;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;
using System.Text.RegularExpressions;

namespace Apps.Pantheon.Api;

public class PantheonClient : BlackBirdRestClient
{
    public PantheonClient(IEnumerable<AuthenticationCredentialsProvider> creds) : base(new()
    {
        BaseUrl = new Uri($"{creds.Get(CredsNames.BaseUrl).Value}/v1/client-api/"),
    })
    {
        this.AddDefaultHeader("x-pantheon-auth-key", creds.Get(CredsNames.AuthKey).Value);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (response.Content is null)
        {
            var message = response.ErrorMessage ?? "No content returned by server";
            throw new PluginApplicationException($"Status {response.StatusCode} - {message}");
        }

        if (response.ContentType == "text/html")
        {
            var match = Regex.Match(response.Content, @"<title>(.*?)</title>", RegexOptions.IgnoreCase);
            string titleErrorMessage = match.Success ? match.Groups[1].Value : "No content returned by server";
            throw new PluginApplicationException(titleErrorMessage);
        }

        var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        var errorMessage = $"Status {error?.Status} - {error?.Message}";

        throw new PluginApplicationException(errorMessage);
    }
}