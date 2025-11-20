using Apps.Pantheon.Models.Entities.Project;

namespace Apps.Pantheon.Models.Response.Project;

public record SearchProjectsResponse(List<ProjectEntity> Data);
