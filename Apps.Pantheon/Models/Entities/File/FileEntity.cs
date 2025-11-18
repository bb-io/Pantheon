using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Pantheon.Models.Entities.File;

public class FileEntity
{
    [Display("File ID")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display("Asset reference")]
    [JsonProperty("assetReference")]
    public string AssetReference { get; set; }

    [Display("Source language")]
    [JsonProperty("source")]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [JsonProperty("target")]
    public string TargetLanguage { get; set; }
}
