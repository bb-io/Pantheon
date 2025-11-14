using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Pantheon.Models.Entities.Project;

public class ProjectEntity
{
    [JsonProperty("id")]
    [Display("Project ID")]
    public string Id { get; set; }

    [JsonProperty("status")]
    [Display("Status")]
    public string Status { get; set; }

    [JsonProperty("languages")]
    [Display("Languages")]
    public List<ProjectLanguagesEntity> Languages { get; set; }

    [JsonProperty("dueDate")]
    [Display("Due date")]
    public DateTime? DueDate { get; set; }
}
