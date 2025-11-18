using RestSharp;
using Newtonsoft.Json;
using Apps.Pantheon.Constants;
using Apps.Pantheon.Models.Response;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
using Apps.Pantheon.Models.Response.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;

namespace Apps.Pantheon.Actions;

[ActionList("Files")]
public class FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
    : PantheonInvocable(invocationContext)
{
    [Action("Upload file", Description = "Upload a file to a created project")]
    public async Task<UploadFileResponse> UploadFile(
        [ActionParameter] ProjectIdentifier project, 
        [ActionParameter] UploadFileRequest input)
    {
        var request = new RestRequest($"project/{project.Id}/file", Method.Post);

        var fileStream = await fileManagementClient.DownloadAsync(input.File); 
        using var ms = new MemoryStream();
        await fileStream.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        request.AddFile(input.File.Name, fileBytes, input.File.Name);

        var assetJson = JsonConvert.SerializeObject(new
        {
            name = Path.GetFileNameWithoutExtension(input.File.Name),
            dueDate = input.DueDate,
            languages = new[] { new { source = input.SourceLanguage, target = input.TargetLanguage } },
            type = input.FileType
        }, JsonConfig.Settings);
        request.AddParameter("asset", assetJson);

        var result = await Client.ExecuteWithErrorHandling<DataResponse<UploadFileResponse>>(request);
        return result.Data;
    }

    [Action("Download target file", Description = "Download a file for a target locale from a specific project")]
    public async Task<DownloadTargetFileResponse> DownloadTargetFile(
        [ActionParameter] ProjectIdentifier projectId,
        [ActionParameter] DownloadTargetFileRequest input)
    {
        var request = new RestRequest($"project/{projectId.Id}/deliverable/{input.DeliverableId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling(request);

        if (response.ContentType == "text/html")
        {
            var deliverablesRequest = new RestRequest($"project/{projectId.Id}/deliverables");
            var deliverables = await Client.ExecuteWithErrorHandling<SearchDeliverableFilesResponse>(deliverablesRequest);
            
            var deliverable = deliverables.Data.First(d => d.Id == input.DeliverableId);
            var deliverableFileName = deliverable.AssetReference;
            var deliverableId = deliverable.Id;

            var bytes = response.RawBytes!;
            using var outputStream = new MemoryStream(bytes);
            var file = await fileManagementClient.UploadAsync(outputStream, response.ContentType, deliverableFileName);
            return new(deliverableId, deliverableFileName, file);
        }

        var hyperlinkResponse = JsonConvert.DeserializeObject<DeliverableHyperlinkResponse>(response.Content!) 
            ?? throw new PluginApplicationException("Error while parsing a hyperlink deliverable response");

        return new(hyperlinkResponse.Id, hyperlinkResponse.Name, hyperlinkResponse.Url);
    }

    [Action("Search deliverables", Description = "Get a list of deliverable files for a specific project")]
    public async Task<SearchDeliverableFilesResponse> SearchDeliverables([ActionParameter] ProjectIdentifier project)
    {
        var request = new RestRequest($"project/{project.Id}/deliverables", Method.Get);
        return await Client.ExecuteWithErrorHandling<SearchDeliverableFilesResponse>(request);
    }

    [Action("Delete file", Description = "Delete a specific file from a project")]
    public async Task DeleteFile([ActionParameter] ProjectIdentifier project, [ActionParameter] FileIdentifier fileId)
    {
        var request = new RestRequest($"project/{project.Id}/file/{fileId.Id}", Method.Delete);
        var result = await Client.ExecuteWithErrorHandling<DataResponse<string>>(request);

        if (result.Data != "Success file deleted")
            throw new PluginApplicationException(
                $"Failed to delete file {fileId.Id} from the project {project.Id}: {result.Data}"
            );
    }
}
