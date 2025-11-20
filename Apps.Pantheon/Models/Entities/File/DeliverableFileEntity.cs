using Newtonsoft.Json;
using Apps.Pantheon.Extensions;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Pantheon.Models.Entities.File;

public class DeliverableFileEntity
{
    [Display("Deliverable ID")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [Display("Asset reference")]
    [JsonProperty("assetReference")]
    public string AssetReference { get; set; }

    [Display("Source language")]
    [JsonProperty("sourceLocale")]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [JsonProperty("targetLocale")]
    public string TargetLanguage { get; set; }

    [Display("Type")]
    [JsonProperty("type")]
    public string Type { get; set; }

    public override string ToString()
    {
        return $"{AssetReference} (ID: {Id}, Target locale: {TargetLanguage}, Type: {Type.Capitalize()})";
    }
}
