using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;
using Apps.Pantheon.Models.Request.Project;

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

	[TestMethod]
	public async Task GetProjectStatus_ReturnsProjectStatus()
    {
        // Arrange
        var action = new ProjectActions(InvocationContext);
		var request = new GetProjectStatusRequest { ProjectId = "3378249993" };

        // Act
        var result = await action.GetProjectStatus(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

	[TestMethod]
	public async Task CreateProject_ReturnsProjectId()
	{
		// Arrange
		var action = new ProjectActions(InvocationContext);
		var request = new CreateProjectRequest
		{
			ProjectReferenceId = "123",
			Name = "Test from tests",
			Services = ["13"],
			SourceLanguage = "en",
			TargetLanguage = "uk-UA"
		};

		// Act
		var result = await action.CreateProject(request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
	}
}
