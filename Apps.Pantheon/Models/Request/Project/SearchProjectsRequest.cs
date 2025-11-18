using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Models.Request.Project;

public class SearchProjectsRequest
{
    [Display("Statuses")]
    [StaticDataSource(typeof(ProjectStatusDataHandler))]
    public IEnumerable<string>? Statuses { get; set; }

    [Display("Due date after")]
    public DateTime? DueDateAfter { get; set; }

    [Display("Due date before")]
    public DateTime? DueDateBefore { get; set; }
}
