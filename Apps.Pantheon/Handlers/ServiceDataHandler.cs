using RestSharp;
using Apps.Pantheon.Models.Response;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon.Handlers;

public class ServiceDataHandler(InvocationContext invocationContext) : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new RestRequest("services", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ListServicesResponse>(request);

        return response.Data.Select(s => new DataSourceItem(s.Id, s.Name));
    }
}
