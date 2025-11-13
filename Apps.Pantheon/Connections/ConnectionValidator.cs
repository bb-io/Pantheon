using RestSharp;
using Apps.Pantheon.Api;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.Pantheon.Connections;

public class ConnectionValidator: IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = new PantheonClient(authenticationCredentialsProviders);
            var request = new RestRequest("projects", Method.Get);

            await client.ExecuteWithErrorHandling(request);

            return new() { IsValid = true };
        } 
        catch(Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }

    }
}