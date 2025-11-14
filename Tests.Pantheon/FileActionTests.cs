using Tests.Pantheon.Base;
using Apps.Pantheon.Actions;
using Apps.Pantheon.Models.Identifiers;
using Apps.Pantheon.Models.Request.File;
using Blackbird.Applications.Sdk.Common.Files;

namespace Tests.Pantheon;

[TestClass]
public class FileActionTests : TestBase
{
    [TestMethod]
    public async Task UploadFile_ReturnsUploadFileResponse()
    {
		// Arrange
		var action = new FileActions(InvocationContext, FileManager);
		var project = new ProjectIdentifier { ProjectId = "3378249996" };
		var file = new FileReference { Name = "file.html" };
		var request = new UploadFileRequest 
		{ 
			File = file, 
			FileType = "reference", 
			SourceLanguage = "en-US", 
			TargetLanguage = "sv-SE"  
		};

        // Act
        var result = await action.UploadFile(project, request);

		// Assert
		PrintJsonResult(result);
		Assert.IsNotNull(result);
	}
}
