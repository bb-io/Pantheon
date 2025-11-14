using RestSharp;
using Apps.Pantheon.Constants;
using Apps.Pantheon.Models.Response;
using Apps.Pantheon.Models.Request.Project;
using Apps.Pantheon.Models.Response.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;

namespace Apps.Pantheon.Actions;

[ActionList("Projects")]
public class ProjectActions(InvocationContext invocationContext) : PantheonInvocable(invocationContext)
{
    [Action("Search projects", Description = "Search all projects")]
    public async Task<SearchProjectsResponse> SearchProjects()
    {
        var request = new RestRequest("projects", Method.Get);
        return await Client.ExecuteWithErrorHandling<SearchProjectsResponse>(request);
    }

    [Action("Get project status", Description = "Get status for a specified project")]
    public async Task<GetProjectStatusResponse> GetProjectStatus([ActionParameter] GetProjectStatusRequest input)
    {
        var request = new RestRequest($"project/{input.ProjectId}/status", Method.Get);
        var result = await Client.ExecuteWithErrorHandling<DataResponse<GetProjectStatusResponse>>(request);
        return result.Data;
    }

    [Action("Create project", Description = "Create a new project")]
    public async Task<CreateProjectResponse> CreateProject([ActionParameter] CreateProjectRequest input)
    {
        var request = new RestRequest("project", Method.Post);
        var body = new
        {
            projectReferenceId = input.ProjectReferenceId,
            name = input.Name,
            dueDate = input.DueDate,
            languages = new[] { new { source = input.SourceLanguage, target = input.TargetLanguage } },
            services = input.Services.Select(x => new { id = x }).ToArray()
        };

        request.WithJsonBody(body, JsonConfig.Settings);

        var result = await Client.ExecuteWithErrorHandling<DataResponse<CreateProjectResponse>>(request); 
        return result.Data;
    }
}