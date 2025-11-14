using Apps.Pantheon.Models.Entities.Service;

namespace Apps.Pantheon.Models.Response.Service;

public record ListServicesResponse(List<ServiceEntity> Data);