using OutageApi.Domain.Entities;

namespace OutageApi.Application.Interfaces;

public interface IOutageRepository
{
    IEnumerable<Outage> GetAll();
    Outage? GetById(Guid id);
}