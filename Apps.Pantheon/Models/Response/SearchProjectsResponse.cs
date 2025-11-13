using Apps.Pantheon.Models.Entities.Project;

namespace Apps.Pantheon.Models.Response;

public record SearchProjectsResponse(List<ProjectEntity> Data, object Meta);
