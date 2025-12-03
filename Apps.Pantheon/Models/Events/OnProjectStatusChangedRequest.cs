using Apps.Pantheon.Handlers.Static;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Models.Events;

public class OnProjectStatusChangedRequest
{
    [StaticDataSource(typeof(ProjectStatusDataHandler))]
    public string? Status { get; set; }
}
