using Apps.Pantheon.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Pantheon.Models.Identifiers;

public class ProjectIdentifier
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; }
}
