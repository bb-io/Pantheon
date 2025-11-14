using Apps.Pantheon.Models.Entities.Service;

namespace Apps.Pantheon.Models.Response.Service;

public record SearchServicesResponse(List<ServiceEntity> Data);