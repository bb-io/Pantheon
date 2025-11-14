using Newtonsoft.Json;

namespace Apps.Pantheon.Models.Response.Error;

public class ErrorResponse
{
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}
