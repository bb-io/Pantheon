using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Pantheon.Models.Response.File;

public record UploadFileResponse
{
    [Display("File ID")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display("Name")]
    [JsonProperty("name")]
    public string Name { get; set; }
};
