using RestSharp;
using Apps.Pantheon.Models.Events;
using Apps.Pantheon.Models.Response;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Response.Project;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Pantheon.Polling;

[PollingEventList]
public class ProjectPolling(InvocationContext context) : PantheonInvocable(context)
{
    [PollingEvent("On project status changed", Description = "Triggers when the status of a specified project changes")]
    public async Task<PollingEventResponse<ProjectStatusMemory, GetProjectStatusResponse>> OnProjectStatusChanged(
        PollingEventRequest<ProjectStatusMemory> request,
        [PollingEventParameter] ProjectIdentifier project)
    {
        var statusRequest = new RestRequest($"project/{project.ProjectId}/status", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<DataResponse<GetProjectStatusResponse>>(statusRequest);
        string currentStatus = response.Data.Status;

        if (request.Memory is null || request.Memory.LastStatus is null)
        {
            return new PollingEventResponse<ProjectStatusMemory, GetProjectStatusResponse>
            {
                FlyBird = false,
                Memory = new ProjectStatusMemory(currentStatus),
                Result = null
            };
        }

        string previousStatus = request.Memory.LastStatus;
        if (previousStatus != currentStatus)
        {
            return new PollingEventResponse<ProjectStatusMemory, GetProjectStatusResponse>
            {
                FlyBird = true,
                Memory = new ProjectStatusMemory(currentStatus),
                Result = response.Data
            };
        }

        return new PollingEventResponse<ProjectStatusMemory, GetProjectStatusResponse>
        {
            FlyBird = false,
            Memory = new ProjectStatusMemory(currentStatus),
            Result = null
        };
    }
}
