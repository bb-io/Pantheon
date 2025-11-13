using Apps.Pantheon.Models.Entities.Service;

namespace Apps.Pantheon.Models.Response;

public record ListServicesResponse(List<ServiceEntity> Data);