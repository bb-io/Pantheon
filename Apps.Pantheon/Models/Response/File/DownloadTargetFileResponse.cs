using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Pantheon.Models.Response.File;

public record DownloadTargetFileResponse(FileReference File, object Object);