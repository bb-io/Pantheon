using RestSharp;
using Apps.Pantheon.Models.Response.Project;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon.Handlers;

public class ProjectDataHandler(InvocationContext context) : PantheonInvocable(context), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken token)
    {
        var request = new RestRequest("projects", Method.Get);
        var result = await Client.ExecuteWithErrorHandling<SearchProjectsResponse>(request);

        return result.Data.Select(x => new DataSourceItem(x.Id, x.ToString()));
    }
}
