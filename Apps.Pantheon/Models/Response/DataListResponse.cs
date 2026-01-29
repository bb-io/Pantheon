namespace Apps.Pantheon.Models.Response;

public class DataListResponse<T>
{
    public required IEnumerable<T> Data { get; set; }
}
