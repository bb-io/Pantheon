using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Pantheon.Models.Response.File;

public class DownloadTargetFileResponse
{
    [Display("Deliverable ID")]
    public string Id { get; set; }

    [Display("Name")]
    public string Name { get; set; }

    [Display("File")]
    public FileReference? File { get; set; }

    [Display("Deliverable URL")]
    public string? Url { get; set; }

    public DownloadTargetFileResponse(string id, string name, string url)
    {
        Id = id;
        Name = name;
        Url = url;
    }

    public DownloadTargetFileResponse(string id, string name, FileReference file)
    {
        Id = id;
        Name = name;
        File = file;
    }
}