using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Filters.Analysis.Models;
using Newtonsoft.Json;

namespace Apps.Pantheon.Helper;

public static class AnalysisFileParser
{
    public static async Task<Dictionary<string, Dictionary<string, decimal>>> ParseAnalysis(
        IFileManagementClient fileManagementClient, 
        FileReference? exportedAnalysis)
    {
        if (exportedAnalysis is null)
            return [];

        await using var fileStream = await fileManagementClient.DownloadAsync(exportedAnalysis);
        using var reader = new StreamReader(fileStream);
        var jsonString = await reader.ReadToEndAsync();
        
        var analysisObjects = JsonConvert.DeserializeObject<List<Analysis>>(jsonString) ?? [];

        return analysisObjects.ToDictionary(
            analysis => analysis.Locale.Replace('_', '-'), 
            analysis => analysis.RawValues
        );
    }
}