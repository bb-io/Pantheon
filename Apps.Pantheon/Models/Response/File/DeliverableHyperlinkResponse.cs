using Newtonsoft.Json;

namespace Apps.Pantheon.Models.Response.File;

public class DeliverableHyperlinkResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}
