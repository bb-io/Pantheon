using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.Project;
using Blackbird.Applications.Sdk.Common.Exceptions;

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
			ProjectReferenceId = "hello",
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
	public async Task StartProject_WithFilesInIt_IsSuccess()
	{
		// Arrange
		var action = new ProjectActions(InvocationContext);
		var input = new ProjectIdentifier { Id = "3378250001" };

		// Act
		await action.StartProject(input);
    }

    [TestMethod]
    public async Task StartProject_WithoutFilesInIt_ThrowsMisconfigException()
    {
        // Arrange
        var action = new ProjectActions(InvocationContext);
        var input = new ProjectIdentifier { Id = "3378250001" };

        // Act
        var ex = await Assert.ThrowsExactlyAsync<PluginMisconfigurationException>(async () => 
			await action.StartProject(input)
		);

        // Assert
        Assert.Contains("Please upload at least one file before starting", ex.Message);
    }
}
