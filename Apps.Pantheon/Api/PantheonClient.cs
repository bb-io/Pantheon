using Apps.Pantheon.Constants;
using Apps.Pantheon.Models.Response.Error;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text.RegularExpressions;

namespace Apps.Pantheon.Api;

public class PantheonClient : BlackBirdRestClient
{
    private readonly Logger? _logger;

    public PantheonClient(IEnumerable<AuthenticationCredentialsProvider> creds, Logger? logger = null) : base(new()
    {
        BaseUrl = new Uri($"{creds.Get(CredsNames.BaseUrl).Value}/v1/client-api/"),
    })
    {
        _logger = logger;
        this.AddDefaultHeader("x-pantheon-auth-key", creds.Get(CredsNames.AuthKey).Value);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (response.Content is null)
        {
            var message = response.ErrorMessage ?? "No content returned by server";
            throw new PluginApplicationException($"Status {response.StatusCode} - {message}");
        }

        if (IsHtml(response))
        {
            LogUnexpectedResponseBody(response);
            throw new PluginApplicationException(BuildHtmlErrorMessage(response));
        }

        var error = TryParseErrorResponse(response.Content);
        if (error is null)
        {
            LogUnexpectedResponseBody(response);
            throw new PluginApplicationException($"Status {(int)response.StatusCode} - {response.Content.Trim()}");
        }

        throw new PluginApplicationException($"Status {error.Status} - {error.Message}");
    }

    private void LogUnexpectedResponseBody(RestResponse response)
    {
        _logger?.LogError?.Invoke(
            "Unexpected welocalize error response. Status: {StatusCode}. Content type: {ContentType}. Body: {Body}",
            [(int)response.StatusCode, response.ContentType ?? string.Empty, response.Content ?? string.Empty]);
    }

    private static ErrorResponse? TryParseErrorResponse(string content)
    {
        try
        {
            return JsonConvert.DeserializeObject<ErrorResponse>(content);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static bool IsHtml(RestResponse response)
    {
        var hasHtmlContentType = response.ContentType?.Contains("html", StringComparison.OrdinalIgnoreCase) ?? false;
        return hasHtmlContentType || response.Content!.TrimStart().StartsWith('<');
    }

    private static string BuildHtmlErrorMessage(RestResponse response)
    {
        var titleMatch = Regex.Match(
            response.Content!,
            @"<title[^>]*>(.*?)</title>",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        var title = titleMatch.Success ? WebUtility.HtmlDecode(titleMatch.Groups[1].Value).Trim() : string.Empty;
        if (title.Length > 0)
            return title;

        var bodyText = ExtractVisibleText(response.Content!);
        var details = bodyText.Length > 0 ? bodyText : "No content returned by server";
        return $"Status {(int)response.StatusCode} - {details}";
    }

    private static string ExtractVisibleText(string html)
    {
        var withoutScriptsAndStyles = Regex.Replace(
            html,
            @"<(script|style)[^>]*>.*?</\1>",
            " ",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        var withoutTags = Regex.Replace(withoutScriptsAndStyles, @"<[^>]+>", " ");
        return Regex.Replace(WebUtility.HtmlDecode(withoutTags), @"\s+", " ").Trim();
    }
}
