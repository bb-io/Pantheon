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
    public List<string>? ProjectInfoProperties { get; set; }

    [Display("Project info values")]
    public List<string>? ProjectInfoValues { get; set; }

    [Display("Analysis - Unique Bucket Names", Description = "List of unique bucket definitions (e.g. '100%', 'New') applicable to all locales")]
    public List<string>? AnalysisBucketNames { get; set; }

    [Display("Analysis - All Values", Description = "List of values. Order must be: TargetLanguage 1 (all buckets), then TargetLanguage 2 (all buckets), etc")]
    public List<string>? AnalysisValues { get; set; }

    public void Validate()
    {
        if (ProjectInfoProperties?.Count != ProjectInfoValues?.Count)
        {
            throw new PluginMisconfigurationException(
                "The number of 'Project info properties' " +
                "must match the number of values in the 'Project info values' input"
            );
        }

        if (AnalysisBucketNames != null || AnalysisValues != null)
        {
            var localeCount = TargetLanguages?.Count ?? 0;
            var bucketCount = AnalysisBucketNames?.Count ?? 0;
            var valueCount = AnalysisValues?.Count ?? 0;

            if (valueCount != (localeCount * bucketCount))
            {
                throw new PluginMisconfigurationException(
                    $"You have {localeCount} target languages and {bucketCount} analysis buckets. " +
                    $"This requires exactly {localeCount * bucketCount} values, but you provided {valueCount}."
                );
            }
        }
    }
}
