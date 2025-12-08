using RestSharp;
using Newtonsoft.Json;
using Apps.Pantheon.Helper;
using Apps.Pantheon.Constants;
using Apps.Pantheon.Extensions;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
using Apps.Pantheon.Models.Response;
using Apps.Pantheon.Models.Response.File;
using Apps.Pantheon.Models.Entities.File;
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
        var request = new RestRequest($"project/{project.ProjectId}/file", Method.Post);

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

    [Action("Download target file", Description = 
        "Download a file for a target locale from a specific project. " +
        "If the project deliverable is a hyperlink, it returns the URL, name and ID without any files")]
    public async Task<DownloadTargetFileResponse> DownloadTargetFile(
        [ActionParameter] ProjectIdentifier projectId,
        [ActionParameter] DownloadTargetFileRequest input)
    {
        var request = new RestRequest($"project/{projectId.ProjectId}/deliverable/{input.DeliverableId}", Method.Get);
        var response = await Client.ExecuteWithErrorHandling(request);

        if (response.ContentType!.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
        {
            var hyperlinkResponse = JsonConvert.DeserializeObject<DeliverableHyperlinkResponse>(response.Content!)
                ?? throw new PluginApplicationException("Error while parsing a hyperlink deliverable response");
            return new(hyperlinkResponse.Id, hyperlinkResponse.Name, hyperlinkResponse.Url);
        }
        else
        {
            var contentDisposition = response.ContentHeaders?
                .FirstOrDefault(h => h.Name!.Equals("Content-Disposition", StringComparison.OrdinalIgnoreCase))
                ?.Value?.ToString();
            var deliverableFileName = ExtractFilenameFromHeader(contentDisposition) ?? input.DeliverableId;

            var bytes = response.RawBytes 
                ?? throw new PluginApplicationException("No file content received from the server");

            await using var outputStream = new MemoryStream(bytes);
            var file = await fileManagementClient.UploadAsync(outputStream, response.ContentType!, deliverableFileName);
            return new(input.DeliverableId, deliverableFileName, file);
        }
    }

    [Action("Search deliverables", Description = "Get a list of deliverable files for a specific project")]
    public async Task<SearchDeliverableFilesResponse> SearchDeliverables(
        [ActionParameter] ProjectIdentifier project,
        [ActionParameter] SearchDeliverablesRequest input)
    {
        var request = new RestRequest($"project/{project.ProjectId}/deliverables", Method.Get);
        request.AddQueryParameterIfNotEmpty("type", input.Type);
        request.AddQueryParameterIfNotEmpty("targetLocale", input.TargetLocale);

        var result = await Client.ExecuteWithErrorHandling<DataResponse<IEnumerable<DeliverableFileEntity>>>(request);
        result.Data = result.Data.FilterByStringContains(input.AssetReferenceContains, x => x.AssetReference);

        return new(result.Data.ToList());
    }

    [Action("Delete file", Description = "Delete a specific file from a project")]
    public async Task DeleteFile(
        [ActionParameter] ProjectIdentifier project, 
        [ActionParameter] FileIdentifier fileId)
    {
        var request = new RestRequest($"project/{project.ProjectId}/file/{fileId.FileId}", Method.Delete);
        var result = await Client.ExecuteWithErrorHandling<DataResponse<string>>(request);

        if (result.Data != "Success file deleted")
            throw new PluginApplicationException(
                $"Failed to delete file {fileId.FileId} from the project {project.ProjectId}: {result.Data}"
            );
    }

    private static string? ExtractFilenameFromHeader(string? header)
    {
        if (string.IsNullOrEmpty(header))
            return null;

        const string fileNameKey = "filename=";

        var index = header.IndexOf(fileNameKey, StringComparison.OrdinalIgnoreCase);
        if (index < 0)
            return null;

        var fileNamePart = header.Substring(index + fileNameKey.Length);

        return fileNamePart.Trim('"');
    }
}
