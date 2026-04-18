using OutageApi.Domain.Entities;
namespace OutageApi.Application.Interfaces;

using OutageApi.Application.DTOs;


public interface IOutageService
{
    IEnumerable<OutageResponse> All();

    IEnumerable<OutageResponse> GetAll(string? area, string? type, int page, int pageSize);
    
    OutageResponse? GetById(Guid id);
    int GetTotalCount(string? area, string? type);
}