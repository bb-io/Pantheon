using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.Project;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;

namespace Tests.Pantheon;

[TestClass]
public class ProjectActionTests : TestBase
{
	private ProjectActions Actions => new(InvocationContext, FileManager);
	
    [TestMethod]
    public async Task SearchProjects_WithoutFilters_ReturnsProjects()
    {
		// Arrange
		var request = new SearchProjectsRequest();

		// Act
		var result = await Actions.SearchProjects(request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task SearchProjects_WithFilters_ReturnsProjects()
    {
        // Arrange
        var request = new SearchProjectsRequest 
		{
            Statuses = ["created"],
			DueDateAfter = new DateTime(2025, 11, 15, 10, 0, 0, DateTimeKind.Utc),
			DueDateBefore = new DateTime(2025, 11, 17, 10, 0, 0, DateTimeKind.Utc),
		};

        // Act
        var result = await Actions.SearchProjects(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task SearchProjects_IncorrectDateRange_ThrowsMisconfigException()
    {
        // Arrange
        var request = new SearchProjectsRequest
        {
            DueDateAfter = DateTime.Now + TimeSpan.FromDays(4),
            DueDateBefore = DateTime.Now + TimeSpan.FromDays(1),
        };

        // Act
        var ex = await Assert.ThrowsExactlyAsync<PluginMisconfigurationException>(async () => 
            await Actions.SearchProjects(request)
        );

        // Assert
        Assert.Contains("Invalid date range", ex.Message);
    }

    [TestMethod]
	public async Task GetProjectStatus_ReturnsProjectStatus()
    {
        // Arrange
		var request = new ProjectIdentifier { ProjectId = "3378250003" };

        // Act
        var result = await Actions.GetProjectStatus(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

	[TestMethod]
	public async Task CreateProject_ReturnsProjectId()
	{
		// Arrange
		var request = new CreateProjectRequest
		{
			ProjectReferenceId = "helloAnalysisData1",
			Name = "Test from tests (analysis2)",
			Services = ["13"],
			SourceLanguage = "en-US",
			TargetLanguages = ["es-ES"],
			ExportedAnalysis = new FileReference { Name = "analysis.json" }
		};

		// Act
		var result = await Actions.CreateProject(request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
	}

	[TestMethod]
	public async Task StartProject_WithFilesInIt_IsSuccess()
	{
		// Arrange
		var input = new ProjectIdentifier { ProjectId = "3378250003" };

		// Act
		await Actions.StartProject(input);
    }

    [TestMethod]
    public async Task StartProject_WithoutFilesInIt_ThrowsMisconfigException()
    {
        // Arrange
        var input = new ProjectIdentifier { ProjectId = "3378250001" };

        // Act
        var ex = await Assert.ThrowsExactlyAsync<PluginMisconfigurationException>(async () => 
			await Actions.StartProject(input)
		);

        // Assert
        Assert.Contains("Please upload at least one file before starting", ex.Message);
    }
}
