using Apps.Pantheon.Models.Entities.File;

namespace Apps.Pantheon.Models.Response.File;

public record SearchFilesResponse(List<FileEntity> Data);
