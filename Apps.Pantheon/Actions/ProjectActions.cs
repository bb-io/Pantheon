using RestSharp;
using Apps.Pantheon.Models.Response;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon.Actions;

[ActionList("Projects")]
public class ProjectActions(InvocationContext invocationContext) : Invocable(invocationContext)
{
    [Action("Search projects", Description = "Search all projects")]
    public async Task<SearchProjectsResponse> SearchProjects()
    {
        var request = new RestRequest("projects", Method.Get);
        return await Client.ExecuteWithErrorHandling<SearchProjectsResponse>(request);
    }
}