using RestSharp;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Response.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon.Handlers;

public class DeliverableFileDataHandler([ActionParameter] ProjectIdentifier project, InvocationContext context)
    : PantheonInvocable(context), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(project.ProjectId))
            throw new PluginMisconfigurationException("Please specify the project ID first");

        var request = new RestRequest($"project/{project.ProjectId}/deliverables", Method.Get);
        var result = await Client.ExecuteWithErrorHandling<SearchDeliverableFilesResponse>(request);

        return result.Data.Select(x => new DataSourceItem(x.Id, x.ToString()));
    }
}
