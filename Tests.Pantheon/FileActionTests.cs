using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Tests.Pantheon;

[TestClass]
public class FileActionTests : TestBase
{
    [TestMethod]
    public async Task UploadFile_ReturnsUploadFileResponse()
    {
		// Arrange
		var action = new FileActions(InvocationContext, FileManager);
		var project = new ProjectIdentifier { Id = "3378249998" };
		var file = new FileReference { Name = "file1.html" };
		var request = new UploadFileRequest 
		{ 
			File = file, 
			FileType = "work", 
			SourceLanguage = "en-US",
			TargetLanguage = "uk-UA"
		};

        // Act
        var result = await action.UploadFile(project, request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
	}

	[TestMethod]
	public async Task SearchDeliverables_WithoutFilters_ReturnsFiles()
	{
		// Arrange
		var action = new FileActions(InvocationContext, FileManager);
		var project = new ProjectIdentifier { Id = "3378249999" };

		// Act
		var result = await action.SearchDeliverables(project);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

	[TestMethod]
	public async Task DeleteFile_IsSuccess()
	{
		// Arrange
		var action = new FileActions(InvocationContext, FileManager);
		var project = new ProjectIdentifier { Id = "3378249995" };
		var fileId = new FileIdentifier { Id = "1708922" };

		// Act
		await action.DeleteFile(project, fileId);
    }

	[TestMethod]
	public async Task DownloadTargetFile_ExistingDeliverable_IsSuccess()
    {
        // Arrange
        var action = new FileActions(InvocationContext, FileManager);
        var project = new ProjectIdentifier { Id = "3378249999" };
        var request = new DownloadTargetFileRequest { DeliverableId = "20981422" };

        // Act
        var result = await action.DownloadTargetFile(project, request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DownloadTargetFile_NonExistingDeliverable_IsSuccess()
    {
        // Arrange
        var action = new FileActions(InvocationContext, FileManager);
        var project = new ProjectIdentifier { Id = "3378249999" };
        var request = new DownloadTargetFileRequest { DeliverableId = "nonexisting" };

        // Act
        await Assert.ThrowsExactlyAsync<PluginApplicationException>(async () => 
            await action.DownloadTargetFile(project, request)
        );
    }
}
