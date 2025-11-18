using Apps.Pantheon.Handlers;
using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Models.Request.File;

public class SearchDeliverablesRequest
{
    [Display("Asset reference contains")]
    public string? AssetReferenceContains { get; set; }

    [Display("Target locale")]
    [DataSource(typeof(DeliverableTargetLocaleDataHandler))]
    public string? TargetLocale { get; set; }

    [Display("Type")]
    [StaticDataSource(typeof(DeliverableTypeDataHandler))]
    public string? Type { get; set; }
}
