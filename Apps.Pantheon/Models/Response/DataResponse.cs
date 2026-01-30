namespace Apps.Pantheon.Models.Response;

public class DataResponse<T>
{
    public required T Data { get; set; }
}
