using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Filters.Analysis.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Apps.Pantheon.Helper;

public static class AnalysisFileParser
{
    public static async Task<Dictionary<string, JToken>> ParseAnalysis(
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
            analysis => analysis.OriginalLocale, 
            analysis => analysis.RawValues
        );
    }
}