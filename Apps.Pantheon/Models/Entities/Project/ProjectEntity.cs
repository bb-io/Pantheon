using Apps.Pantheon.Extensions;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

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

    public override string ToString()
    {
        return $"ID: {Id}, Status: {Status.Capitalize()}{(DueDate.HasValue ? ", Due date: " + DueDate : "")}";
    }
}
