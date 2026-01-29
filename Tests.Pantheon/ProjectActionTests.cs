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
    public async Task SearchProjects_WithoutFilters_ReturnsProjects()
    {
		// Arrange
		var action = new ProjectActions(InvocationContext);
		var request = new SearchProjectsRequest();

		// Act
		var result = await action.SearchProjects(request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task SearchProjects_WithFilters_ReturnsProjects()
    {
        // Arrange
        var action = new ProjectActions(InvocationContext);
        var request = new SearchProjectsRequest 
		{
            Statuses = ["created"],
			DueDateAfter = new DateTime(2025, 11, 15, 10, 0, 0, DateTimeKind.Utc),
			DueDateBefore = new DateTime(2025, 11, 17, 10, 0, 0, DateTimeKind.Utc),
		};

        // Act
        var result = await action.SearchProjects(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task SearchProjects_IncorrectDateRange_ThrowsMisconfigException()
    {
        // Arrange
        var action = new ProjectActions(InvocationContext);
        var request = new SearchProjectsRequest
        {
            DueDateAfter = DateTime.Now + TimeSpan.FromDays(4),
            DueDateBefore = DateTime.Now + TimeSpan.FromDays(1),
        };

        // Act
        var ex = await Assert.ThrowsExactlyAsync<PluginMisconfigurationException>(async () => 
            await action.SearchProjects(request)
        );

        // Assert
        Assert.Contains("Invalid date range", ex.Message);
    }

    [TestMethod]
	public async Task GetProjectStatus_ReturnsProjectStatus()
    {
        // Arrange
        var action = new ProjectActions(InvocationContext);
		var request = new ProjectIdentifier { ProjectId = "3378250003" };

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
			ProjectReferenceId = "helloAnalysisObject",
			Name = "Test from tests (analysis)",
			Services = ["13"],
			SourceLanguage = "en-US",
			TargetLanguages = ["uk-UA", "de-AT"],
			DueDate = DateTime.Now + TimeSpan.FromDays(5),
            AnalysisBucketNames = ["LOCKED", "PERFECT"],
            AnalysisValues = ["LockedUA", "PerfectUA", "LockedDE", "PerfectDE"]
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
		var input = new ProjectIdentifier { ProjectId = "3378250003" };

		// Act
		await action.StartProject(input);
    }

    [TestMethod]
    public async Task StartProject_WithoutFilesInIt_ThrowsMisconfigException()
    {
        // Arrange
        var action = new ProjectActions(InvocationContext);
        var input = new ProjectIdentifier { ProjectId = "3378250001" };

        // Act
        var ex = await Assert.ThrowsExactlyAsync<PluginMisconfigurationException>(async () => 
			await action.StartProject(input)
		);

        // Assert
        Assert.Contains("Please upload at least one file before starting", ex.Message);
    }
}
