using Newtonsoft.Json;

namespace Apps.Pantheon.Models.Entities.Service;

public class ServiceEntity
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("serviceName")]
    public string Name { get; set; }
}
