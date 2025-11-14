using RestSharp;
using Newtonsoft.Json;
using Apps.Pantheon.Constants;
using Apps.Pantheon.Models.Response;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
using Apps.Pantheon.Models.Response.File;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
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
}
