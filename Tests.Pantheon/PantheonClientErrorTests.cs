using System.Net;
using Apps.Pantheon.Api;
using Apps.Pantheon.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Tests.Pantheon;

[TestClass]
public class PantheonClientErrorTests
{
    private static readonly AuthenticationCredentialsProvider[] PlaceholderCreds =
    [
        new(CredsNames.BaseUrl, "https://example.com"),
        new(CredsNames.AuthKey, "placeholder-key"),
    ];

    [TestMethod]
    public void ConfigureErrorException_PlainTextBody_ThrowsPluginExceptionWithStatusAndBody()
    {
        var response = BuildResponse(HttpStatusCode.RequestEntityTooLarge, "text/plain", "request entity too large");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response));

        Assert.Contains("413", exception.Message);
        Assert.Contains("request entity too large", exception.Message);
    }

    [TestMethod]
    public void ConfigureErrorException_JsonBody_ThrowsPluginExceptionWithApiMessage()
    {
        var response = BuildResponse(
            HttpStatusCode.BadRequest,
            "application/json",
            "{\"status\":400,\"message\":\"Project reference ID must be unique\"}");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response));

        Assert.AreEqual("Status 400 - Project reference ID must be unique", exception.Message);
    }

    [TestMethod]
    public void ConfigureErrorException_HtmlBody_ThrowsPluginExceptionWithPageTitle()
    {
        var response = BuildResponse(
            HttpStatusCode.BadGateway,
            "text/html",
            "<html><head><title>502 Bad Gateway</title></head><body></body></html>");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response));

        Assert.AreEqual("502 Bad Gateway", exception.Message);
    }

    [TestMethod]
    public void ConfigureErrorException_HtmlBodyWithCharsetContentType_ThrowsPluginExceptionWithPageTitle()
    {
        var response = BuildResponse(
            HttpStatusCode.RequestEntityTooLarge,
            "Text/HTML; charset=utf-8",
            "<html><head><title>413 Request Entity Too Large</title></head><body></body></html>");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response));

        Assert.AreEqual("413 Request Entity Too Large", exception.Message);
    }

    [TestMethod]
    public void ConfigureErrorException_HtmlBodyWithWrongContentType_ThrowsPluginExceptionWithPageTitle()
    {
        var response = BuildResponse(
            HttpStatusCode.ServiceUnavailable,
            "text/plain",
            "\r\n<!DOCTYPE html><html><head><title>503 Service Unavailable</title></head><body></body></html>");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response));

        Assert.AreEqual("503 Service Unavailable", exception.Message);
    }

    [TestMethod]
    public void ConfigureErrorException_HtmlBodyWithoutTitle_ThrowsPluginExceptionWithStatusAndBodyText()
    {
        var response = BuildResponse(
            HttpStatusCode.RequestEntityTooLarge,
            "text/html",
            "<html><head><style>h1 { color: red; }</style></head><body><h1>Payload too large</h1>\r\n" +
            "<pre>request entity too large</pre><script>var ignored = 1;</script></body></html>");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response));

        Assert.AreEqual("Status 413 - Payload too large request entity too large", exception.Message);
    }

    [TestMethod]
    public void ConfigureErrorException_PlainTextBody_LogsResponseBody()
    {
        var loggedParameters = new List<object>();
        var logger = new Logger("Error") { LogError = (_, parameters) => loggedParameters.AddRange(parameters) };
        var response = BuildResponse(HttpStatusCode.RequestEntityTooLarge, "text/plain", "request entity too large");

        Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response, logger));

        CollectionAssert.Contains(loggedParameters, "request entity too large");
    }

    [TestMethod]
    public void ConfigureErrorException_HtmlBody_LogsFullResponseBody()
    {
        const string html = "<html><head><title>502 Bad Gateway</title></head><body><p>upstream details</p></body></html>";
        var loggedParameters = new List<object>();
        var logger = new Logger("Error") { LogError = (_, parameters) => loggedParameters.AddRange(parameters) };
        var response = BuildResponse(HttpStatusCode.BadGateway, "text/html", html);

        Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response, logger));

        CollectionAssert.Contains(loggedParameters, html);
    }

    [TestMethod]
    public void ConfigureErrorException_JsonBody_DoesNotLogResponseBody()
    {
        var loggedParameters = new List<object>();
        var logger = new Logger("Error") { LogError = (_, parameters) => loggedParameters.AddRange(parameters) };
        var response = BuildResponse(HttpStatusCode.BadRequest, "application/json", "{\"status\":400,\"message\":\"Bad request\"}");

        Assert.ThrowsExactly<PluginApplicationException>(() => ThrowConfiguredError(response, logger));

        Assert.IsEmpty(loggedParameters);
    }

    private static RestResponse BuildResponse(HttpStatusCode statusCode, string contentType, string content) =>
        new(new RestRequest())
        {
            StatusCode = statusCode,
            ContentType = contentType,
            Content = content,
        };

    private static void ThrowConfiguredError(RestResponse response, Logger? logger = null) =>
        throw new ExposedPantheonClient(PlaceholderCreds, logger).BuildErrorException(response);

    private class ExposedPantheonClient(IEnumerable<AuthenticationCredentialsProvider> creds, Logger? logger)
        : PantheonClient(creds, logger)
    {
        public Exception BuildErrorException(RestResponse response) => ConfigureErrorException(response);
    }
}
