using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Models.Request.File;

public class SearchAssetsRequest
{
    [Display("Asset reference contains")]
    public string? AssetReferenceContains { get; set; }

    [Display("Source language"), StaticDataSource(typeof(LanguageDataHandler))]
    public string? SourceLanguage { get; set; }

    [Display("Target language"), StaticDataSource(typeof(LanguageDataHandler))]
    public string? TargetLanguage { get; set; }
}
