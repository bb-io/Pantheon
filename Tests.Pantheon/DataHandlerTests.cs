using Tests.Pantheon.Base;
using Apps.Pantheon.Handlers;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
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

    [TestMethod]
    public async Task FileDataHandler_ReturnsFileIds()
    {
        // Arrange
        var project = new ProjectIdentifier { Id = "3378249995" };
        var handler = new FileDataHandler(project, InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        // Assert
        PrintDataHandlerJsonResult(result);
        Assert.IsGreaterThan(0, result.Count());
    }

    [TestMethod]
    public async Task DeliverableFileDataHandler_ReturnsDeliverableFileIds()
    {
        // Arrange
        var project = new ProjectIdentifier { Id = "3378249999" };
        var handler = new DeliverableFileDataHandler(project, InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        // Assert
        PrintDataHandlerJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DeliverableTargetLocaleDataHandler_ReturnsTargetDeliverableLocales()
    {
        // Arrange
        var project = new ProjectIdentifier { Id = "3378249999" };
        var input = new SearchDeliverablesRequest { };
        var handler = new DeliverableTargetLocaleDataHandler(project, input, InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        // Assert
        PrintDataHandlerJsonResult(result);
        Assert.IsNotNull(result);
    }
}
