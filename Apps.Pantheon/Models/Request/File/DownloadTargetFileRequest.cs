using Apps.Pantheon.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Pantheon.Models.Request.File;

public class DownloadTargetFileRequest
{
    [Display("Deliverable file ID")]
    [DataSource(typeof(DeliverableFileDataHandler))]
    public string DeliverableId { get; set; }
}
