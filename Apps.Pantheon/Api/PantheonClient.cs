using Apps.Pantheon.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Pantheon.Api;

public class PantheonClient : BlackBirdRestClient
{
    public PantheonClient(IEnumerable<AuthenticationCredentialsProvider> creds) : base(new()
    {
        BaseUrl = new Uri(""),
    })
    {
        this.AddDefaultHeader("Authorization", creds.Get(CredsNames.Token).Value);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var error = JsonConvert.DeserializeObject(response.Content);
        var errorMessage = "";

        throw new PluginApplicationException(errorMessage);
    }
}