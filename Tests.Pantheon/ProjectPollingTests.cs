using Tests.Pantheon.Base;
using Apps.Pantheon.Polling;
using Apps.Pantheon.Models.Events;
using Apps.Pantheon.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Tests.Pantheon;

[TestClass]
public class ProjectPollingTests : TestBase
{
    [TestMethod]
    public async Task OnProjectStatusChanged_ReturnsNewProjectStatus()
    {
        // Arrange
        var polling = new ProjectPolling(InvocationContext);
        var project = new ProjectIdentifier { Id = "3378250000" };
        var memory = new ProjectStatusMemory { LastStatus = "created" };
        var request = new PollingEventRequest<ProjectStatusMemory> { Memory = memory, PollingTime = DateTime.UtcNow };

        // Act
        var result = await polling.OnProjectStatusChanged(request, project);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}
