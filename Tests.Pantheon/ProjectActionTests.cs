using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;

namespace Tests.Pantheon;

[TestClass]
public class ProjectActionTests : TestBase
{
    [TestMethod]
    public async Task SearchProjects_ReturnsProjects()
    {
		// Arrange
		var action = new ProjectActions(InvocationContext);

		// Act
		var result = await action.SearchProjects();

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
	}
}
