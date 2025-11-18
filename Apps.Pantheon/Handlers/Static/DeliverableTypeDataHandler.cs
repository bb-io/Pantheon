using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Pantheon.Handlers.Static;

public class DeliverableTypeDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new List<DataSourceItem> 
        { 
            new DataSourceItem("deliverable", "Deliverable"),
            new DataSourceItem("deliverable reference", "Deliverable reference"),
        };
    }
}
