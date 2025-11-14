using Apps.Pantheon.Handlers;
using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Models.Request.Project;

public class CreateProjectRequest
{
    [Display("Project reference ID", Description = "Project identifier. Must be unique")]
    public string ProjectReferenceId { get; set; }

    [Display("Name")]
    public string Name { get; set; }

    [Display("Due date")]
    public DateTime? DueDate { get; set; }

    [Display("Source language")]
    [StaticDataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [StaticDataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Services")]
    [DataSource(typeof(ServiceDataHandler))]
    public List<string> Services { get; set; }
}
