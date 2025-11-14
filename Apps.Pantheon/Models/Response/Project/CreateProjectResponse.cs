using Blackbird.Applications.Sdk.Common;

namespace Apps.Pantheon.Models.Response.Project;

public record CreateProjectResponse
{
    [Display("Created project ID")]
    public string Id { get; set; }
}
