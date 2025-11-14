using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;
using Apps.Pantheon.Models.Request.Project;
using Apps.Pantheon.Models.Identifiers;

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
		var request = new ProjectIdentifier { Id = "3378249983" };

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
			ProjectReferenceId = "abba123",
			Name = "Test from tests",
			Services = ["13"],
			SourceLanguage = "en-US",
			TargetLanguages = ["uk-UA"]
		};

		// Act
		var result = await action.CreateProject(request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
	}

	[TestMethod]
	public async Task StartProject_IsSuccess()
	{
		// Arrange
		var action = new ProjectActions(InvocationContext);
		var input = new ProjectIdentifier { Id = "3378249995" };

		// Act
		await action.StartProject(input);
	}
}
