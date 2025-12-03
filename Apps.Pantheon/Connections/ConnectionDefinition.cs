using Apps.Pantheon.Constants;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.Pantheon.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Authentication key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.BaseUrl) 
                { 
                    DisplayName = "Base URL", 
                    DataItems =
                    [
                        new("https://hypnos-client-api.welocalize.io", "Sandbox"),
                        new("https://hypnos.welocalize.tools", "Production")
                    ] 
                }, 
                new(CredsNames.AuthKey) { DisplayName = "Authentication key", Sensitive = true }
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values) => values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)
        ).ToList();
}