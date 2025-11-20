using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Models.Request.File;

public class UploadFileRequest
{
    [Display("File")]
    public FileReference File { get; set; }

    [Display("File type")]
    [StaticDataSource(typeof(FileTypeDataHandler))]
    public string FileType { get; set; }

    [Display("Source language")]
    [StaticDataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [StaticDataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Due date")]
    public DateTime? DueDate { get; set; }
}
