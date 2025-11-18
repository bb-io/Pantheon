using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Pantheon.Handlers.Static;

public class DeliverableTypeDataHandler : IDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData(DataSourceContext context)
    {
        return new List<DataSourceItem> 
        { 
            new DataSourceItem("deliverable", "Deliverable"),
            new DataSourceItem("deliverable reference", "Deliverable reference"),
        };
    }
}
