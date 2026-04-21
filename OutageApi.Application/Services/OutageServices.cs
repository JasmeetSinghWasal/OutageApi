using OutageApi.Application.Interfaces;
using OutageApi.Domain.Entities;
namespace OutageApi.Application.Services;

using OutageApi.Application.DTOs;
public class OutageService : IOutageService
{

    private readonly IOutageRepository _repository;

    public OutageService(IOutageRepository repository)
    {
        _repository = repository;
    }

  public IEnumerable<OutageResponse> All()
    {
        IEnumerable<Outage>? query = _repository.GetAll();

        return query
            .OrderByDescending(o => o.ReportedAt)
            .Select(MapToResponse);
    }

    public IEnumerable<OutageResponse> GetAll(string? area, string? type, int page, int pageSize)
    {
        IEnumerable<Outage>? query = _repository.GetAll();

        if (!string.IsNullOrWhiteSpace(area))
            query = query.Where(o => o.Area.Equals(area, StringComparison.OrdinalIgnoreCase));

        // if (type.HasValue)
        //     query = query.Where(o => o.Type == type.Value);

        return query
            .OrderByDescending(o => o.ReportedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(MapToResponse);
    }

private static OutageResponse MapToResponse(Outage outage) => new()
{
    Id = outage.OutageId,
    Area = outage.Area,
    Type = (OutageApi.Application.DTOs.OutageType)outage.Type,
    Tags = outage.Tags,
    ReportedAt = outage.ReportedAt,
    Status = outage.Status,
    Description = outage.Description
};
public OutageResponse? GetById(Guid id)
{
    var outage = _repository.GetById(id);
    return outage is null ? null : MapToResponse(outage);
}

    public int GetTotalCount(string? area, string? type)
    {
        // var query = _repository.GetAll();
        // if (!string.IsNullOrWhiteSpace(area))
        //     query = query.Where(o => o.Area.Equals(area, StringComparison.OrdinalIgnoreCase));
        // if (type.HasValue)
        //     query = query.Where(o => o.Type == type.Value);
        return 0;
    }

}