using RestSharp;
using Apps.Pantheon.Extensions;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
using Apps.Pantheon.Models.Response.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon.Handlers;

public class DeliverableTargetLocaleDataHandler(
    [ActionParameter] ProjectIdentifier project,
    [ActionParameter] SearchDeliverablesRequest input, 
    InvocationContext context) 
    : PantheonInvocable(context), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken token)
    {
        var request = new RestRequest($"project/{project.ProjectId}/deliverables");
        request.AddQueryParameterIfNotEmpty("type", input.Type);
        request.AddQueryParameterIfNotEmpty("targetLocale", input.TargetLocale);

        var result = await Client.ExecuteWithErrorHandling<SearchDeliverableFilesResponse>(request);
        return result.Data.Select(d => new DataSourceItem(d.TargetLanguage, d.TargetLanguage)).DistinctBy(x => x.Value);
    }
}
