using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Pantheon.Handlers.Static;

public class ProjectStatusDataHandler : IDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData(DataSourceContext context)
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
