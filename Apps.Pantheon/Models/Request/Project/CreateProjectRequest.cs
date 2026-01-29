using Apps.Pantheon.Handlers;
using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
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
    public List<string> TargetLanguages { get; set; }

    [Display("Services")]
    [DataSource(typeof(ServiceDataHandler))]
    public List<string> Services { get; set; }

    [Display("Project info properties", Description = "Corresponds to the 'Project info values' input. Values must be in the same order")]
    public IEnumerable<string>? ProjectInfoProperties { get; set; }

    [Display("Project info values")]
    public IEnumerable<string>? ProjectInfoValues { get; set; }

    public void Validate()
    {
        if (ProjectInfoProperties?.Count() != ProjectInfoValues?.Count())
        {
            throw new PluginMisconfigurationException(
                "The number of 'Project info properties' must match the number of values in the 'Project info values' input"
            );
        }
    }
}
