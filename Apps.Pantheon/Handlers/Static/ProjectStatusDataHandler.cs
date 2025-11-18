using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Handlers.Static;

public class ProjectStatusDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new List<DataSourceItem>
        {
            new DataSourceItem("created", "Created"),
            new DataSourceItem("processing", "Processing"),
            new DataSourceItem("in scoping", "In scoping"),
            new DataSourceItem("completed", "Completed"),
            new DataSourceItem("error", "Error"),
        };
    }
}
