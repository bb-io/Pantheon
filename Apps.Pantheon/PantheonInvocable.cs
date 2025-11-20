using Apps.Pantheon.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon;

public class PantheonInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected PantheonClient Client { get; }
    public PantheonInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }
}