using Tests.Pantheon.Base;
using Apps.Pantheon.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Tests.Pantheon;

[TestClass]
public class DataHandlerTests : TestBase
{
    [TestMethod]
    public async Task ServiceDataHandler_ReturnsServices()
    {
        // Arrange
        var handler = new ServiceDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        // Assert
        PrintDataHandlerJsonResult(result);
        Assert.IsGreaterThan(0, result.Count());
    }

    [TestMethod]
    public async Task ProjectDataHandler_ReturnsProjectIds()
    {
        // Arrange
        var handler = new ProjectDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        // Assert
        PrintDataHandlerJsonResult(result);
        Assert.IsGreaterThan(0, result.Count());
    }
}
